using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CreateScrollList : MonoBehaviour 
{
	public Formation formationMgr;
	private List< List<FormationModel.positionData> > formations;
	private int numUnitsInFormations;

	public Transform contentPanel;

	public GameObject sampleButton;

	public void SetFormationEntries(List< List<FormationModel.positionData> > _formations)
	{
		Debug.Log("Hey");
		formations = new List< List<FormationModel.positionData> > (_formations);
		numUnitsInFormations = _formations [0].Count;
		Start ();
	}

	public void AddNewButton(int index)
	{
		GameObject NewButton = Instantiate (sampleButton) as GameObject;
		SampleButton buf = NewButton.GetComponent<SampleButton>();
		buf.formationID.text = "6";
//		SampleButton button = NewButton.GetComponent<SampleButton> ();
//		button.formationID = index;
		//button.button.onClick.AddListener(() => SomethingToDo (index));
		NewButton.transform.SetParent (contentPanel);
	}

	public void SomethingToDo(int pos)
	{
		formationMgr.SetFormation(pos, formations[pos]);
	}

	void Start()
	{
		if (formations != null)
		{
			for(int i = 0; i < formations.Count; i++)
			{
				AddNewButton (i);
			}
		}
	}
}
