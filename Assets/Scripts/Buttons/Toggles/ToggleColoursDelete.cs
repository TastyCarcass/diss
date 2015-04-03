using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ToggleColoursDelete: MonoBehaviour 
{
	public ShadowFormation frm;
	// Update is called once per frame
	void Update () 
	{
		if(frm.deleteUnitsMode)
		{
			GetComponent<Image>().color = Color.grey;
		}
		else
		{
			GetComponent<Image>().color = Color.white;
		}
	}
}
