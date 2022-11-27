using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
	public List<GameObject> trackObjs;

	[HideInInspector]
	public List<Track> tracks;

	private void Start() {
		foreach (GameObject obj in trackObjs) {
			tracks.Add(obj.GetComponent<Track>());
		}
	}
}
