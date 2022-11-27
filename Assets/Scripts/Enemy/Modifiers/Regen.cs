using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Regen : EnemyModifier
{
	public float minHealAmount = 2;
	public float healPercentage = 0.05f; // Percentage of total health
	public float healDelay = 3f; // Seconds

	public float range = 3f;
	public float levelYApprox = 0.5f;

	override public void Init() {
		StartCoroutine(PulseHeal());
	}

	IEnumerator PulseHeal() {
		yield return new WaitForSeconds(healDelay);

		List<Enemy> nearby = FindNearbyEnemies();

		foreach (Enemy enemy in nearby) {
			enemy.Heal(Mathf.Max(minHealAmount, enemy.stats.EffectiveHealth * healPercentage));
		}

		StartCoroutine(PulseHeal());
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
}
