using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	public GameObject pauseMenu;

	public void OpenPauseMenu() {
		pauseMenu.SetActive(true);
	}

	public void ClosePauseMenu() {
		pauseMenu.SetActive(false);
	}

	public void Pause() {
		Time.timeScale = 0;
	}
	
	public void Unpause() {
		Time.timeScale = 1;
	}

	public void PauseHandler() {
		OpenPauseMenu();
		Pause();
	}

	public void UnpauseHandler() {
		ClosePauseMenu();
		Unpause();
	}

	public void ReturnToMain() {
		Unpause();
		Screen.orientation = ScreenOrientation.Portrait;
		SceneManager.LoadScene("MainMenu");
	}
}
