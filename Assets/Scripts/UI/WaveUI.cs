using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
	public GameObject waveObj;

	private TMP_Text waveText;
	private WaveManager waveManager;

	private void Start() {
		waveText = waveObj.GetComponent<TMP_Text>();

		waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
		waveManager.OnWaveStart += OnWaveChange;

		OnWaveChange(-1);
	}

	void OnWaveChange(int wave) {
		waveText.text = (wave + 1).ToString() + "/" + waveManager.Count.ToString();
	}
}
