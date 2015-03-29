using UnityEngine;
using System.Collections;

public class AddUnitButton : MonoBehaviour 
{
	public Formation mainForm;
	public ShadowFormation shadowForm;

	public CameraManager CamMgr;

	public Camera mainCam;
	public Camera editCam;
	public GameObject mainGUI;
	public GameObject editGUI;

	public void Pressed(InputEventArgs e)
	{
		Debug.Log ("unit button pressed");
		if(!CamMgr.editMode)
		{
			CamMgr.ToggleCam();
			shadowForm.Import (true, mainForm.Export(true));
		}
		else 
		{
			CamMgr.ToggleCam();
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
