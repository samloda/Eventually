using UnityEngine;
using System.Collections;

public class HintScript : MonoBehaviour {
	private GameObject enemy; //Handle to the enemy
	// Use this for initialization
	void Start () {
		GameObject enemy = GameObject.Find ("Enemy");

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (enemy.transform.position, this.transform.position) < 100)
						this.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				else
						this.renderer.enabled = false;
	}
}
