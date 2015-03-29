using UnityEngine;
using System.Collections;

public class FNode : MonoBehaviour, IIdentification
{
	public int ID;

	public void Pressed ()
	{
		transform.parent.SendMessage("NodePressed",ID); 
	}

	public void DragEnded(InputEventArgs e)
	{
		FinishedMoving ();
	}

	public void Cancelled(InputEventArgs e)
	{
		FinishedMoving ();
	}

	private void FinishedMoving()
	{
		GetComponentInParent<IFormation> ().UpdateFormationUnit (ID, transform.localPosition);
	}

	public void OnDrag(InputEventArgs e)
	{
		//e.touchObject.
		Vector2 moved = e.touchObject.position;
		Vector3 worldPos = e.touchCam.ScreenToWorldPoint (moved);
		Vector3 localPos = transform.parent.InverseTransformPoint (worldPos);
		localPos.y = 1;

		transform.localPosition = localPos;
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
