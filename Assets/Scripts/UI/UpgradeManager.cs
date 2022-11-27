using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeManager : MonoBehaviour
{
	public GameObject promptContainer;
    public GameObject prompt;
	public GameObject buttonPrefab;
    TowerBase prompting;
    StatManager statManager;

    private void Start() {
        statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
    }

    public void Prompt(TowerBase prompter) {
        prompting = prompter;

		List<UpgradeBase> possibleUpgrades = prompter.GetValidUpgrades();
		// Debug.Log(possibleUpgrades.Count);

		foreach(Transform child in prompt.transform) {
			Destroy(/* the */ child.gameObject); // Corrupt them all
		}

		int ctr = 0;
		// Debug.Log("Hi");
		foreach(UpgradeBase upgrade in possibleUpgrades) {
			GameObject buttonContainer = Instantiate(buttonPrefab, prompt.transform);
			Transform buyContainer = buttonContainer.transform.GetChild(2);

			TMP_Text descText = buttonContainer.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
			TMP_Text priceText = buyContainer.GetChild(2).GetComponent<TMP_Text>();

			descText.text = upgrade.id;
			priceText.text = upgrade.cost.ToString();

			Button button = buyContainer.GetComponent<Button>();
			button.onClick.AddListener(delegate{Selection(upgrade.id);});

			ctr++;
		}

		promptContainer.SetActive(true);
    }

    public void Selection(string upgradeId) {
		Debug.Log("Hello There!");
		UpgradeBase upgrade = prompting.GetValidUpgrades().Find(x => x.id == upgradeId);

		if (upgrade == null) {
            Debug.LogWarning("tf you doing??? key is invalid!!!");
            return;
        }

        int price = upgrade.cost;

        if (statManager.Money < price) {
            Debug.Log("no money plox");
            return;
        }

        statManager.ChangeMoney(-price);
		bool success = prompting.ApplyUpgrade(upgradeId);
		if(!success) {
			Debug.Log("something went wrong");
		}

        prompting = null;
        promptContainer.SetActive(false);
    }

	public void Close() {
		prompting = null;
        promptContainer.SetActive(false);
	}
}
