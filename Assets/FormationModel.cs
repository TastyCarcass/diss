using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Title: Formation model
//Purpose: Each formation will contain this data.
//the position data represents a node.
public class FormationModel
{
	public class positionData
	{
		public float xPos;
		public float yPos;
		public float zPos;
	} // Position data
	public int numNodes; // Number of nodes in this formation
	public List<positionData> posList; //List of positions.
} // Formation Model.
