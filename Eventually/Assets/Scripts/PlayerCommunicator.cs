using UnityEngine;
using System.Collections;

public class PlayerCommunicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Communicator.player = this.gameObject; //Set the communicator player variable
	}
}
