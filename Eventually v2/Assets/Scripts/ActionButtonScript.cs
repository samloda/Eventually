using UnityEngine;
using System.Collections;

public class ActionButtonScript : MonoBehaviour {

	public bool handOccupied = false; //Track if the player can use things
	public Transform pickupHandle; //Handle for the empty gameobject pickups are bound to

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) { //If the player hits the use key
						if (handOccupied) { //Check if the hand is occupied
								pickupHandle.GetChild(0).GetComponent<UseableBase> ().Use (this.gameObject); //Call the use function of what is held
								handOccupied = false;
						} else { //Otherwise
								Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0.0f)); //Declare a ray pointing out from the center of the camera view
								RaycastHit hit; //Raycasthit to get a reference to the targeted object
								if (Physics.Raycast (ray, out hit, 10f)) { //Raycast outward for 10 units
										//Use inheritance and polymorphism
										//to get whatever use script is on the hit object and call the use function
										UseableBase usescript = hit.collider.gameObject.GetComponent<UseableBase> ();
										usescript.Use (this.gameObject);

										if (usescript is UseableObjectPickup) //Check if the object activated was an object to pick up
												handOccupied = true; //If so, set the hand occupation to true
								}
						}
				}
	}
}