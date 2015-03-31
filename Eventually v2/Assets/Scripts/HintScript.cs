using UnityEngine;
using System.Collections;

public class HintScript : MonoBehaviour {
	private GameObject enemy; //Handle to the enemy
	// Use this for initialization
	void Start()
	{
		this.renderer.enabled = false; //Disable the renderer to start
	}
	
	// Update is called once per frame
	void Update () {
		if (Communicator.enemy != null) { //Test if the enemy exists
						if (Vector3.Distance (Communicator.enemy.transform.position, this.transform.position) < 25f) //If so, find the distance between enemy and hint
								this.gameObject.GetComponent<MeshRenderer> ().enabled = true; //If it is close enough, enable the renderer
						else
								this.renderer.enabled = false; //If not, disable the renderer
				}
	}
}
