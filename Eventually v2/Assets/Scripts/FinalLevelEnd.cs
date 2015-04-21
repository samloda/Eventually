using UnityEngine;
using System.Collections;

public class FinalLevelEnd : MonoBehaviour {
	
	private bool preparing = false;
	
	void OnTriggerEnter(Collider other)
	{
		if (preparing == false && other.gameObject.tag == "Player") { //If the player enters the trigger zone
			Communicator.manager.PrepareEndGame (); //have the manager load the next level with that as the handicap value
			preparing = true;
		}
	}
}
