﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Initialiser : MonoBehaviour
{
	public FormationsList formationsBar;
	public CreateScrollList formationsBar2;
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
			if (FormationData.Formations[s].Count > 0)
			{
				index = s;
				break;
			}
		}

		if (index != -1)
		{
			List< List<FormationModel.positionData> > listOfFormations = FormationData.Formations[index];
			formationsBar.SetFormationEntries(listOfFormations);

			//List<FormationModel.positionData> defaultFormation = listOfFormations [0];

			//formationRoot.SetFormation (0, defaultFormation);
		}
		else
		{

		}
	}
}
