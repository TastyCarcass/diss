using UnityEngine;
using System.Collections;

//Title: FNode
//Purpose: This scripts recieves touch events and handles them appropriately. 
//It's main focus is movement on a drag. The units will always be following
//these nodes.

public class FNode : MonoBehaviour, IIdentification
{
	public int ID; //Node identification.
	
	bool pressed; //Register if pressed. 
	
	public void Pressed ()
	{
		//Send event to parent that it has been pressed.
		transform.parent.SendMessage("NodePressed",ID);
		pressed = true;
	}//Pressed
	
	public void DragEnded(InputEventArgs e)
	{
		//Send event to parent that the drag has ended.
		FinishedMoving ();
		pressed = false; 
	}//Drag Ended
	
	public void Cancelled(InputEventArgs e)
	{
		//Send event to parent that the touch input has been cancelled.
		FinishedMoving ();
		pressed = false; 
	} //Cancelled
	
	private void FinishedMoving()
	{
		//This function registers its position within the formation's formation library.
		GetComponentInParent<IFormation> ().UpdateFormationUnit (ID, transform.localPosition);
		pressed = false;
	} //Finished Moving
	
	public void OnDrag(InputEventArgs e)
	{
		// Handles the dragging of an object.
		if (pressed) 
		{
			//Only allowed if the object has first been pressed.
			//This prevents sliding onto an object to move it.
			//Calculate the local position of the touch.
			//Literal values are used for the height.
			Vector3 moved = e.touchObject.position;
			moved.z = 15; 
			Vector3 worldPos = e.touchCam.ScreenToWorldPoint (moved);
			Vector3 localPos = transform.parent.InverseTransformPoint (worldPos);
			localPos.y = 1; 
			transform.localPosition = localPos;
		}
	} // On Drag
	
	public void SetUniqueID(int newID)
	{
		// sets a new ID based on the int parameter 
		ID = newID;
	} //Set Unique ID
	
	public int GetUniqueID()
	{ 
		// Returns the ID of this node. 
		return ID;
	} // Get Unique ID
} //FNode
