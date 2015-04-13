using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public AudioSource myLoadSource; //Sound to play when loading the next level
	public AudioSource myReloadSource; //sound to play when reloading the level
	public GameObject enemyPrefab; //Used for spawning the player
	public delegate void SoundAction(AudioSource source); //Event for the manager to read
	public static event SoundAction SoundEvent;

	private int thisLevel = 1; //Variable to store the current level index
	private float handicap = 0f; //Variable to store the distance between player and enemy

	public void Awake() //Initial loading
	{
		DontDestroyOnLoad (this.gameObject); //Make this persistent
		Communicator.manager = this; //Set a reference to this script
		Debug.Log ("Let's begin."); //Tell the player they have started the game
		Application.LoadLevel (thisLevel); //Load the first level
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	public void LoadNextLevel(float _handicap)
	{
		SoundEvent (myLoadSource); //Play the level shift sound
		handicap = _handicap;
		Debug.Log ("You Win! Now on to the next puzzle, hurry!"); //Tell the player they beat the level
		Application.LoadLevel (++thisLevel); //Load the next level by incrementing 'thislevel'
		Invoke ("SpawnEnemy", handicap + .1f); //Call function to spawn the enemy after the handicap time plus .1 (this is to make sure the next level has loaded first)
	}
	
	public void ReLoadLevel()
	{
		Debug.Log ("You Lose! You stayed still too long and it got you!"); //Tell the playe they died from the monster
		Application.LoadLevel (thisLevel); //Reload the current level
		Invoke ("SpawnEnemy", handicap + .1f); //Call function to spawn the enemy after a certain time
	}
	
	private void SpawnEnemy()
	{
		Instantiate (enemyPrefab, Communicator.enemySpawnPoint.position, Communicator.enemySpawnPoint.rotation); //Spawn the enemy at the proper point and store in the enemy variable
	}
}
