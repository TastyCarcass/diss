using UnityEngine;
using System.Collections;

//Title: Signal Floor Pressed
//Purpose: Sends a signal to target on tap. 

public class SignalFloorPressed : MonoBehaviour 
{
	public GameObject tgt; // object to send message to

	float time = 0; //To register a tap, we must keep track of the time
	bool pressed = false; //Will be used for counting.

	void Pressed(InputEventArgs e)
	{
		pressed = true; 
	} //Pressed
	public void DragEnded(InputEventArgs e)
	{
		//Send event to parent that the drag has ended.
		if(time < 0.5f)
			tgt.SendMessage ("FloorPressed", e);
		pressed = false; 
	}//Drag Ended
	
	public void Cancelled(InputEventArgs e)
	{
		//Send event to parent that the touch input has been cancelled.
		if(time < 0.5f)
			tgt.SendMessage ("FloorPressed", e);
		pressed = false; 
	} //Cancelled

	void Update()
	{
		//Called each frame
		if(pressed)
		{
			time += Time.deltaTime;
		}
		else time = 0;
	} //Update
} // Signal Floor Pressed
