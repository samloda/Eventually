using UnityEngine;
using System.Collections;

public class MonsterVisionScript : MonoBehaviour {

	private Camera playerCam; //Handle for player camera
	private Camera monsterCam; //Hadle for monster camera
	private bool inZone = false; //Boolean to detect if player is in the trigger zone

	// Use this for initialization
	void Start () {
		Camera[] cameras = Camera.allCameras; //Get a reference to all the cameras
		playerCam = Camera.main; //Get the main camera as the player camera

		for (int ii = 0; ii < Camera.allCamerasCount; ii++) { //Iterate through all the cameras
			if (cameras[ii] != Camera.main) //If the camera isn't the main camera
				monsterCam = cameras[ii]; //Set it as the monster camera
				}
		monsterCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inZone) { //If the player is in the trigger zone
						if (Input.GetKeyDown (KeyCode.M)) { //if they press down the m key
								playerCam.enabled = false; //Set the camera to the monster cam
								monsterCam.enabled = true;
						} else if (Input.GetKeyUp (KeyCode.M)) { //If they lift it up
								playerCam.enabled = true; //Set camera back
								monsterCam.enabled = false;
						}
				}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") //If the object entering the zone is the player
						inZone = true; //Set boolean to true
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
