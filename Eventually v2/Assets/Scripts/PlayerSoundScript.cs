using UnityEngine;
using System.Collections;

public class PlayerSoundScript : MonoBehaviour {

	public AudioSource myWalkSource; //Source for the  walking sound
	public AudioSource myJumpSource; //Source for the jumping sound
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;

	private int stepCoolDown = 0; //Cooldown for playing the steps
	
	// Update is called once per frame
	void Update () {
		//If the player is walking and the step cooldown has reached zero
		if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) && --stepCoolDown <= 0) { //stepCoolDown is decrimented right before its check
						stepCoolDown += 20 + Random.Range(-2, 2); //reset cooldown with some variance
						SoundEvent (myWalkSource); //Call the event and pass in the walk source
				}

		if (Input.GetButtonDown ("Jump")) { //If the player presses the jump button
						SoundEvent (myJumpSource); //Call the event and pass in the jump source
				}
	}
}
