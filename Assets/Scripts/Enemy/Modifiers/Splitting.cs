using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Splitting : EnemyModifier
{
	public GameObject splitterChild;
	
	private WaveManager waveManager;

	void OnDeath() {
		waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();

		SpawnChild(-0.125f);
		SpawnChild(0.125f);
	}

	void SpawnChild(float offset) {
		float curSegIndex = self.movement.segmentIndex;
		var (nodeStartPos, nodeEndPos) = self.movement.track.GetSegmentBounds(curSegIndex);
		Vector3 moveDir = (nodeEndPos - nodeStartPos).normalized;

		GameObject enemyObj = Instantiate(splitterChild, transform.position + moveDir * offset, new Quaternion(), transform.parent);
		Enemy enemy = enemyObj.GetComponent<Enemy>();

		enemy.Init(self.movement.track);
		waveManager.BindEnemyEvents(enemy);

		enemy.movement.segmentIndex = curSegIndex;
		enemy.movement.distOnSegment = self.movement.distOnSegment + offset;
	}

	override public void Init() {
		self.OnDeath += OnDeath;
	}
}
