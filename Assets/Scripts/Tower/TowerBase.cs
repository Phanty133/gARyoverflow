using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Baracuda.Monitoring;
using System.Reflection;

public enum TargetModes
{
	FIRST,
	LAST,
	STRONGEST,
	WEAKEST,
	CLOSE,
	FURTHEST
}

[Serializable]
public struct ModelUpgrade {
	public string upgradeId;
	public GameObject model;

}

[RequireComponent(typeof(Physics))]
public abstract class TowerBase : MonoBehaviour
{
    // Info
    public string type;

	public bool isTargetable { get; private set; }
	public TargetModes targetingMode { get; private set; }

	public List<ModelUpgrade> upgradeModels;

    // Stats
    public float baseCost;

    public float baseDamage;
    public float damage
	{
		get
		{
			return (baseDamage + GetFlatUpgradeMod("damage")) * GetMultUpgradeMod("damage");
		}
	}
    
    public float baseFireRate;
    public float fireRate
    {
        get
        {
            return (baseFireRate + GetFlatUpgradeMod("fireRate")) * GetMultUpgradeMod("fireRate");
        }
    }

	[Monitor]
	private float cooldown = 0f;

    public float baseRange;
    public float range
	{
		get
		{
			return (baseRange + GetFlatUpgradeMod("range")) * GetMultUpgradeMod("range");
		}
	}

    // Upgrades
    public Dictionary<string, float> multUpgradeMods;

	public Dictionary<string, float> flatUpgradeMods;

	public List<UpgradeBase> upgrades;
	public List<UpgradeBase> unlockedUpgrades;

	// Pre-Defined Functions
	protected float GetFlatUpgradeMod(string key) {
		if(flatUpgradeMods.ContainsKey(key)) return flatUpgradeMods[key];
		else return 0;
	}

	protected float GetMultUpgradeMod(string key) {
		if(multUpgradeMods.ContainsKey(key)) return multUpgradeMods[key];
		else return 1;
	}

	protected List<Enemy> GetEnemies(Vector3? posOverride = null, bool infiniteY = false) {
		Collider[] surrounded = infiniteY ? 
								Physics.OverlapCapsule(
									new Vector3(transform.position.x, -100000, transform.position.z), 
									new Vector3(transform.position.x, 100000, transform.position.z),
									range,
									1<<LayerMask.NameToLayer("Enemy")) :
								Physics.OverlapSphere(posOverride ?? transform.position, range, 1<<LayerMask.NameToLayer("Enemy"));

		return ListUtil.ArrayToList(surrounded)
					   .FindAll(collider => collider.gameObject.GetComponent<Enemy>() != null)
					   .Select(collider => collider.gameObject.GetComponent<Enemy>()).ToList();
	}

	protected Enemy FindTarget(Func<Enemy, bool> filter = null, Vector3? posOverride = null, TargetModes? targetingOverride = null) {
		List<Enemy> enemies = GetEnemies(posOverride);
		if(filter != null) enemies = enemies.FindAll(enemy => filter.Invoke(enemy));

		switch (targetingOverride ?? targetingMode) {
			case TargetModes.FIRST:
				return ListUtil.GetMaxFromList(enemies, (a, b) => a.movement.TrackProgress > b.movement.TrackProgress);
			case TargetModes.LAST:
				return ListUtil.GetMaxFromList(enemies, (a, b) => a.movement.TrackProgress < b.movement.TrackProgress);
			case TargetModes.CLOSE:
				return ListUtil.GetMaxFromList(enemies, (a, b) => (a.transform.position - transform.position).magnitude < (b.transform.position - transform.position).magnitude);
			case TargetModes.FURTHEST:
				return ListUtil.GetMaxFromList(enemies, (a, b) => (a.transform.position - transform.position).magnitude > (b.transform.position - transform.position).magnitude);
			case TargetModes.STRONGEST:
				return ListUtil.GetMaxFromList(enemies, (a, b) => a.stats.baseHealth > b.stats.baseHealth); // TODO: change this to relative strength
			case TargetModes.WEAKEST:
				return ListUtil.GetMaxFromList(enemies, (a, b) => a.stats.baseHealth < b.stats.baseHealth); // TODO: change this to relative strength
			default:
				throw new System.Exception("Undefined behaviour for target type " + targetingMode.ToString("g"));
		}
	}

	public List<UpgradeBase> GetValidUpgrades() {
		// Debug.Log(upgrades.Count);
		return upgrades.FindAll(upgrade => IsUpgradeValid(upgrade));
	}

	bool IsUpgradeValid(UpgradeBase upgrade) {
		return upgrade.prereqs.All(x => unlockedUpgrades.Any(y => y.id == x)) && 
			   !upgrade.exclusive.Any(x => unlockedUpgrades.Any(y => y.id == x)) &&
			   !unlockedUpgrades.Any(x => x.id == upgrade.id);
	}

	public bool ApplyUpgrade(string upgradeId) {
		UpgradeBase upgrade = upgrades.FirstOrDefault(x => x.id == upgradeId);
		if(upgrade == null || !IsUpgradeValid(upgrade)) return false;

		upgrade.ApplyUpgrade(ref flatUpgradeMods, ref multUpgradeMods);
		unlockedUpgrades.Add(upgrade);
		return true;
	}

    public void PromptUpgrade() {
		// Debug.Log("Get clicked on!!");
        GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>().Prompt(this);
    }

	public abstract void Damage();
	protected abstract void OnUpdate();
	protected abstract void OnStart();

	void Start() {
		multUpgradeMods = new Dictionary<string, float>();
		flatUpgradeMods = new Dictionary<string, float>();

		upgrades = new List<UpgradeBase>();
		unlockedUpgrades = new List<UpgradeBase>();

		OnStart();

		Type parent = typeof(UpgradeBase);
		Type[] types = Assembly.GetAssembly(parent).GetTypes();
		Type[] inheritingTypes = types.Where(t => t.IsSubclassOf(parent)).ToArray();
		for (int x = 0; x < inheritingTypes.Length; x++)
		{
			UpgradeBase up = (UpgradeBase)Activator.CreateInstance(inheritingTypes[x]);
			// Debug.Log(type);
			if(up.tower.Equals(type)) upgrades.Add(up);
		}
		// Debug.Log(upgrades.Count);

        this.StartMonitoring();
	}

	void OnDestroy() {
		this.StopMonitoring();
	}

	void Update() {
		if(cooldown <= 0) {
			Damage();
			cooldown = 1/fireRate;
		}
		if(cooldown > 1/fireRate) cooldown = 1 / fireRate;

		foreach(Transform child in transform) {
			child.gameObject.SetActive(false);
		}

		foreach(ModelUpgrade mu in upgradeModels) {
			if(unlockedUpgrades.Any(x => x.id == mu.upgradeId)) {
				upgradeModels.Find(x => x.upgradeId == mu.upgradeId).model.SetActive(true);
				break;
			}
		}

		if(unlockedUpgrades.Count == 0) upgradeModels.Find(x => x.upgradeId == "initial").model.SetActive(true);

		OnUpdate();

		cooldown -= Time.deltaTime;
	}
}