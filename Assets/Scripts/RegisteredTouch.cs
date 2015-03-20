using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class RegisteredTouch
	{
		// the GameObject which was selected
		public GameObject touchedObject;
		// the ID of the finger which touched it
		public int touchID;
		
		//These bools a
		public bool dragged;
		public bool pressed;
		public bool held;

		public bool inUse;

		private void Initialise()
		{
			//constructor
			touchedObject = null;
			touchID = -1;
			inUse = false; 
			
			dragged = false;
			pressed = false;
		}

		public RegisteredTouch ()
		{
			Initialise ();
		}

		public void Pressed(InputEventArgs e)
		{
			if (!pressed)
			{
				touchedObject = e.hitObject;
				touchedObject.SendMessage ("Pressed",e, SendMessageOptions.DontRequireReceiver);
				Debug.Log(touchedObject.name);
				pressed = true;
			}
		}

		public void Moved(InputEventArgs e)
		{
			if(touchedObject == null)
			{
				touchedObject = e.hitObject;
				touchedObject.SendMessage ("DragEntered", e, SendMessageOptions.DontRequireReceiver);
			}
			else if(touchedObject == e.hitObject)
			{
				touchedObject.SendMessage ("OnDrag", e, SendMessageOptions.DontRequireReceiver);
			}
			else if(touchedObject != e.hitObject)
			{
				touchedObject.SendMessage ("DragExited", e, SendMessageOptions.DontRequireReceiver);
				touchedObject = e.hitObject;
				touchedObject.SendMessage ("DragEntered", e, SendMessageOptions.DontRequireReceiver);
			}

		}

		public void Cancelled(InputEventArgs e)
		{
			// bug prevention
			if (touchedObject != null)
			{
				if(touchedObject == e.hitObject)
				{
					touchedObject.SendMessage("Cancelled", e, SendMessageOptions.DontRequireReceiver);
				}

			}
			Initialise ();
		}

		public void RegisterTouch(int ID)
		{
			inUse = true;
			touchID = ID;
		}

		public int GetID()
		{
			return touchID;
		}
	}
}

