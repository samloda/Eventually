﻿using UnityEngine;
using System.Collections;

public class HintScript : MonoBehaviour {
	private GameObject enemy; //Handle to the enemy
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if (Communicator.enemy != null) {
						if (Vector3.Distance (Communicator.enemy.transform.position, this.transform.position) < 100)
								this.gameObject.GetComponent<MeshRenderer> ().enabled = true;
						else
								this.renderer.enabled = false;
				}
	}
}
