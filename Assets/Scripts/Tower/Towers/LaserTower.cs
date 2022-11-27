using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Physics))]
public class LaserTower : TowerBase
{
	public float distanceCoeff = 1;

	public override void Damage() {
		List<Enemy> enemies = GetEnemies(infiniteY: true);

		foreach(Enemy enemy in enemies) {
			enemy.Damage(damage * Mathf.Pow(0.25f, Mathf.Abs(transform.position.y - enemy.transform.position.y) * distanceCoeff));
		}
	}
	protected override void OnUpdate() { }
	protected override void OnStart() { 
		type = "laser";
	}
}