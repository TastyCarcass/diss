using UnityEngine;
using System.Collections;

public class SignalObjectPressed : MonoBehaviour {

	public GameObject tgt;
	void Pressed(InputEventArgs e)
	{
		tgt.SendMessage ("Pressed", e);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
