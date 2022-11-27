using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
	public GameObject healthObj;
	public GameObject moneyObj;

	TMP_Text healthText;
	TMP_Text moneyText;
	StatManager statManager;

	private void Start() {
		healthText = healthObj.GetComponent<TMP_Text>();
		moneyText = moneyObj.GetComponent<TMP_Text>();
		statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();

		statManager.OnHealthChange += OnHealthChange;
		statManager.OnMoneyChange += OnMoneyChange;

		// Load initial values
		OnHealthChange(0);
		OnMoneyChange(0);
	}

	void OnHealthChange(int amount) {
		healthText.text = statManager.Health.ToString();
	}

	void OnMoneyChange(int amount) {
		moneyText.text = statManager.Money.ToString();
	}
}
