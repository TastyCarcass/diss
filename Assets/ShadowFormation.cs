using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Title: Shadow Formation
// Purpose: This is the formation used by the edit mode.
// It behaves similarly to the main formation. However, it does not 
// create units. 
// It acts as a buffer for uncommitted changes. Upon pressing the update
// formation button, those changes are exported to the main formation.
public class ShadowFormation : MonoBehaviour, IFormation
{	
	private static int newUniqueIDNumber = 0;
	
	public Transform parentTrans;
	public FNode prefab;

	private int positionIndex = -1;

	public List<FormationModel.positionData> startPositionsList = new List<FormationModel.positionData>();
	public Dictionary<int, FormationModel.positionData> positionsDictionary = new Dictionary<int, FormationModel.positionData>();
	private List<FNode> nodesList = new List<FNode> ();

	public bool addUnitsMode = false;
	public bool deleteUnitsMode = false;

	public void NodePressed(int ID)
	{
		if(deleteUnitsMode)
		{
			//If a node has been pressed and we are in delete units mode
			// Remove it from dictionary
			positionsDictionary.Remove(ID);

			int index = -1;
			// Find node with thsi ID
			for (int i = 0; i < nodesList.Count; i++)
			{
				if (nodesList[i].GetUniqueID() == ID)
				{
					index = i;
					break;
				}
			}
			// obtain a reference to deleted object
			GameObject del = nodesList[index].gameObject;

			// Remove from list
			nodesList.Remove(nodesList[index]);

			// delete it
			Destroy(del);
		}
	} //Node Pressed

	public void FloorPressed(InputEventArgs e)
	{
		if(addUnitsMode)
		{
			Debug.LogError("Pressed");
			//Logic to add unit to pressed position.
			Vector3 realPos;
			Vector2 camPos = e.touchObject.position;
			realPos = e.touchCam.ScreenToWorldPoint (camPos);

			AddNewUnit (realPos);
		}
	} // Floor Pressed

	public void AddUnitButtonPressed()
	{
		if (addUnitsMode)
		{
			addUnitsMode = false;
		}
		else
		{
			addUnitsMode = true;
			deleteUnitsMode = false;
		}
	} //Add Unit Button Pressed

	public void DeleteUnitButtonPressed()
	{
		if (deleteUnitsMode) 
		{
			deleteUnitsMode = false;
		}
		else
		{
			deleteUnitsMode = true;
			addUnitsMode = false;
		}		
	} //Delete Unit Button Pressed

	private void SetFormation(List<FormationModel.positionData> formation)
	{
		startPositionsList.Clear();
		positionsDictionary.Clear();

		for(int i = 0; i < nodesList.Count; i++)
		{
			GameObject obj = nodesList[i].gameObject;
			nodesList[i] = null;
			Destroy (obj);
		}
			
		nodesList.Clear();

		startPositionsList = new List<FormationModel.positionData>(formation);

		foreach(FormationModel.positionData cData in formation)
		{
			AddFormationUnit(new Vector3(cData.xPos, cData.yPos, cData.zPos));
		}
	} // Set Formation
	
	public static int GetNewUniqueID()
	{
		newUniqueIDNumber++;
		return newUniqueIDNumber;
	} //Get New Unique ID
	
	public void UpdateFormationUnit(int id, Vector3 newPos)
	{
		if (positionsDictionary.ContainsKey(id))
		{
			Debug.Log("Updating position of unreleased node");
			positionsDictionary[id].xPos = newPos.x;
			positionsDictionary[id].yPos = newPos.y;
			positionsDictionary[id].zPos = newPos.z;
		}
	} //Update Formation Unit
	
	public void AddFormationUnit(Vector3 localPos)
	{	
		FNode buff = Instantiate(prefab) as FNode; //Buffer node
		buff.transform.parent = parentTrans; 
		
		buff.SetUniqueID(GetNewUniqueID());
		
		buff.transform.localPosition = localPos;
		
		FormationModel.positionData posData = new FormationModel.positionData ();
		posData.xPos = localPos.x;
		posData.yPos = localPos.y;
		posData.zPos = localPos.z;
		
		positionsDictionary.Add(buff.GetUniqueID(), posData);
		nodesList.Add (buff);
	} //Add Formation Unit
	
	public void AddNewUnit(Vector3 worldPos)
	{	
		Vector3 localPos = parentTrans.InverseTransformPoint(worldPos);

		localPos.y = 0; 
		
		AddFormationUnit (localPos);
		
	} //Add New Unit

	public void ResetFormation()
	{
		SetFormation (new List<FormationModel.positionData>(startPositionsList));
	} //Reset Formation

	public List<FormationModel.positionData> Export(bool asNew)
	{
		if (asNew) 
		{
			List<FormationModel.positionData> returnList = new List<FormationModel.positionData>(positionsDictionary.Values.ToList());
			return returnList;
		}
		else
		{
			return positionsDictionary.Values.ToList();
		}
	} // export

	public void Import (bool asNew, List<FormationModel.positionData> aList)
	{
		if(asNew)
		{
			SetFormation (aList);
		}
		else
		{
			SetFormation (aList);
		}
	} // Import
}
