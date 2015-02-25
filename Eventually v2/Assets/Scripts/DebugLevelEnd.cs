using UnityEngine;
using System.Collections;

public class DebugLevelEnd : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (collider.gameObject.tag == "Player") {
						GameObject manager = GameObject.Find ("LevelManager");
						manager.GetComponent<LevelManager> ().LoadNextLevel ();
				}
	}
}
