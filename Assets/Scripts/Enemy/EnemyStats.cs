using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	public int baseHealth = 10;
	public float speed = 3;
	public float healthModifier = 1f;

	public float EffectiveHealth {
		get { return baseHealth * healthModifier; }
	}
}
