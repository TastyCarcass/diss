using UnityEngine;
using System.Collections;

public class AddUnitToggle : MonoBehaviour 
{
	public ShadowFormation form;

	void Pressed()
	{
		form.SendMessage("AddUnitButtonPressed");
	}
}
