using UnityEngine;
using System.Collections;

public class ActionButtonScript : MonoBehaviour {

	public bool handOccupied = false; //Track if the player can use things
	public bool possibleUse = false; //Flag if the player is looking at a usable item
	public Transform pickupHandle; //Handle for the empty gameobject pickups are bound to

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0.0f)); //Declare a ray pointing out from the center of the camera view
		RaycastHit hit; //Raycasthit to get a reference to the targeted object
		Physics.Raycast (ray, out hit, 10f); //Raycast outward for 10 units
		//Use inheritance and polymorphism
		//to get whatever use script is on the hit object
		UseableBase usescript = hit.collider.gameObject.GetComponent<UseableBase> ();
		
		if (usescript == null && handOccupied == false) //If a use script is not found
						possibleUse = false; //change the flag to false
				else //Otherwise
						possibleUse = true; //Change the flag to true

		if (Input.GetKeyDown (KeyCode.E) && possibleUse == true) { //If the player hits the use key
						if (handOccupied) { //Check if the hand is occupied
								pickupHandle.GetChild (0).GetComponent<UseableObjectPickup> ().Use (this.gameObject); //Call the use function of what is held
								handOccupied = false;
						} else { //Otherwise
								usescript.Use (this.gameObject); //Call the use function
								if (usescript is UseableObjectPickup) //Check if the object activated was an object to pick up
										handOccupied = true; //If so, set the hand occupation to true
						}
				}
	}
}