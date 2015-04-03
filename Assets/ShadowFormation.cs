using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowFormation : MonoBehaviour, IFormation
{	
//	public Transform parentTrans;
//	public FNode prefab;
//	
//	public Dictionary<int, FormationModel.positionData> positionsList = new Dictionary<int, FormationModel.positionData>();
//	private List<FNode> nodesList = new List<FNode> ();

	private static int newUniqueIDNumber = 0;
	
	public Transform parentTrans;
	public FNode prefab;

	private int positionIndex = -1;
	
	public Dictionary<int, FormationModel.positionData> positionsList = new Dictionary<int, FormationModel.positionData>();
	private List<FNode> nodesList = new List<FNode> ();

	public bool addUnitsMode = false;
	public bool deleteUnitsMode = false;

	public void NodePressed(int ID)
	{
		if(deleteUnitsMode)
		{
			//do stuff
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
			Debug.Log("Add posish");
			Debug.Log(realPos);
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

		//If new formation does not have the same amount of units.
		//if (numUnitsIndex !=  null && numUnitsIndex != formation.Count)
		if(true)
		{
			positionsList = new Dictionary<int, FormationModel.positionData>();
			for(int i = 0; i<nodesList.Count; i++)
			{
				GameObject obj = nodesList[i].gameObject;
				nodesList[i] = null;
				Destroy (obj);
			}
			
			nodesList = new List<FNode>();

			foreach(FormationModel.positionData cData in formation)
			{
				AddFormationUnit(new Vector3(cData.xPos, cData.yPos, cData.zPos));
			}
		}
		else
		{
			//If it has the same amount of units
			List<FormationModel.positionData> cData = new List<FormationModel.positionData>();
			
			foreach(FormationModel.positionData cData1 in positionsList.Values)
			{
				cData.Add(cData1);
			}
			
			for (int i = 0; i < cData.Count; i++)
			{
				cData[i] = formation[i];
				Vector3 newPos = new Vector3(cData[i].xPos, cData[i].yPos, cData[i].zPos);
				nodesList[i].transform.localPosition = newPos;
			}
		}
	}
	
	public static int GetNewUniqueID()
	{
		newUniqueIDNumber++;
		return newUniqueIDNumber;
	}
	
	public void UpdateFormationUnit(int id, Vector3 newPos)
	{
		if (positionsList.ContainsKey(id))
		{
			Debug.Log("Updating position of unreleased node");
			positionsList[id].xPos = newPos.x;
			positionsList[id].yPos = newPos.y;
			positionsList[id].zPos = newPos.z;
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
		
		positionsList.Add(buff.GetUniqueID(), posData);
		nodesList.Add (buff);
		
		//SaveFormationInfo ();
	}
	
	public void AddNewUnit(Vector3 worldPos)
	{
		// Set the transform to a local position
		// Change the worldPos to localPos
		// Using InverseTransformPoint of parent
		
		Vector3 localPos = parentTrans.InverseTransformPoint(worldPos);
		
		localPos.y = 1; 
		
		AddFormationUnit (localPos);
		
	}


	public List<FormationModel.positionData> Export(bool asNew)
	{
		return null;
	}

	public void Import (bool asNew, List<FormationModel.positionData> aList)
	{
		Debug.Log ("importing");
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
