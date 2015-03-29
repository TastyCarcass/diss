using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	public Camera mainCam;
	public Camera editCam;

	public GameObject mainGUI;
	public GameObject editGUI;


	public bool editMode = false;

	public void ToggleCam()
	{
		if (!editMode) {
			editMode = true;
		
			editCam.gameObject.SetActive (true);
			editGUI.gameObject.SetActive (true);
			mainCam.gameObject.SetActive (false);
			mainGUI.gameObject.SetActive (false);
		} else {
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
		editMode = false;
		editCam.gameObject.SetActive (false);
		editGUI.gameObject.SetActive (false);
		mainCam.gameObject.SetActive (true);
		mainGUI.gameObject.SetActive (true);
	}
}
