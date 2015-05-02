using UnityEngine;
using System.Collections;

public class FNode : MonoBehaviour, IIdentification
{
	public int ID;
	bool pressed;

	public void Pressed ()
	{
		transform.parent.SendMessage("NodePressed",ID);
		pressed = true;
	}

	public void DragEnded(InputEventArgs e)
	{
		FinishedMoving ();
		pressed = false; 
	}

	public void Cancelled(InputEventArgs e)
	{
		FinishedMoving ();
		pressed = false; 
	}

	private void FinishedMoving()
	{
		GetComponentInParent<IFormation> ().UpdateFormationUnit (ID, transform.localPosition);
		pressed = false;
	}

	public void OnDrag(InputEventArgs e)
	{
		if (pressed) 
		{
			//e.touchObject.
			Vector3 moved = e.touchObject.position;
			moved.z = 15;
			Vector3 worldPos = e.touchCam.ScreenToWorldPoint (moved);
			Vector3 localPos = transform.parent.InverseTransformPoint (worldPos);
			localPos.y = 1;

			transform.localPosition = localPos;
		}
	}

	public void SetUniqueID(int newID)
    {
        ID = newID;
    }

    public int GetUniqueID()
    {
        return ID;
    }
}
