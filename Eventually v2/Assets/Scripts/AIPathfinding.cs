using UnityEngine;
using System.Collections;

public class AIPathfinding : MonoBehaviour {

	public static GameObject target; //A handle to the player for the navmesh agent to chase
	public NavMeshAgent agent;

	public enum AIStatus //State machine values
	{
		Chasing = 0,
		Waiting = 1,
		Victory = 2
	}

	private AIStatus status = AIStatus.Chasing; //Instance of the enum for states
	private bool hasWon = false; //Boolean to detect if the player has been killed

	// Use this for initialization
	void Start () {
		if (target == null) //conditional statement to make sure gameobject.find is only used once
						target = GameObject.FindGameObjectWithTag ("Player"); //Set the player handle to target
		agent = GetComponent<NavMeshAgent> (); //Get a handle to this object's agent
	}
	
	// Update is called once per frame
	void Update () {
		CheckStatus (); //Function to set the ai state

		switch (status) { //Do a switch statement on update based on what status the object is in
		case AIStatus.Chasing: //If the object status is idle
			Chase (); //Call the idle function
			break;
		case AIStatus.Waiting: //If the object status is aggressive
			Wait(); //Call the chase function
			break;
		case AIStatus.Victory: //If the object status is combative
			Win(); //Attack the player
			break;
		}
	}

	void CheckStatus()
	{
		agent.SetDestination (target.transform.position); //Handle for the player position as a destination
		if (Vector3.Distance (this.transform.position, target.transform.position) <= 1f) { //If the enemy gets within 1 unit of the player
						hasWon = true; //Change its win status to true
				}

		//Conditional statement uses short circuit evaluation to only chase or wait if the player is still alive
		if (hasWon) { //If the player has been defeated
						status = AIStatus.Victory; //Change to victory state
				} else if (agent.hasPath) { //If the player has not been defeated and a path exists
						status = AIStatus.Chasing; //Chase after them
				} else { //If there is no path and the player has not been defeated
						status = AIStatus.Waiting; //Wait
				}
	}

	void Chase()
	{
		agent.Resume (); //Call the resume function in case the agent has been stopped by wait
	}

	void Wait()
	{
		agent.Stop (false); //If the agent needs to wait, call the stop function

		/* AIDIFFICULTY GETS UPDATED HERE. THE AI WILL INCREMENTALLY INCREASE IN POWER */
	}

	void Win()
	{
		//DEBUG LOGIC, WILL BE REPLACED
		Debug.Log ("You lose!"); //Print loss statement
		Application.LoadLevel ("MainScene"); //Reload level
		/* PLAYERDEATH GETS UPDATED HERE.
		 * THE GUI WILL INFORM THE PLAYER OF THEIR LOSS
		 * AND ANY LAST FUNCTIONS WILL BE CALLED BEFORE LEVEL RESET */
	}
}
