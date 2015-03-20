using UnityEngine;
using System.Collections;

public class FNode : MonoBehaviour, IIdentification
{
	private int ID;

	public void SetUniqueID(int newID)
    {
        ID = newID;
    }

    public int GetUniqueID()
    {
        return ID;
    }
}
