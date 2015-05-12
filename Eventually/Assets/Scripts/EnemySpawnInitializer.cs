using UnityEngine;
using System.Collections;

public class EnemySpawnInitializer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Communicator.enemySpawnPoint = this.transform; //Initializes the spawn point in the communicator for the level manager
	}
}
