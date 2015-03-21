using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class FormationData
{

	private static string dataPath = "Assets/Resources/TextFiles/FormationData.txt";

	private static List<FormationModel> fData = new List<FormationModel>();

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

		fData.Add (newFormation);

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
			FormationModel[] ourData = JsonConvert.DeserializeObject<FormationModel[]> (data);
			fData = new List<FormationModel>(ourData);
			Debug.Log("fData: " + fData.Count);
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

	public static void AddTestFormation()
	{
		FormationModel nf = new FormationModel ();

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
		string toSave = JsonConvert.SerializeObject (fData);

		File.WriteAllText(dataPath, toSave);
	}
}
