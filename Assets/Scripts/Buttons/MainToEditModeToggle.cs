using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainToEditModeToggle : MonoBehaviour 
{
	public Formation mainForm;
	public ShadowFormation shadowForm;

	public Text buttonText;
	public string onMainCamLabel = "To Edit Mode";
	public string onEditCamLabel = "To Main Mode (Cancel)";

	public CameraManager CamMgr;

	public void EditToMainAsNewFormation()
	{
		mainForm.Import (true, shadowForm.Export (true));
		ToggleCamView ();
	}

	public void EditToMainAsUpdateFormation()
	{
		mainForm.Import (false, shadowForm.Export (true));
		ToggleCamView ();
	}


	public void MainToEditAsFormation()
	{
		shadowForm.Import (true, mainForm.Export(true));
		ToggleCamView ();
	}

	void ToggleCamView()
	{
		if(!CamMgr.editMode)
		{
			CamMgr.ToggleCam();

			buttonText.text = onEditCamLabel;
		}
		else 
		{
			buttonText.text = onMainCamLabel;
			CamMgr.ToggleCam();
		}
	}
}
