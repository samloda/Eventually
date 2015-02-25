using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject enemyPrefab; //Used for spawning the player

	private int thisLevel = 1; //Variable to store the current level index
	private float handicap = 5f; //Variable to store the distance between player and enemy

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
		handicap = _handicap;
		Debug.Log ("You Win! Now on to the next puzzle, hurry!"); //Tell the player they beat the level
		Application.LoadLevel (++thisLevel); //Load the next level by incrementing 'thislevel'
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	public void ReLoadLevel()
	{
		Debug.Log ("You Lose! You stayed still too long and it got you!"); //Tell the playe they died from the monster
		Application.LoadLevel (thisLevel); //Reload the current level
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	private void SpawnEnemy()
	{
		GameObject enemySpawnPoint = GameObject.Find ("EnemySpawn"); //Find the enemy spawn point and save it
		Instantiate (enemyPrefab, enemySpawnPoint.transform.position, enemySpawnPoint.transform.rotation); //Spawn the enemy at the proper point and store in the enemy variable
	}
}
