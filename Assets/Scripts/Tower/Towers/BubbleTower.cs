using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Physics))]
public class BubbleTower : TowerBase
{
    public float modifierFactor = 0.75f;
	public float decayTime = 5f;
	private ParticleSystem particleSystem;

	public override void Damage() {
		List<Enemy> enemies = GetEnemies();

		foreach(Enemy enemy in enemies) {
			enemy.ModifySpeed(modifierFactor, decayTime);
		}

		particleSystem.Play();
	}
	protected override void OnUpdate() { }
	protected override void OnStart() { 
		type = "bubble";
		particleSystem = transform.GetComponentInChildren<ParticleSystem>();
	}
}