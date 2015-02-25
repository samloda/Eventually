using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject enemyPrefab; //Used for spawning the player
	public GameObject playerPrefab; //Used for spawning the enemy
	
	private int thisLevel = 1; //Variable to store the current level index
	private float handicap = 0f; //Variable to store the distance between player and enemy
	private GameObject player; //Handle to the player
	private GameObject enemy; //Handle to the enemy
	
	public void Awake() //Initial loading
	{
		DontDestroyOnLoad (this.gameObject);
		Debug.Log ("Let's begin."); //Tell the player they have started the game
		Application.LoadLevel (thisLevel); //Load the next level by incrementing 'thislevel'
		SpawnPlayer (); //Call function to spawn the player
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	public void LoadNextLevel()
	{
		Debug.Log ("You Win! Now on to the next puzzle, hurry!"); //Tell the player they beat the level
		handicap = Vector3.Distance (player.transform.position, enemy.transform.position); //Calculate and store the distance between player and enemy before level change
		Application.LoadLevel (++thisLevel); //Load the next level by incrementing 'thislevel'
		SpawnPlayer (); //Call function to spawn the player
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	public void ReLoadLevel()
	{
		Debug.Log ("You Lose! You stayed still too long and it got you!"); //Tell the playe they died from the monster
		Application.LoadLevel (thisLevel); //Reload the current level
		SpawnPlayer (); //Call function to spawn the player
		Invoke ("SpawnEnemy", handicap); //Call function to spawn the enemy after a certain time
	}
	
	private void SpawnPlayer()
	{
		GameObject playerSpawnPoint = GameObject.Find ("PlayerSpawn"); //Find the player spawn point and save it
		player = (GameObject)Instantiate (playerPrefab, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation); //Spawn the player at the proper point and store in the player variable
	}
	
	private void SpawnEnemy()
	{
		GameObject enemySpawnPoint = GameObject.Find ("EnemySpawn"); //Find the enemy spawn point and save it
		enemy = (GameObject)Instantiate (enemyPrefab, enemySpawnPoint.transform.position, enemySpawnPoint.transform.rotation); //Spawn the enemy at the proper point and store in the enemy variable
		enemy.GetComponent<AIPathfinding> ().manager = this; //Apply the manager to the pathfinding script so it can call reloadlevel
	}
}
