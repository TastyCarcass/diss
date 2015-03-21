using UnityEngine;
using System.Collections;

public class FNode : MonoBehaviour, IIdentification
{
	private int ID;

	public void OnDrag(InputEventArgs e)
	{
		Vector2 moved = e.touchObject.deltaPosition;
		Vector3 movedBy = new Vector3(moved.x, 0, moved.y);

		movedBy += transform.localPosition;

		transform.localPosition = movedBy;
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
