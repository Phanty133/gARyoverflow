using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct TowerPrice {
    public string towerId;
    public int cost;
}

public class TowerPrompterManager : MonoBehaviour
{
    public static List<TowerPrice> TowerPrices = new List<TowerPrice>() {
        new TowerPrice() { towerId = "bullet", cost = 100 },
        new TowerPrice() { towerId = "laser", cost = 100 },
        new TowerPrice() { towerId = "bubble", cost = 100 }
    };
    public GameObject prompt;
    TowerPlacer prompting;
    StatManager statManager;

    private void Start() {
        statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
    }

    public static int? GetCost(string id) {
        var price = TowerPrompterManager.TowerPrices.FirstOrDefault(t => t.towerId == id);

        if (!price.Equals(default(TowerPrice))) {
            return price.cost;
        } else {
            return null;
        }
    }

    public void Prompt(TowerPlacer prompter) {
        prompting = prompter;

        prompt.SetActive(true);
    }

    public void ClosePrompt() {
        prompting = null;
        prompt.SetActive(false);
    }

    public void Selection(string tower) {
        int? priceNullable = TowerPrompterManager.GetCost(tower);

        if (priceNullable == null) {
            Debug.LogWarning("tf you doing??? key is invalid!!!");
            return;
        }

        int price = priceNullable.Value;

        if (statManager.Money < price) {
            Debug.Log("no money plox");
            return;
        }

        statManager.ChangeMoney(-price);
        prompting.PlaceTower(tower);

        prompting = null;
        prompt.SetActive(false);
    }
}
