using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GUISkin MyGUISkin; //Gui skin can be set in the editor
	public Texture2D Background; //Texture to be the background of the main menu
	public Texture2D Logo; //Logo to display above the menu
	public GameObject levelManager; //The level manager to activate on play

	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200); //Size of the main menu

	private string menuState; //Menustate tracks what menu to display

	private string main = "main"; //string of the main menu
	private string options = "options"; //String of the options menu

	private float volume = 1.0f; //Volume for sound effects
	private float ambienceVolume = 1.0f; //Volume for music

	// Use this for initialization
	void Start () 
	{
		menuState = main; //Start the menu on main
	}

	private void OnGUI()
	{
		if (Background != null)
		{
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Background); //Draw the background
		}

		if (Logo != null)
		{
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), Logo); //Draw the logo
		}

		GUI.skin = MyGUISkin; //Set the guiskin to what was fed in

		// Check UI State
		if (menuState == main)
		{
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu"); //If on the main menu, draw the window and call the menu function
		}

		if (menuState == options)
		{
			WindowRect = GUI.Window(1, WindowRect, optionsFunc, "Options"); //If on the options menu, draw the window and call the options menu function
		}
	}

	private void menuFunc (int id)
	{
		// buttons
		if (GUILayout.Button("Play Game")) //On the main menu, have a button for playing the game
		{
			levelManager.SetActive(true); //That activates the level manager and begins
		}

		if (GUILayout.Button("Options")) //Have a button for options menu
		{
			menuState = options; //Which navigates to the options menu
		}

		if (GUILayout.Button("Quit Game")) //Have a button to quit the game
		{
			Application.Quit(); //Which closes the application
		}
	}

	private void optionsFunc (int id)
	{
		GUILayout.Box("Volume"); //Draw the box for the menu
		GUILayout.Label ("Effects"); //Label effects for effects volume
		volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f); //Horizontal slider sets the volume
		GUILayout.Label ("Music"); //Label music for music volume
		ambienceVolume = GUILayout.HorizontalSlider(ambienceVolume, 0.0f, 1.0f); //Horizontal slider sets the music volume

		SoundManager.effectsVolume = volume; //Set the soundmanager effects volume
		SoundManager.ambienceVolume = ambienceVolume; //Set the sound manager ambience volume

		if (GUILayout.Button("Back To Main Menu")) //Draw a button to return to main menu
		{
			menuState = main; //That sets menustate to main
		}
	}
}
