using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
	public static void PlayClip(AudioClip clip) {
		GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(clip);
	}
}
