using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class for creating custom enemy modifiers
public abstract class EnemyModifier : MonoBehaviour
{
	[HideInInspector]
	public Enemy self;

	public abstract void Init();
}
