using UnityEngine;
using System.Collections;

public class AddUnitButton : MonoBehaviour 
{
	bool NewUnitMode;

	public Camera mainCam;
	public Camera editCam;


	public void Pressed(InputEventArgs e)
	{
		Debug.Log ("unit button pressed");
		if(!NewUnitMode)
		{
			NewUnitMode = true;
		}
		else 
		{
			NewUnitMode = false; 
		}
	}

	// Use this for initialization
	void Start () 
	{
		NewUnitMode = false;
	}

	// Update is called once per frame
	void Update () 
	{
		//Mode Checks
		if(NewUnitMode)
		{
			if(mainCam.gameObject.active)
			{
				editCam.gameObject.SetActive (true);
				mainCam.gameObject.SetActive (false);
			}
		}
		else
		{
			if(editCam.gameObject.active)
			{
				editCam.gameObject.SetActive (false);
				mainCam.gameObject.SetActive (true);
			}
		}
	}
}
