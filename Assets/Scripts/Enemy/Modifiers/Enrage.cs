using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enrage : EnemyModifier
{
	public float speedBuffModifier = 1.25f;
	public float modifierDecayTime = 1f;

	public float range = 3f;
	public float levelYApprox = 0.5f;

	private bool enraged = false;
	private List<Enemy> prevNearbyEnemies = new List<Enemy>();
	private Dictionary<Enemy, Coroutine> activeRemovalCoroutines = new Dictionary<Enemy, Coroutine>();

	override public void Init() {
		self.OnDamaged += OnDamaged;
	}

	private List<Enemy> FindNearbyEnemies() {
		List<Enemy> output = new List<Enemy>();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, 1 << LayerMask.NameToLayer("Enemy"));

		float yMin = transform.position.y - levelYApprox;
		float yMax = transform.position.y + levelYApprox;

		foreach (var hitCollider in hitColliders) {
			GameObject obj = hitCollider.gameObject;
			float objY = transform.position.y;

			if (obj == gameObject) continue;
			if (objY < yMin || objY > yMax) continue;

			output.Add(obj.GetComponent<Enemy>());
		}

		return output;
	}

	void OnDamaged(float amount) {
		enraged = self.health < self.stats.EffectiveHealth * 0.5;
		self.ModifySpeed(enraged ? speedBuffModifier : 1f);
	}

	IEnumerator RemoveBuff(Enemy enemy) {
		yield return new WaitForSeconds(modifierDecayTime);

		if (enemy.isActiveAndEnabled) {
			enemy.ModifySpeed(1f);
			activeRemovalCoroutines.Remove(enemy);
		}
	}

	private void Update() {
		if (enraged) {
			List<Enemy> nearbyEnemies = FindNearbyEnemies();

			foreach (Enemy enemy in nearbyEnemies) {
				enemy.ModifySpeed(speedBuffModifier);

				if (activeRemovalCoroutines.ContainsKey(enemy)) {
					StopCoroutine(activeRemovalCoroutines[enemy]);
				}
			}

			foreach (Enemy enemy in prevNearbyEnemies) {
				if (nearbyEnemies.Contains(enemy)) continue;

				RemoveBuff(enemy);
			}

			prevNearbyEnemies = nearbyEnemies;
		} else if (prevNearbyEnemies.Count != 0) {
			foreach(Enemy enemy in prevNearbyEnemies) {
				StartCoroutine(RemoveBuff(enemy));
			}

			prevNearbyEnemies.Clear();
		}
	}
}
