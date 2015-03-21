//Author: Ian McLeod
//Purpose: The formation creation script. 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Formation : MonoBehaviour 
{
	private static int newUniqueIDNumber = 0;

    public Transform parentTrans;
    public FNode prefab;

	private int numUnitsIndex = -1;
	private int positionIndex = -1;

	public Dictionary<int, FormationModel.positionData> positionsList = new Dictionary<int, FormationModel.positionData>();
	private List<FNode> nodesList = new List<FNode> ();

	public void SetFormation(int _positionIndex, List<FormationModel.positionData> formation)
	{
		if (_positionIndex != positionIndex)
		{
			positionsList = new Dictionary<int, FormationModel.positionData>();
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
			positionIndex = _positionIndex;

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
			positionsList[id].xPos = newPos.x;
			positionsList[id].yPos = newPos.y;
			positionsList[id].zPos = newPos.z;
		}

		List<FormationModel.positionData> newList = new List<FormationModel.positionData> ();

		foreach(FormationModel.positionData cData in positionsList.Values)
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

    void DeleteUnit()
    {
    }

	/*
	// Use this for initialization
	void Start () 
	{
		//nodeArr = new GameObject[nodeCount];
		//For debug
		nodeCount = 16;
		lineCount = 4;
		space = 2;
		//For loop for initial creation
		for (numUnits = 0; numUnits < nodeCount; numUnits++) 
		{
			FNode buff = Instantiate (prefab) as FNode;
			buff.transform.parent = parentTrans;

            buff.SetUniqueID(numUnits);

			nodeList.Add (buff.gameObject);            
		}


		//Object placement
		int index = 0;
		for (int i = 0; i < lineCount; i++) 
		{
			for(int j = 0; j < lineCount; j++)
			{
				float transX = j * space;
				float transZ = i * space;
				Vector3 trans = new Vector3(transX, 0.2f, transZ);
				nodeList[index].transform.localPosition = trans;
				index++;
			}
		}
		for (int i = 0; i < nodeCount; i++) 
		{
			Vector3 trans = new Vector3(-3f, 0f, -3f );
			nodeList[i].transform.localPosition += trans; 
		}
	}*/
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
