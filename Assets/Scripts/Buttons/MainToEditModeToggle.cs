using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainToEditModeToggle : MonoBehaviour 
{
	public Formation mainForm;
	public ShadowFormation shadowForm;

	public Text buttonText;
	public string onMainCamLabel = "To Edit Mode";
	public string onEditCamLabel = "To Main Mode";

	public CameraManager CamMgr;


	public void OnClick()
	{
		if(!CamMgr.editMode)
		{
			CamMgr.ToggleCam();
			shadowForm.Import (true, mainForm.Export(true));
			buttonText.text = onEditCamLabel;
		}
		else 
		{
			buttonText.text = onMainCamLabel;
			CamMgr.ToggleCam();
		}
	}
}
