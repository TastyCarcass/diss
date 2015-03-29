using UnityEngine;
using System.Collections;

public class AddUnitButton : MonoBehaviour 
{
	bool NewUnitMode;

	public Camera mainCam;
	public Camera editCam;
	public GameObject mainGUI;
	public GameObject editGUI;

	public void Pressed(InputEventArgs e)
	{
		Debug.Log ("unit button pressed");
		if(!NewUnitMode)
		{
			NewUnitMode = true;

			editCam.gameObject.SetActive (true);
			editGUI.gameObject.SetActive (true);
			mainCam.gameObject.SetActive (false);
			mainGUI.gameObject.SetActive (false);
		}
		else 
		{
			NewUnitMode = false; 

			editCam.gameObject.SetActive (false);
			editGUI.gameObject.SetActive (false);
			mainCam.gameObject.SetActive (true);
			mainGUI.gameObject.SetActive (true);
		}
	}

	// Use this for initialization
	public void Initialise () 
	{
		NewUnitMode = false;
		editCam.gameObject.SetActive (false);
		editGUI.gameObject.SetActive (false);
		mainCam.gameObject.SetActive (true);
		mainGUI.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
