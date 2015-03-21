using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationModel
{
	public class positionData
	{
		public float xPos;
		public float yPos;
		public float zPos;
	}

	public string id;
	public int numNodes;
	public List<positionData> posList;
}
