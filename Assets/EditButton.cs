using UnityEngine;
using System.Collections;

public class EditButton : MonoBehaviour
{
	public ShadowFormation form;

	public string ButtonType;

	void Pressed()
	{
		if (ButtonType == "AddUnit") 
		{
			form.SendMessage("AddUnitButtonPressed");
		}
		if (ButtonType == "DeleteUnit") 
		{
			form.SendMessage("DeleteUnitButtonPressed");
		}
		if (ButtonType == "Update") 
		{

		}
	}
}
