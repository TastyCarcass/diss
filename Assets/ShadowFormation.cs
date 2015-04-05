using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
			positionsDictionary.Remove(ID);

			int index = -1;
			for (int i = 0; i < nodesList.Count; i++)
			{
				if (nodesList[i].GetUniqueID() == ID)
				{
					index = i;
					break;
				}
			}

			GameObject del = nodesList[index].gameObject;

			nodesList.Remove(nodesList[index]);

			Destroy(del);
		}
	}

	public void FloorPressed(InputEventArgs e)
	{
		if(addUnitsMode)
		{
			//Logic to add unit to pressed position.
			Vector3 realPos;
			Vector2 camPos = e.touchObject.position;
			realPos = e.touchCam.ScreenToWorldPoint (camPos);
			AddNewUnit (realPos);
		}
	}

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
	}

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
	}

	private void SetFormation(List<FormationModel.positionData> formation)
	{
		// TODO: Change to take bool parameter whether or not asNew from Import()
	
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
	}
	
	public static int GetNewUniqueID()
	{
		newUniqueIDNumber++;
		return newUniqueIDNumber;
	}
	
	public void UpdateFormationUnit(int id, Vector3 newPos)
	{
		if (positionsDictionary.ContainsKey(id))
		{
			Debug.Log("Updating position of unreleased node");
			positionsDictionary[id].xPos = newPos.x;
			positionsDictionary[id].yPos = newPos.y;
			positionsDictionary[id].zPos = newPos.z;
		}
	}
	
	public void AddFormationUnit(Vector3 localPos)
	{	
		FNode buff = Instantiate(prefab) as FNode;
		buff.transform.parent = parentTrans;
		
		buff.SetUniqueID(GetNewUniqueID());
		
		buff.transform.localPosition = localPos;
		
		FormationModel.positionData posData = new FormationModel.positionData ();
		posData.xPos = localPos.x;
		posData.yPos = localPos.y;
		posData.zPos = localPos.z;
		
		positionsDictionary.Add(buff.GetUniqueID(), posData);
		nodesList.Add (buff);
		
		//SaveFormationInfo ();
	}
	
	public void AddNewUnit(Vector3 worldPos)
	{	
		Vector3 localPos = parentTrans.InverseTransformPoint(worldPos);
		
		localPos.y = 1; 
		
		AddFormationUnit (localPos);
		
	}

	public void ResetFormation()
	{
		SetFormation (new List<FormationModel.positionData>(startPositionsList));
	}

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
	}

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
	}
}
