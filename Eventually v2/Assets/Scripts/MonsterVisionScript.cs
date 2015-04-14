using UnityEngine;
using System.Collections;

public class MonsterVisionScript : MonoBehaviour {

	public Camera playerCam; //Handle for player camera
	public Camera monsterCam; //Hadle for monster camera
	public bool inZone = false; //Boolean to detect if player is in the trigger zone
	public AudioSource myVisionSource; //Source for the monster vision sound effect
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;

	// Update is called once per frame
	void Update () {
		if (inZone) { //If the player is in the trigger zone
						if (Input.GetKeyDown (KeyCode.M)) { //if they press down the m key
								SoundEvent(myVisionSource); //Play the non-diagetic sound for 
								SetCams (); //Set Cameras
								playerCam.enabled = false; //Set the camera to the monster cam
								monsterCam.enabled = true;
						} else if (Input.GetKeyUp (KeyCode.M)) { //If they lift it up
								playerCam.enabled = true; //Set camera back
								monsterCam.enabled = false;

						}
				}
	}

	void SetCams()
	{
		playerCam = Communicator.player.transform.GetChild (1).gameObject.GetComponent<Camera> (); //Get the camera from the child object of the player
		monsterCam = Communicator.enemy.transform.GetChild (0).gameObject.GetComponent<Camera> (); //Get the camera from the child object of the enemy
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") { //If the object entering the zone is the player
						inZone = true; //Set boolean to true
				}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player") { //If the object exiting the zone is the player
						inZone = false; //Set boolean to false
						playerCam.enabled = true; //Also, reset the cameras just in case they are still holding the key
						monsterCam.enabled = false;
				}
	}
}
