using UnityEngine;
using System.Collections;

public class DebugWindow : MonoBehaviour {

	public static string ourText = "Testing";

	void OnGUI()
	{
		GUI.Button (new Rect (10, 10, 1200, 100), ourText);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
