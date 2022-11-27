using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public delegate void Death();
	public event Death OnDeath;

	public delegate void EndReached();
	public event EndReached OnEndReached;

	public delegate void Spawn();
	public event Spawn OnSpawn;

	public delegate void Damaged(float amount);
	public event Damaged OnDamaged;

	public delegate void Healed(float amount);
	public event Healed OnHealed;

	// The stats that weren't modified, are set to NaN
	public delegate void StatsModified(EnemyStatModifiers deltaDebuffs);
	public event StatsModified OnStatsModified;

	public EnemyMovement movement;
	public EnemyStats stats;
	public EnemyStatModifiers statModifiers;

	private StatManager statManager;

	public float Strength {
		get { return strength; }
	}

	public int ID {
		get { return id; }
	}

	[SerializeField]
	private int id = 0;

	[SerializeField]
	private float strength = 0;
	public float health;
	public int damage = 1;
	public int deathMoney = 100;
	public GameObject deathParticlePrefab;

	public void Damage(float amount) {
		float effectiveDamage = amount * statModifiers.damageModifier;

		health -= effectiveDamage;
		OnDamaged?.Invoke(effectiveDamage);

		// Debug.Log($"Oh no I have been hurt owieeee :(( MY health is now {health}");
	}

	public void Heal(float amount) {
		float effectiveHealAmount = Mathf.Min(stats.EffectiveHealth - health, amount);

		health += effectiveHealAmount;
		OnHealed?.Invoke(effectiveHealAmount);

		Debug.Log("Health now: " + health.ToString());
	}

	public void ModifySpeed(float newSpeedModifier, float decay = 0f) {
		statModifiers.speedModifier = newSpeedModifier;
		movement.movementModifier = newSpeedModifier;
		OnStatsModified?.Invoke(new EnemyStatModifiers() { speedModifier = newSpeedModifier, damageModifier = float.NaN });

		Debug.Log("Speed modified to: " + newSpeedModifier.ToString());

		if (decay != 0) {
			StartCoroutine(SpeedDecay(decay));
		}
	}

	private IEnumerator SpeedDecay(float decayTime) {
		yield return new WaitForSeconds(decayTime);

		ModifySpeed(1f);
	}

	public void ModifyDamage(float newDamageModifier) {
		statModifiers.damageModifier = newDamageModifier;
		OnStatsModified?.Invoke(new EnemyStatModifiers() { speedModifier = float.NaN, damageModifier = newDamageModifier });

		Debug.Log("Damage modified to: " + newDamageModifier.ToString());
	}

	private void OnEndReach() {
		Debug.Log("fucking noob");
		statManager.DecreaseHealth(damage);
		OnEndReached?.Invoke();
		Destroy(gameObject);
	}

	private void OnDie() {
		statManager.ChangeMoney(deathMoney);
		OnDeath?.Invoke();
		Instantiate(deathParticlePrefab, transform.position, new Quaternion());
		Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
	{
		statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
		// Init();
		OnSpawn?.Invoke();
	}

	public void Init(Track track) {
		statModifiers = new EnemyStatModifiers() { speedModifier = 1f, damageModifier = 1f };

		movement = GetComponent<EnemyMovement>();
		stats = GetComponent<EnemyStats>();

		health = stats.EffectiveHealth;
		movement.movementSpeed = stats.speed;
		movement.track = track;

		movement.OnStopped += OnEndReach;

		if (TryGetComponent<EnemyModifier>(out EnemyModifier mod)) {
			mod.self = this;
			mod.Init();
		}
	}

	private void LateUpdate() {
		if (health <= 0) {
			OnDie();
		}
	}

	private void Update() {
		// Damage(5 * Time.deltaTime);
		// Debug.Log(movement.TrackProgress);
	}
}
