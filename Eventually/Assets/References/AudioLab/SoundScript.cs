using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip audioClip;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = this.gameObject.AddComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.PlayOneShot (audioClip);
	}
}
