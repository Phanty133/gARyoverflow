using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenuManager : MonoBehaviour
{
	public string imgURL = "";
	public GameObject mainPage;
	public GameObject targetPage;
	public GameObject infoPage;

	private Coroutine dlCoroutine;

	public void OnStartGame() {
		mainPage.SetActive(false);
		targetPage.SetActive(true);
	}

	public void OnReady() {
		targetPage.SetActive(false);
		infoPage.SetActive(true);
	}

	public void OnUnderstand() {
		SceneManager.LoadScene("GameScene");
	}

	public void OnExit() {
		Application.Quit();
	}

	public void DownloadImage() {
		Application.OpenURL(imgURL);
	}
}
