using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
	public delegate void WaveStart(int wave);
	public event WaveStart OnWaveStart;
	
	public delegate void WaveEnd();
	public event WaveEnd OnWaveEnd;
	
	public Wave ActiveWave {
		get { return waves[wave]; }
	}

	public int ActiveEnemies {
		get { return enemyContainer.transform.childCount; }
	}

	public int Count {
		get { return waves.Count; }
	}

	public List<Wave> waves = new List<Wave>();
	public GameObject enemyContainer;
	public int wave = 0;
	public bool started = false;
	public bool paused = true;

	private int spawnedEnemies = 0;
	private EnemyManager enemyManager;
	private TrackManager trackManager;
	private int waveSubset = 0;
	private int waveSubsetSpawned = 0;

	public void StartWaves() {
		started = true;
		SendWave();
	}

	public void NextWaveHandler() {
		if (!started) {
			StartWaves();
		} else {
			SendWave();
		}
	}

	public void SendWave() {
		if (!paused) return;

		waveSubset = 0;
		waveSubsetSpawned = 0;
		spawnedEnemies = 0;

		paused = false;
		OnWaveStart?.Invoke(wave);
		StartCoroutine(SendEnemy());
	}

	IEnumerator SendEnemy() {
		if (waveSubsetSpawned >= ActiveWave.subsets[waveSubset].count) {
			waveSubset++;
			waveSubsetSpawned = 0;
		}

		yield return new WaitForSeconds(ActiveWave.subsets[waveSubset].spawnDelay / 1000f);

		if (waveSubset < ActiveWave.subsets.Count) {
			SpawnEnemy(ActiveWave.subsets[waveSubset].id);
			spawnedEnemies++;
			waveSubsetSpawned++;

			StartCoroutine(SendEnemy());
		}
	}

	public void SpawnEnemy(int id) {
		GameObject enemyPrefab = enemyManager.enemies[id];
		float strength = enemyPrefab.GetComponent<Enemy>().Strength;
		Track track = SelectRandomTrack(strength, ActiveWave.pathSelection);

		GameObject enemyObj = Instantiate(enemyPrefab, track.StartPosition, new Quaternion(), enemyContainer.transform);
		Enemy enemy = enemyObj.GetComponent<Enemy>();

		enemy.Init(track);
		BindEnemyEvents(enemy);
	}

	public Track SelectRandomTrack(float strength, List<PathProbability> probabilities) {
		float prob;

		if (probabilities.Count != 0) {
			prob = probabilities.FirstOrDefault(pp => strength >= pp.strengthThreshold).probability;
		} else {
			prob = 0.5f;
		}

		int trackIndex = Random.value < prob ? 1 : 0;

		return trackManager.tracks[trackIndex];
	}

	public void OnEnemyRemove() {
		StartCoroutine(OnEnemyRemoveEOF());
	}

	IEnumerator OnEnemyRemoveEOF() {
		yield return new WaitForEndOfFrame();

		if (ActiveEnemies == 0 && spawnedEnemies >= ActiveWave.Count) {
			paused = true;
			wave++;
			OnWaveEnd?.Invoke();
		}
	}

	public void BindEnemyEvents(Enemy enemy) {
		enemy.OnDeath += OnEnemyRemove;
		enemy.OnEndReached += OnEnemyRemove;
	}

	private void Start() {
		enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
		trackManager = GameObject.FindGameObjectWithTag("TrackManager").GetComponent<TrackManager>();
	}
}
