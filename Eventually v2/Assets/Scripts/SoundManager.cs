using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	private List<AudioSource> activeSources = new List<AudioSource>();
	private List<AudioSource> ambienceSources = new List<AudioSource>();
	private AudioSource specialSource;
	private bool playSounds = true;

	void OnEnable()
	{
		PlayerSoundScript.SoundEvent += PlaySound;
		ActionButtonScript.SoundEvent += PlaySound;
		AIPathfinding.SoundEvent += PlaySound;
	}

	void OnDisable()
	{
		PlayerSoundScript.SoundEvent -= PlaySound;
		ActionButtonScript.SoundEvent -= PlaySound;
		AIPathfinding.SoundEvent -= PlaySound;
	}

	void PlaySound(AudioSource source)
	{
		if (playSounds && source.clip.isReadyToPlay) {
						source.Play ();
						activeSources.Add (source);
				}
	}

	void PlayAmbience(AudioSource source)
	{
		source.loop = true;
		if (playSounds && source.clip.isReadyToPlay) {
						source.Play ();
				}
		ambienceSources.Add(source);
	}

	void PlaySpecialSound(AudioSource source)
	{
		specialSource = source;
		specialSource.Play ();
		playSounds = false;
		UseList (activeSources, 1);
		UseList (ambienceSources, 2);
	}

	void Update()
	{
		if (playSounds) {
						if (activeSources.Count != 0) {
								List<AudioSource> inactiveSources = new List<AudioSource> ();
								foreach (AudioSource activeSource in activeSources) {
										if (activeSource.isPlaying == false) {
												inactiveSources.Add (activeSource);
										}
								}
								foreach (AudioSource inactiveSource in inactiveSources) {
										activeSources.Remove (inactiveSource);
								}
						}
				} else {
						if (specialSource.isPlaying == false) {
								specialSource = null;
								playSounds = true;
								UseList(ambienceSources, 0);
						}
				}
	}

	void UseList(List<AudioSource> sources, int play)
	{
		foreach (AudioSource source in sources) {
						switch (play) {
						case 0: //Play
								if (source.clip.isReadyToPlay)
										source.Play ();
								break;
						case 1: //Stop
								source.Stop ();
								break;
						case 2: //Pause
								source.Pause ();
								break;
						}
				}
				
	}
}
