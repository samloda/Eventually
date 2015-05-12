using UnityEngine;
using System.Collections;

public class UseableObjectPickup : UseableBase {

	private bool pickedUp = false;
	private Color opaque;
	private Color transparent;
	private Color overlap;

	void Start()
	{
		opaque = this.renderer.material.color; //Default color
		transparent = new Color(opaque.r, opaque.g, opaque.b, .5f); //color with 50% transparency
		overlap = new Color (1f, transparent.g, transparent.b, transparent.a); //Transparent color with red hue
	}

	public override void Use (GameObject activator)
	{
		if (pickedUp) { //If the object has been picked up when used
						this.transform.parent = null; //Reset the parent
						pickedUp = false; //Set picked up to false

						//When set down, re enable independent movement and collision detection and turn the object opaque
						this.rigidbody.isKinematic = false;
						this.collider.isTrigger = false;
						this.renderer.material.color = opaque;
						this.gameObject.GetComponent<NavMeshObstacle> ().enabled = true;

				} else { //If the object has NOT been picked up when used
						//Handle to the transform that objects are snapped to when picked up
						Transform pickupHandle = activator.GetComponent<ActionButtonScript> ().pickupHandle;
						
						//When picked up, disable object collisions and movement and turn it transparent
						this.rigidbody.isKinematic = true;
						this.collider.isTrigger = true;
						this.renderer.material.color = transparent;
						this.gameObject.GetComponent<NavMeshObstacle> ().enabled = false;

						this.transform.parent = pickupHandle; //Parent this to the handle
						this.transform.position = pickupHandle.position; //Set the position to match the handle
						this.transform.rotation = pickupHandle.rotation; //Set the rotation to match the handle
						
						pickedUp = true; //Set the picked up value to true
				}
	}

	public void Update()
	{
		if (pickedUp) { //If the object is picked up
						if (Input.GetAxis ("Mouse ScrollWheel") > 0) { //Check scroll wheel up
								this.transform.localPosition += Vector3.up; //Move up one unit
						} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) { //Check scroll wheel down
								this.transform.localPosition += Vector3.down; //Move down one unit
						} else if (Input.GetButtonDown ("Fire1")) { //Check left mouse click
								this.transform.localPosition += Vector3.forward; //Move left one unit
						} else if (Input.GetButtonDown ("Fire2")) { //Check right mouse click
								this.transform.localPosition += Vector3.back; //Move right one unit
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (pickedUp && other.gameObject.tag != "Player") //When the held object is overlapping an object not the player
						this.renderer.material.color = overlap; //Change its color to the transparent red
	}

	void OnTriggerExit(Collider other)
	{
		if (pickedUp && other.gameObject.tag != "Player") //When the held object is not overlapping anything other than the player
						this.renderer.material.color = transparent; //Turn it transparent
	}
}
