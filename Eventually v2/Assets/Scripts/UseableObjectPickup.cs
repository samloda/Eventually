using UnityEngine;
using System.Collections;

public class UseableObjectPickup : UseableBase {

	private bool pickedUp = false;

	public override void Use (GameObject activator)
	{
		if (pickedUp) { //If the object has been picked up when used
			this.transform.parent = null; //Reset the parent
			this.rigidbody.isKinematic = false; //Reactivate rigidbody physics
			pickedUp = false; //Set picked up to false
				} else { //If the object has NOT been picked up when used
						//Handle to the transform that objects are snapped to when picked up
						Transform pickupHandle = activator.transform.GetChild (0).transform;

						this.transform.parent = pickupHandle; //Parent this to the handle
						this.transform.position = pickupHandle.position; //Set the position to match the handle
						this.transform.rotation = pickupHandle.rotation; //Set the rotation to match the handle

						this.rigidbody.isKinematic = true; //Deactivate rigidbody physics
						
						pickedUp = true; //Set the picked up value to true
				}
	}
}
