using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollBar : MonoBehaviour 
{
	public Formation formationMgr;
	Vector2 scrollPosition = Vector2.zero;
	float height = Screen.height;
	float width= Screen.width;

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
		Rect mRect = new Rect(0, 0, width/8, _height);
		scrollPosition = GUI.BeginScrollView(new Rect(0,50,width/8,height), scrollPosition,
		                                     mRect);
		if (formations != null)
		{
			float px = 10;
			float py = 0;
			float tw = 60;
			float th = 60;

			for(int i = 0; i < formations.Count; i++) {
				
				if(GUI.Button(new Rect(px,py,tw,th), "Fmtn: " + i))
				{
					formationMgr.SetFormation(i, formations[i]);
				}   
				
				py += 75;
				
			}
			
		}
		GUI.EndScrollView();
		
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
