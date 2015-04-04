using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationsList : MonoBehaviour 
{
	public Formation formationMgr;
	Vector2 scrollPosition = Vector2.zero;
	float height = Screen.height;
	float width = Screen.width;

	public float GUISize = 0.7f;
	public float buttonXPadding = 0.1f;

	public  float buttonXSize;

	private List< List<FormationModel.positionData> > formations;
	private int numUnitsInFormations;

	public void SetFormationEntries(List< List<FormationModel.positionData> > _formations)
	{
		formations = new List< List<FormationModel.positionData> > (_formations);
		numUnitsInFormations = _formations [0].Count;
	}


	void OnGUI()
	{
		float _height = formations != null ? (float)(formations.Count * 100.0f) : height;
		Rect mRect = new Rect(0, 0, width * GUISize, _height);

		scrollPosition = GUI.BeginScrollView(new Rect(0, 0, width * GUISize, height), scrollPosition, mRect);

		if (formations != null)
		{
			float px = ( (width * GUISize) - (buttonXSize) ) / 2;
			float py = 0;
			float tw = buttonXSize;
			float th = 60;

			for(int i = 0; i < formations.Count; i++) {
				
				if(GUI.Button(new Rect(px, py, tw, th), "Formation: " + i))
				{
					formationMgr.SetFormation(i, formations[i]);
				}   
				
				py += 75;
			}
		}

		GUI.EndScrollView();
	}

	void Start()
	{
		height = Screen.height;
		width = Screen.width;

		buttonXSize = (width * GUISize) * (1 - (buttonXPadding * 2));
	}
}
