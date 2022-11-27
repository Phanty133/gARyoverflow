using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleHandler : MonoBehaviour
{
	public float deleteTimeMultiplier = 1.1f;

	private void Start() {
		StartCoroutine(ObjDeleteAfterParticles());
	}

	IEnumerator ObjDeleteAfterParticles() {
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.startLifetime.constant * deleteTimeMultiplier);

		Destroy(gameObject);
	}
}
