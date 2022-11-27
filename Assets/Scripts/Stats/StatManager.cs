using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
	public delegate void HealthChange(int amount);
	public event HealthChange OnHealthChange;

	public delegate void MoneyChange(int amount);
	public event MoneyChange OnMoneyChange;

	public int Health {
		get { return health; }
		private set { health = value; }
	}

	public int Money {
		get { return money; }
		private set { money = value; }
	}

	[SerializeField]
	private int health = 100;
	[SerializeField]
	private int money = 420;

	public void DecreaseHealth(int amount) {
		health -= amount;
		OnHealthChange(amount);
	}

	public void ChangeMoney(int amount) {
		money += amount;
		OnMoneyChange(amount);
	}
}
