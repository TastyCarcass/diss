//Author: Ian McLeod
//Purpose: The formation creation script. 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.Characters.ThirdPerson;


public class Formation : MonoBehaviour, IFormation
{
	private static int newUniqueIDNumber = 0;

    public Transform parentTrans;
    public FNode prefab;
	public GameObject UnitPrefab;

	private int numUnitsIndex = -1;
	private int positionIndex = -1;

	public Dictionary<int, FormationModel.positionData> positionsDict = new Dictionary<int, FormationModel.positionData>();
	private List<FNode> nodesList = new List<FNode> ();
	private List<AICharacterControl> unitList = new List<AICharacterControl> ();

	//Movement controls
	Vector3 Target;
	bool hasTarget = false;

	public void NodePressed(int ID)
	{

	}

	public void SetFormation(int _positionIndex, List<FormationModel.positionData> formation)
	{
		//If new formation does not have the same amount of units.
		if (numUnitsIndex !=  null && numUnitsIndex != formation.Count)
		{
			positionsDict = new Dictionary<int, FormationModel.positionData>();
			for(int i = 0; i < nodesList.Count; i++)
			{
				GameObject obj = nodesList[i].gameObject;
				nodesList[i] = null;
				Destroy (obj);
			}

			for (int i = 0; i < unitList.Count; i++)
			{
				GameObject obj = unitList[i].gameObject;
				unitList[i] = null;
				Destroy(obj.transform.parent.gameObject);
			}

			unitList = new List<AICharacterControl>();
			nodesList = new List<FNode>();
			positionIndex = _positionIndex;
			numUnitsIndex = formation.Count;

			foreach(FormationModel.positionData cData in formation)
			{
				AddFormationUnit(new Vector3(cData.xPos, cData.yPos, cData.zPos));
			}
		}
		else
		{
			// We need to do some hanky panky to move the current units we have
			// To the most suitable positions.
			// This is difficult and probably involves a lot of algorithm stuff

			//If it has the same amount of units
			positionIndex = _positionIndex;

			List<FormationModel.positionData> cData = new List<FormationModel.positionData>();

			foreach(FormationModel.positionData cData1 in positionsDict.Values)
			{
				cData.Add(cData1);
			}

			for (int i = 0; i < cData.Count; i++)
			{
				cData[i] = formation[i];
				Vector3 newPos = new Vector3(cData[i].xPos, cData[i].yPos, cData[i].zPos);
				nodesList[i].transform.localPosition = newPos;
			}

			for (int i = 0; i < nodesList.Count; i++)
			{
				unitList[i].SetNodeTarget(nodesList[i].transform, nodesList[i].GetUniqueID());
			}
		}
	}

	public static int GetNewUniqueID()
	{
		newUniqueIDNumber++;
		return newUniqueIDNumber;
	}

	public Transform GetNodeFromID(int otherID)
	{
		for(int i = 0; i < nodesList.Count; i++)
		{
			if(nodesList[i].GetUniqueID() == otherID)
			{
				return nodesList[i].transform;
			}
		}
		return null;
	}

	public void SaveFormationInfo()
	{
		List<FormationModel.positionData> newList = new List<FormationModel.positionData> ();
		
		foreach(FormationModel.positionData cData in positionsDict.Values)
		{
			newList.Add(cData);
		}
		
		if (numUnitsIndex == newList.Count)
		{
			FormationData.UpdateFormation (numUnitsIndex, positionIndex, newList);
		}
		else
		{
			FormationData.DeleteFormation(numUnitsIndex, positionIndex);
			
			FormationModel newFormation = new FormationModel();
			newFormation.numNodes = newList.Count;
			newFormation.posList = newList;

			int newIndex = FormationData.AddNewFormation(newFormation);

			numUnitsIndex = newList.Count;
			positionIndex = newIndex;
		}
	}

	public void UpdateFormationUnit(int id, Vector3 newPos)
	{
		if (positionsDict.ContainsKey(id))
		{
			Debug.Log("Updating position of unreleased node");
			positionsDict[id].xPos = newPos.x;
			positionsDict[id].yPos = newPos.y;
			positionsDict[id].zPos = newPos.z;
		}

		SaveFormationInfo ();
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

		positionsDict.Add(buff.GetUniqueID(), posData);
		nodesList.Add (buff);
	
		GameObject unitthing = Instantiate (UnitPrefab) as GameObject;
		AICharacterControl unitBuff = unitthing.transform.GetComponentInChildren<AICharacterControl> ();
		unitBuff.transform.position = parentTrans.TransformPoint (localPos);
		unitBuff.SetNodeTarget(buff.transform, buff.GetUniqueID());

		unitList.Add (unitBuff);
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
		if (asNew) 
		{
			List<FormationModel.positionData> returnList = new List<FormationModel.positionData>(positionsDict.Values.ToList());
			return returnList;
		}
		else
		{
			return positionsDict.Values.ToList();
		}
	}

	public void Import(bool asNew, List<FormationModel.positionData> aList)
	{
		if(asNew)
		{
			FormationModel newFormation = new FormationModel();
			newFormation.numNodes = aList.Count;
			newFormation.posList = aList;
			
			int newIndex = FormationData.AddNewFormation(newFormation);
			
			SetFormation(newIndex, aList);
		}
		else
		{
			if (aList.Count == numUnitsIndex)
			{
				SetFormation(positionIndex, aList);
				SaveFormationInfo();
			}
			else
			{
				// Delete our formation entirely
				// Recreate
				FormationData.DeleteFormation(numUnitsIndex, positionIndex);
				
				FormationModel newFormation = new FormationModel();
				newFormation.numNodes = aList.Count;
				newFormation.posList = aList;
				
				int newIndex = FormationData.AddNewFormation(newFormation);
				
				SetFormation(newIndex, aList);
			}
		}
	}

	public void FloorTapped(InputEventArgs e)
	{
		Vector2 moved = e.touchObject.position;
		Vector3 worldPos = e.touchCam.ScreenToWorldPoint(new Vector3(moved.x, moved.y, 15));
		Debug.LogError (worldPos);
		worldPos.y = 0;
		Target = worldPos;
		hasTarget = true;
	}

	private float GetSlowestUnitMultiplier()
	{
		float unitSpeed = 3.5f;
		float lowestSpeed = unitSpeed;

		foreach(AICharacterControl cControl in unitList)
		{
			if (cControl.agent.speed < lowestSpeed)
			{
				lowestSpeed = cControl.agent.speed;
			}
		}

		float percentDiff = lowestSpeed / unitSpeed;

		return percentDiff > 0.2f ? percentDiff : 0.2f;
	}

	public void Move()
	{
		if(hasTarget)
		{
			float stride = 0.1f * GetSlowestUnitMultiplier();
			transform.position = Vector3.MoveTowards(transform.position, (Vector3)Target, stride);
		
			if(Vector3.Distance(transform.position, (Vector3)Target)<0.1f)
			{
				hasTarget = false;
			}
		}
	}

	public void Update()
	{
		Move();
	}
}
