using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake() {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	// Start is called before the first frame update
	void Start()
	{
		// StartCoroutine(wave());
		// Time.timeScale = 5f;
	}
}
