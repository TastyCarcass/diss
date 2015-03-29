using UnityEngine;
using System.Collections;

public class UpdateFormation : MonoBehaviour 
{
	//Save Formation of original
	public ShadowFormation shadowForm;
	public Formation mainForm;


	void Pressed()
	{
		mainForm.Import (true, shadowForm.Export(true));
	}
}
