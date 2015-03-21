using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class FormationData
{
	private static string path = Application.persistentDataPath;

	private static string dataPath = path + "FormationData.txt";

	private static Dictionary<int, List<List<FormationModel.positionData>>> formations;

	// For the love of God only use FormationData CLASS .add, .remove etc methods.
	// NEVER CALL METHODS ON THIS DICTIONARY MANUALY
	public static Dictionary<int, List<List<FormationModel.positionData>>> Formations
	{
		get
		{
			if(formations == null)
			{
				Debug.Log(Application.persistentDataPath);
				formations = new Dictionary<int, List<List<FormationModel.positionData>>>();
				LoadFormations();
			}
			return formations;
		}
	}

	public static void UpdateFormation(int numUnitsIndex, int posIndex, List<FormationModel.positionData> posList)
	{
		List<List<FormationModel.positionData>> toUpdate = formations [numUnitsIndex];
		toUpdate [posIndex] = posList;

		SaveFormations ();
	}

	// Returns the index of the newly added formation.
	public static int AddNewFormation(FormationModel newFormation)
	{
		if (!formations.ContainsKey(newFormation.numNodes))
		{
			formations.Add(newFormation.numNodes, new List<List<FormationModel.positionData>>());
		}

		formations [newFormation.numNodes].Add (newFormation.posList);

		SaveFormations ();

		return formations [newFormation.numNodes].Count - 1;
	}

	public static void DeleteFormation(int numUnitsIndex, int position)
	{
		List<List<FormationModel.positionData>> toUpdate = formations [numUnitsIndex];
		toUpdate.RemoveAt (position);

		SaveFormations ();
	}

	private static void LoadFormations ()
	{
		if (File.Exists(dataPath))
		{
			string data = File.ReadAllText (dataPath);

			formations = JsonConvert.DeserializeObject<Dictionary<int, List<List<FormationModel.positionData>>>>(data);
		}
		else
		{
			AddTestFormation();
			LoadFormations();
		}
	}

	public static void AddTestFormation()
	{
		FormationModel nf = new FormationModel ();

		nf.id = "testformation";

		nf.numNodes = 1;
		nf.posList = new List<FormationModel.positionData> ();

		FormationModel.positionData data = new FormationModel.positionData ();
		data.xPos = 1;
		data.yPos = 1;
		data.zPos = 1;

		nf.posList.Add (data);

		AddNewFormation (nf);
	}

	public static void SaveFormations()
	{
		string toSave = JsonConvert.SerializeObject (formations);
		File.WriteAllText(dataPath, toSave);
	}
}
