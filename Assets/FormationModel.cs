using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationModel
{
	public class Data
	{
		public JSONDATA data;
	}

	public class JSONDATA
	{
		public FormationModel[] JSONData;
	}

	public class positionData
	{
		public float xPos;
		public float yPos;
		public float zPos;
	}

	public int numNodes;
	public List<positionData> posList;
}
