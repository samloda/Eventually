using UnityEngine;
using System.Collections;

public class ActionButtonGUIScript : MonoBehaviour {
	private string reticle = " o "; //String for the reticle
	private ActionButtonScript myFlags; //Reference to the action button script with flags to use

	void Start()
	{
		myFlags = Communicator.player.GetComponent<ActionButtonScript> (); //Get a handle to the action button script
	}

	void OnGUI()
	{
		//x means the hand is occupied
		//o means the hand is not occupied
		//brackets means the use key can be pressed
		//no brackets means the use key can not be pressed
		if (myFlags.handOccupied) { //If the player is holding something
						GUI.contentColor = Color.red; //Turn the reticle red
						reticle = "[x]"; //Set the reticle to an x with brackets
				} else { //Otherwise
						if (myFlags.possibleUse) { //If the player can use what they are looking at
								GUI.contentColor = Color.green; //Turn the reticle green
								reticle = "[o]"; //Set the reticle to an o with brackets
						} else { //Otherwise
								GUI.contentColor = Color.white; //Turn the reticle default white
								reticle = " o "; //Set the reticle to default o without brackets
						}
				}


		GUI.Label (new Rect ((Screen.width / 2) - 1, (Screen.height / 2) - 1, 100f, 100f), reticle); //Display the reticle
	}
}

