using UnityEngine;
using System.Collections;

public class DebugLevelEnd : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player") { //If the player enters the trigger zone
			Debug.Log("You win!"); //Print win statement
			Application.LoadLevel("MainScene"); //Reload level
				}
	}
}
