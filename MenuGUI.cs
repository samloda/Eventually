using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuGUI : MonoBehaviour {

	public GUISkin myGUISkin;
	public Texture2D Background;
	public Texture2D Logo;

	public string[] creditsTextLines = new string[0];

	public Rect WindowRect = new Rect ((Screen.width * .5f) - 100f,1f,1f,1f);

	private string menuState;

	private string textToDisplay = "Credits\n";
	private float volume = 1.0f;

	void Start()
	{
		menuState = main;

		for (int ii = 0; ii < creditsTextLines.Length; ii++) {
			textToDisplay += creditsTextLines[ii] + "\n";
				}
		textToDisplay += "Press Esc to go back";
	}

	void OnGUI() {

		if (menuState == SendMessageOptions) {

				}
		if (menuState == credits) {
			GUI.Box (new Rect(0.0f,0.0f,Screen.width,Screen.height), textToDisplay);
				}
	{

	private void optionsFunc(int id)
	{
		GUILayout.Box ("Volume");
		volume = GUILayout.HorizontalSlider (volume, 0.0f, 1.0f);
	}
}