using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScanUI : MonoBehaviour
{
	public void OnTargetFound() {
		gameObject.SetActive(false);
	}
}
