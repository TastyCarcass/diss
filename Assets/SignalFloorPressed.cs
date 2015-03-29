using UnityEngine;
using System.Collections;

public class SignalFloorPressed : MonoBehaviour 
{
	public GameObject tgt;

	void Pressed(InputEventArgs e)
	{
		tgt.SendMessage ("FloorPressed", e);
	}
}
