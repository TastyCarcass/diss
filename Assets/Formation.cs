//Author: Ian McLeod
//Purpose: The formation creation script. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Formation : MonoBehaviour 
{
	private static int newUniqueIDNumber = 0;
    public Transform parentTrans;
    public FNode prefab;
	public List<FormationModel.positionData> positionsList = new List<FormationModel.positionData>();
	//System.Collections.Generic.List<GameObject> nodeList; //list of nodes 
	public List<GameObject> nodeList = new List<GameObject> ();

	public void SetFormation(List<FormationModel.positionData> formation)
	{
		positionsList = new List<FormationModel.positionData> (formation);

		foreach(FormationModel.positionData cData in positionsList)
		{
			AddFormationUnit(new Vector3(cData.xPos, cData.yPos, cData.zPos));
		}
	}

	public static int GetNewUniqueID()
	{
		newUniqueIDNumber++;
		return newUniqueIDNumber;
	}

	public void AddFormationUnit(Vector3 localPos)
	{	
		Debug.Log (localPos);
		FNode buff = Instantiate(prefab) as FNode;
		buff.transform.parent = parentTrans;
		
		nodeList.Add(buff.gameObject);  

		// that's not going to work.
		buff.SetUniqueID(GetNewUniqueID());

		buff.transform.localPosition = localPos;
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
