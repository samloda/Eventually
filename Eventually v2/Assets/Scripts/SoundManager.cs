using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public static float effectsVolume;
	public static float ambienceVolume;

	private List<AudioSource> activeSources = new List<AudioSource>(); //List of the sources currently playing a sound effect
	private List<AudioSource> ambienceSources = new List<AudioSource>(); //List of the sources playing ambient sound
	private AudioSource specialSource; //Audio source playing a special sound like the mario star
	private bool playSounds = true; //Whether the manager should play new sounds

	void OnEnable()
	{
		PlayerSoundScript.SoundEvent += PlaySound; //On enable, subscribe to all events
		ActionButtonScript.SoundEvent += PlaySound;
		AIPathfinding.SoundEvent += PlaySound;
		LevelManager.SoundEvent += PlaySound;
		LevelManager.SoundStopperEvent += StopAll;
		MonsterVisionScript.SoundEvent += PlaySpecialSound;
		MusicPlayerScript.SoundEvent += PlayAmbience;
	}

	void OnDisable()
	{
		PlayerSoundScript.SoundEvent -= PlaySound; //On disable, unsubscribe from all events
		ActionButtonScript.SoundEvent -= PlaySound;
		AIPathfinding.SoundEvent -= PlaySound;
		LevelManager.SoundEvent -= PlaySound;
		LevelManager.SoundStopperEvent -= StopAll;
		MonsterVisionScript.SoundEvent -= PlaySpecialSound;
		MusicPlayerScript.SoundEvent -= PlayAmbience;
	}

	void PlaySound(AudioSource source) //Function plays a sound, takes in an audio source
	{
		if (playSounds && source.clip.isReadyToPlay) { //If the manager can play sounds and the clip is ready to play
						source.volume = effectsVolume; //Set the volume of the source to the global volume setting
						source.Play (); //Play the sound
						activeSources.Add (source); //Add it to active sources
				}
	}

	void PlayAmbience(AudioSource source) //Function plays ambient sound, takes in an audio source
	{
		source.volume = ambienceVolume; //Set the volume of the source to the global music volume setting
		source.loop = true; //Sets the source to loop
		if (playSounds && source.clip.isReadyToPlay) { //If the manager can play sounds and the clip is ready to play
						source.Play (); //Play the sound
				}
		ambienceSources.Add (source); //Regardless, add it. This covers anything that begins while a special sound is playing

	}

	void PlaySpecialSound(AudioSource source) //Function to play special sound that overwrites all others, takes in an audio source
	{
		playSounds = false; //Tell the manager to stop playing sounds
		source.volume = effectsVolume; //Set the volume of the source to the global volume setting
		source.Play (); //Play the sound
		UseList (activeSources, 1); //Stop all active sound effects like footsteps
		UseList (ambienceSources, 2); //Pause all the ambience sources
		specialSource = source; //Set the special source to this source
	}

	void Update()
	{
		if (playSounds) { //Check if the manager should play sounds
						if (activeSources.Count != 0) { //And if there are any active sound sources
								List<AudioSource> inactiveSources = new List<AudioSource> (); //Make a temporary list to store inactive sources
								foreach (AudioSource activeSource in activeSources) { //Iterate through the active sources
										if (activeSource.isPlaying == false) { //If they are not playing
												inactiveSources.Add (activeSource); //Queue them for removal
										}
								}
								foreach (AudioSource inactiveSource in inactiveSources) { //Iterate through each inactive source
										activeSources.Remove (inactiveSource); //Remove it from the active sources
								}
						}
				} else { //If the manager should not be playing sounds
						if (specialSource.isPlaying == false) { //Check if the special source is finished playing
								specialSource = null; //If so, set the special source in the manager to null
								playSounds = true; //And play sounds again
								UseList(ambienceSources, 0); //And resume playing all the ambience sources
						}
				}
	}

	void UseList(List<AudioSource> sources, int play) //Function that can pause stop or play a list of audio sources
	{
		foreach (AudioSource source in sources) { //Iterate through all sources in the list
						switch (play) { //Use the int to decide what to do
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

	void StopAll() //Function to stop and reset the manager from scratch
	{
		if (activeSources.Count != 0) //If there are any active sources
						UseList (activeSources, 1); //Stop them
		if (ambienceSources.Count != 0) //If there are any ambience sources
						UseList (ambienceSources, 1); //Stop them
		if (specialSource != null) //If there is a special source
						specialSource.Stop (); //Stop it

		activeSources.Clear (); //And clear all existing references
		ambienceSources.Clear ();
		specialSource = null;

		playSounds = true; //Set playsounds to true in case there was a special sound
	}
}
