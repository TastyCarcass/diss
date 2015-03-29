using UnityEngine;
using System.Collections;
using System;

public class ButtonEventFolder : MonoBehaviour 
{
	public GameObject tgt;
	public string methodName;
	void Pressed()
	{
		try
		{
			tgt.SendMessage(methodName,SendMessageOptions.DontRequireReceiver);
		}
		catch(Exception e)
		{
			Debug.LogError("target or methodname wasn't supplied");
		}
	}
}
