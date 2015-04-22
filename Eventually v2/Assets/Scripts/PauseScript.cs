using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public MouseLook cameraControl1; //scripts to disable on pause
	public MouseLook cameraControl2;
	public ActionButtonScript actionButton;
	public ActionButtonGUIScript actionGUI;

	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200); //Size of the main menu
	
	private string menuState; //Menustate tracks what menu to display
	
	private string main = "main"; //string of the main menu
	private string options = "options"; //String of the options menu
	
	private float volume = 1.0f; //Volume for sound effects
	private float ambienceVolume = 1.0f; //Volume for music

	private bool paused = false; //Boolean for if the game is paused

	// Use this for initialization
	void Start () 
	{
		menuState = main; //Start the menu on main
	}
	
	private void OnGUI()
	{
		if (paused) { //if paused
						// Check UI State
						if (menuState == main) {
								WindowRect = GUI.Window (0, WindowRect, menuFunc, "Main Menu"); //If on the main menu, draw the window and call the menu function
						}
		
						if (menuState == options) {
								WindowRect = GUI.Window (1, WindowRect, optionsFunc, "Options"); //If on the options menu, draw the window and call the options menu function
						}
				}
	}
	
	private void menuFunc (int id)
	{
		// buttons
		if (GUILayout.Button("Resume")) //On the main menu, have a button for resuming the game
		{
			PauseToggle(); //Unpause the game
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

	private void PauseToggle() //function handles pausing and unpausing
	{
		if (paused) { //If paused
						cameraControl1.enabled = true; //Unpause and re enable scripts
						cameraControl2.enabled = true;
						actionButton.enabled = true;
						actionGUI.enabled = true;
						Time.timeScale = 1.0f;
				} else { //If unpaused
						cameraControl1.enabled = false; //Pause and disable script
						cameraControl2.enabled = false;
						actionButton.enabled = false;
						actionGUI.enabled = false;
						Time.timeScale = 0.0f;
				}

		paused = !paused; //Reset bool
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) //Toggle pause on escape
						PauseToggle ();
	}
}
