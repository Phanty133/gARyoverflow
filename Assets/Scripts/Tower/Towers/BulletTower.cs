using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Physics))]
public class BulletTower : TowerBase
{
	public float baseCharges;
    public float charges
    {
        get
        {
            return (baseCharges + GetFlatUpgradeMod("charges")) * GetMultUpgradeMod("charges");
        }
    }

	public float baseLifetime;
    public float lifetime
    {
        get
        {
            return (baseLifetime + GetFlatUpgradeMod("lifetime")) * GetMultUpgradeMod("lifetime");
        }
    }

	public float baseVelocity;
    public float velocity
    {
        get
        {
            return (baseLifetime + GetFlatUpgradeMod("velocity")) * GetMultUpgradeMod("velocity");
        }
    }

	public GameObject projectileObj;

	IEnumerator Shoot(Enemy enemy) {
		GameObject projectile = Instantiate(projectileObj, transform.position, new Quaternion());

		Projectile proj = projectile.GetComponent<Projectile>();
		proj.charges = (int) charges;
		proj.lifetime = lifetime;
		proj.target = enemy;
		proj.damage = damage;
		proj.parent = this;
		proj.velocity = velocity;

		float x = transform.eulerAngles.x;
		float z = transform.eulerAngles.z;
		transform.LookAt(enemy.transform);
		transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, z);

		if(flatUpgradeMods.ContainsKey("shotIncrease")) {
			yield return new WaitForSeconds(0.5f);
			Enemy target = FindTarget();
			if(target) Shoot(target);
		}
	}

	public Enemy FindNewTarget(Vector3 position) {
		return FindTarget(enemy => CanHit(enemy), position, TargetModes.CLOSE);
	}

	bool CanHit(Enemy enemy) {
		return !Physics.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, Vector3.Distance(transform.position, enemy.transform.position), ~(1<<LayerMask.NameToLayer("Enemy") + 1<<LayerMask.NameToLayer("Tower")));
	}

	public override void Damage() {
		Enemy target = FindTarget(enemy => CanHit(enemy));
		if(target) Shoot(target);
	}
	protected override void OnUpdate() { }
	protected override void OnStart() { 
		type = "bullet";
	}
}