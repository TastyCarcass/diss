using UnityEngine;
using System.Collections;
//Title: Camera Manager
//Purpose: Ensures that the correct cameras and GUIs are active throughout the program.
public class CameraManager : MonoBehaviour 
{
	public Camera mainCam;
	public Camera editCam;

	public GameObject mainGUI;
	public GameObject editGUI;


	public bool editMode = false;

	public void ToggleCam()
	{
		if (!editMode) 
		{
			//ACTIVATE EDIT MODE
			editMode = true;
		
			editCam.gameObject.SetActive (true);
			editGUI.gameObject.SetActive (true);
			mainCam.gameObject.SetActive (false);
			mainGUI.gameObject.SetActive (false);
		}
		else 
		{
			//DECTIVATE EDIT MODE
			editMode = false; 
		
			editCam.gameObject.SetActive (false);
			editGUI.gameObject.SetActive (false);
			mainCam.gameObject.SetActive (true);
			mainGUI.gameObject.SetActive (true);
		}
	}
	// Use this for initialization
	public void Initialise () 
	{
		//Default settings when the program is loaded.
		editMode = false;
		editCam.gameObject.SetActive (false);
		editGUI.gameObject.SetActive (false);
		mainCam.gameObject.SetActive (true);
		mainGUI.gameObject.SetActive (true);
	} // Initialise
} // Camera Manager
