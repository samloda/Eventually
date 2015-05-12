using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	public AudioSource myMusicSource; //Source for the ambient music
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;

	void Start()
	{
		SoundEvent (myMusicSource);
	}
}
