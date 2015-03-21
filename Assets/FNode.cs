using UnityEngine;
using System.Collections;

public class FNode : MonoBehaviour, IIdentification
{
	private int ID;

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
		GetComponentInParent<Formation> ().UpdateFormationUnit (ID, transform.localPosition);
	}

	public void OnDrag(InputEventArgs e)
	{
		Vector2 moved = e.touchObject.position;
		Debug.LogError ("OLA: " + moved);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint (moved);
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
