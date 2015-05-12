using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public AudioSource myLoadSource; //Sound to play when loading the next level
	public GameObject enemyPrefab; //Used for spawning the player
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;
	public delegate void SoundStopper(); //Event for the manager to read
	public static event SoundStopper SoundStopperEvent;
	public int lastLevel = 0; //Integer to store the last level's index

	private int thisLevel = 0; //Variable to store the current level index
	private float handicap = 0f; //Variable to store the distance between player and enemy

	public void Start() //Initial loading
	{
		DontDestroyOnLoad (this.gameObject); //Make this persistent
		Communicator.manager = this; //Set a reference to this script
		thisLevel = Application.loadedLevel + 1; //Set thislevel to current level index + 1
		Application.LoadLevel (thisLevel); //Load the first level
		Invoke ("SpawnEnemy", handicap + .1f); //Call function to spawn the enemy after a certain time
	}
	
	public void PrepareNextLevel(float _handicap)
	{
		SoundStopperEvent (); //Stop all current sounds
		SoundEvent (myLoadSource); //Play the level shift sound
		handicap = _handicap;
		Invoke ("LoadNextLevel", myLoadSource.clip.length); //Load the next level once the sound has finished
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel (++thisLevel); //Load the next level by incrementing 'thislevel'
		Invoke ("SpawnEnemy", handicap + .1f); //Call function to spawn the enemy after the handicap time plus .1 (this is to make sure the next level has loaded first)
	}
	
	public void ReLoadLevel()
	{
		SoundStopperEvent ();
		Application.LoadLevel (thisLevel); //Reload the current level
		Invoke ("SpawnEnemy", handicap + .1f); //Call function to spawn the enemy after a certain time
	}

	public void PrepareEndGame()
	{
		SoundStopperEvent (); //Stop all sounds
		SoundEvent (myLoadSource); //Play the level shift sound
		Invoke ("LoadEndGame", myLoadSource.clip.length);
	}

	public void LoadEndGame()
	{
		Application.LoadLevel (lastLevel); //Load the end screen
		Communicator.manager = null; //Set the manager to null
		Destroy (this.gameObject); //Destroy the level manager
	}

	private void SpawnEnemy()
	{
		Instantiate (enemyPrefab, Communicator.enemySpawnPoint.position, Communicator.enemySpawnPoint.rotation); //Spawn the enemy at the proper point and store in the enemy variable
	}
}
