using UnityEngine;
using System.Collections;

public static class Communicator{ //Class with handles to important objects and scripts

	public static GameObject player; //The player
	public static GameObject enemy; //The enemy
	public static LevelManager manager; //The level manager
	public static Transform enemySpawnPoint; //The spawn point of the enemy, changes between levels
}
