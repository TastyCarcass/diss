using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Initialiser : MonoBehaviour
{
	public Formation formationRoot;
	public string SavedToken;
	// Use this for initialization
	void Start () 
	{
		// TODO: Get last saved formation from prev. session and load.
		// FORNOW: Just load any formation
		int index = -1;

		foreach(int s in FormationData.Formations.Keys)
		{
			index = s;
			break;
		}

		List< List<FormationModel.positionData> > listOfFormations = FormationData.Formations[index];
		List<FormationModel.positionData> defaultFormation = listOfFormations [0];

		Debug.Log (defaultFormation [0].xPos);
		Debug.Log (defaultFormation [0].yPos);
		Debug.Log (defaultFormation [0].zPos);

		formationRoot.SetFormation (defaultFormation);
	}
}
