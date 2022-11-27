using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class TowerEntry
{
	public string key;
	public GameObject value;
}

public class TowerPlacer : MonoBehaviour
{
    public bool isAvailable = false;

    public List<TowerEntry> possibleTowers = new List<TowerEntry>();

    Transform towerContainer; // Secure, Contain, Protect
    StatManager statManager;

    void Start() {
        towerContainer = GameObject.FindGameObjectWithTag("TowerContainer").transform;
        statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
    }

    public void PlaceTower(string id) {
        GameObject tower = Instantiate(possibleTowers.Find(x => x.key == id).value, towerContainer);
        tower.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
        tower.transform.position = transform.position;

        isAvailable = false;
        Destroy(gameObject);
    }

    public void PromptTower() {
        GameObject.FindGameObjectWithTag("TowerPrompterManager").GetComponent<TowerPrompterManager>().Prompt(this);
    }
}
