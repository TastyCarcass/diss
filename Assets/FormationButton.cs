using UnityEngine;
using System.Collections;

public class formationbutton : MonoBehaviour 
{
	int formID;
	public Formation formationMgr;
	public CreateScrollList scrollList;
	void thing()
	{
		scrollList.SomethingToDo(formID);
	}

}
