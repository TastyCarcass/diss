using UnityEngine;
using System.Collections;

public class SignalFloorPressed : MonoBehaviour 
{
	public GameObject tgt;

	float timeSincePress = 0; 
	bool pressed;

	void Pressed(InputEventArgs e)
	{

		pressed = true;
	}
	void Cancelled(InputEventArgs e)
	{
		//If a release is detected before the time, it is a tap. Sendmessage.  
		if(timeSincePress<0.5f)
		{
			tgt.SendMessage ("FloorTapped", e);
		}
		timeSincePress = 0;
		pressed = false;
	}
	void Update()
	{
		if (pressed) 
		{
			timeSincePress += Time.deltaTime;
		}
	}
}
