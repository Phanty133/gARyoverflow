using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Summoner : EnemyModifier
{
	public GameObject summonChild;
	public float spawnDelay = 1f;
	public int spawnCount = 3;
	public float spawnOffset = 0.075f;
	public float baseOffset = 0.25f;
	public float randomOffsetSpread = 0.1f;

	private WaveManager waveManager;

	override public void Init() {
		waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();

		StartCoroutine(Summon());
	}

	void SpawnChild(float offset) {
		float curSegIndex = self.movement.segmentIndex;
		var (nodeStartPos, nodeEndPos) = self.movement.track.GetSegmentBounds(curSegIndex);
		Vector3 moveDir = (nodeEndPos - nodeStartPos).normalized;

		GameObject enemyObj = Instantiate(summonChild, transform.position + moveDir * offset, new Quaternion(), transform.parent);
		Enemy enemy = enemyObj.GetComponent<Enemy>();

		enemy.Init(self.movement.track);
		waveManager.BindEnemyEvents(enemy);

		enemy.movement.segmentIndex = curSegIndex;
		enemy.movement.distOnSegment = self.movement.distOnSegment + offset;
	}

	IEnumerator Summon() {
		yield return new WaitForSeconds(spawnDelay);

		for (int i = 0; i < spawnCount; i++) {
			float offsetSpread = (UnityEngine.Random.value * 2 - 1) * randomOffsetSpread;
			float offset = baseOffset + offsetSpread + i * spawnOffset;
			SpawnChild(offset * -1);
		}

		StartCoroutine(Summon());
	}
}
