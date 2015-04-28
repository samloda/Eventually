using UnityEngine;
using System.Collections;

public class UIManagerEndScreen : MonoBehaviour {
	
	public GUISkin MyGUISkin; //Gui skin can be set in the editor
	public Texture2D Background; //Texture to be the background of the main menu
	public Texture2D Logo; //Logo to display above the menu

	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200); //Size of the main menu

	private string menuState; //Menustate tracks what menu to display

	public string[] CreditsTextLines = new string[0]; //Credits text fed in through the editor by line
	
	private string main = "main"; //string of the main menu
	private string credits = "credits"; //String of the credits menu

	private string textToDisplay = "Credits \n"; //Text to display on credits screen, starts with a header
	
	// Use this for initialization
	void Start () 
	{
		menuState = main; //Start the menu on main
		
		for (int x = 0; x < CreditsTextLines.Length; x++) //Iterate through the array of credits text
		{
			textToDisplay += CreditsTextLines[x] + " \n "; //Add each entry as a new line
		}
		textToDisplay += "Press Space To Go Back"; //Finish with instruction on how to get back
	}
	
	private void OnGUI()
	{
		if (Background != null)
		{
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Background); //Display the background texture
		}
		
		if (Logo != null)
		{
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), Logo); //Display the logo texture
		}
		
		GUI.skin = MyGUISkin; //Set the gui skin
		
		// Check UI Statea
		if (menuState == main)
		{
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "You Win!"); //If menu state is main, draw the menu window with the header telling the player they won
		}

		if (menuState == credits)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), textToDisplay); //If in the credits screen, draw the box for the credits and display credits text
		}
	}
	
	private void menuFunc (int id)
	{
		// buttons
		if (GUILayout.Button("Restart Game?")) //Button to restart game
		{
			Application.LoadLevel(0); //Sends player back to the start screen
		}
		
		if (GUILayout.Button("Credits")) //Button to show credits
		{
			menuState = credits; //Changes menustate to credits
		}
		
		if (GUILayout.Button("Quit Game")) //Button to quit game
		{
			Application.Quit(); //Closes the application
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (menuState == credits && Input.GetKey(KeyCode.Space)) //If the player is on the credits screen and hits escape
		{
			menuState = main; //Set the menustate to main
		}
	}
}
