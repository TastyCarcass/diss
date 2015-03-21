using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class FormationData
{

	private static string dataPath = "Assets/Resources/TextFiles/formationData.txt";

	private static FormationModel[] fData;

	private static Dictionary<int, List<List<FormationModel.positionData>>> formations;

	// For the love of God only use FormationData CLASS .add, .remove etc methods.
	// NEVER CALL METHODS ON THIS DICTIONARY MANUALY
	public static Dictionary<int, List<List<FormationModel.positionData>>> Formations
	{
		get
		{
			if(formations == null)
			{
				formations = new Dictionary<int, List<List<FormationModel.positionData>>>();
				LoadFormations();
			}
			return formations;
		}
	}

	public static void AddNewFormation(FormationModel newFormation)
	{
		if (!formations.ContainsKey(newFormation.numNodes))
		{
			formations.Add(newFormation.numNodes, new List<List<FormationModel.positionData>>());
		}

		formations [newFormation.numNodes].Add (newFormation.posList);

		SaveFormations ();
	}

	public static void DeleteFormation(FormationModel toDelete)
	{
		// TODO: Fill this in.
		Debug.LogError ("Not implemented: DeleteFormation (FormationData.cs)");
		SaveFormations ();
	}

	private static void LoadFormations ()
	{
		if (File.Exists(dataPath))
		{
			string data = File.ReadAllText (dataPath);
			FormationModel.Data ourData = JsonConvert.DeserializeObject<FormationModel.Data> (data);
			fData = ourData.data.JSONData;
			Debug.Log("fData: " + fData.Length);
		}
		else
		{
			throw new FileNotFoundException();
		}
		foreach(FormationModel cModel in fData)
		{
			if(!formations.ContainsKey(cModel.numNodes))
			{
				formations.Add(cModel.numNodes, new List<List<FormationModel.positionData>>());
			}
			
			formations[cModel.numNodes].Add(cModel.posList);
		}
	}

	public static void SaveFormations()
	{
		string toSave = JsonConvert.SerializeObject (formations);

		File.WriteAllText(dataPath, toSave);
	}
}
