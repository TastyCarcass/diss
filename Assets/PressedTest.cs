﻿using UnityEngine;
using System.Collections;

public class PressedTest : MonoBehaviour {

//	void Pressed(InputEventArgs e)
//	{
//		DebugWindow.ourText = "Tadah";
//		Debug.Log ("Press message recieved ");
//	}

	void DragEntered(InputEventArgs e)
	{
		Debug.Log ("Drag Entered");
	}
	void DragExited(InputEventArgs e)
	{
		Debug.Log ("Drag Exited");
	}
	void OnDrag(Vector2 newPos)
	{
		Debug.Log ("Dragging");
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
