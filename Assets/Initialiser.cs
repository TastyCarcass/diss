﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Title: Initialiser
//Upon loading the game, this is used for the formation.
public class Initialiser : MonoBehaviour
{
	public FormationsList formationsBar; // The list of formations
	public Formation formationRoot; //The formation to alter
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

			List<FormationModel.positionData> defaultFormation = listOfFormations [0];

			formationRoot.SetFormation (0, defaultFormation);
		}
		else
		{

		}
	}
}
