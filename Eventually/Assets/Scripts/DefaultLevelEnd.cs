using UnityEngine;
using System.Collections;

public class DefaultLevelEnd : MonoBehaviour {

	private bool preparing = false;

	void OnTriggerEnter(Collider other)
	{
		if (preparing == false && other.gameObject.tag == "Player") { //If the player enters the trigger zone
			//float handicap = 0f; //Create a variable handicap, handicap is how long the next level will wait before spawning the enemy
			//handicap = Vector3.Distance(Communicator.player.transform.position, Communicator.enemy.transform.position); //Calculate distance between player and enemy
			Communicator.manager.PrepareNextLevel (0f); //have the manager load the next level with that as the handicap value
			preparing = true;
		}
	}
}
