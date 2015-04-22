using UnityEngine;
using System.Collections;

public class AIPathfinding : MonoBehaviour {

	public GameObject target; //A handle to the player for the navmesh agent to chase
	public NavMeshAgent agent; //Agent for pathfinding
	public LevelManager manager; //The level manager
	public AudioSource myGrowlSource; //Sound to play when the enemy kills the player
	public AudioSource myMoveSource; //Sound to play while the snail moves
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;

	public enum AIStatus //State machine values
	{
		Chasing = 0,
		Waiting = 1,
		Victory = 2
	}

	private AIStatus status = AIStatus.Chasing; //Instance of the enum for states
	private bool hasWon = false; //Boolean to detect if the player has been killed
	private int stepCoolDown = 0; //Cooldown for playing the steps

	// Use this for initialization
	void Awake () {
		Communicator.enemy = this.gameObject; //Set the comminicator's variable for the enemy
		target = Communicator.player; //Set the target to the player
		manager = Communicator.manager; //Set the manager to the level manager script
		agent = GetComponent<NavMeshAgent> (); //Get a handle to this object's agent
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckStatus (); //Function to set the ai state

		switch (status) { //Do a switch statement on update based on what status the object is in
				case AIStatus.Chasing: //If the object status is idle
						Chase (); //Call the idle function
						break;
				case AIStatus.Waiting: //If the object status is aggressive
						Wait (); //Call the chase function
						break;
				case AIStatus.Victory: //If the object status is combative
						if (myGrowlSource.isPlaying == false) { //If the sound hasn't already begun
								SoundEvent (myGrowlSource); //Play the growl sound
								Invoke ("Win", myGrowlSource.clip.length); //Attack the player
						}
						break;
				}
	}

	void CheckStatus()
	{
		if (hasWon == false) {
						agent.SetDestination (target.transform.position); //Handle for the player position as a destination
						if (Vector3.Distance (this.transform.position, target.transform.position) <= 2f) { //If the enemy gets within 1 unit of the player
								hasWon = true; //Change its win status to true
								agent.Stop();
						}
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
		MoveSound (); //Function to play a move sound effect periodically
		agent.Resume (); //Call the resume function in case the agent has been stopped by wait
	}

	void Wait()
	{
		agent.Stop (); //If the agent needs to wait, call the stop function
	}

	void Win()
	{
		manager.ReLoadLevel (); //Reload level due to player death
	}

	void MoveSound()
	{
		if (--stepCoolDown <= 0) { //Decriment the step cooldown and check if it is zero
						stepCoolDown += 30 + Random.Range (-10, 10); //reset cooldown with some variance
						SoundEvent(myMoveSource); //Call the event to play the sound
		}
	}
}
