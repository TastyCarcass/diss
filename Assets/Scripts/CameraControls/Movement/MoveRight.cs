//Author: Ian McLeod
//Title: Movement

using UnityEngine;
using System.Collections;
using System;

public class MoveRight : MonoBehaviour
{
	bool active;
	public Camera cam;
	Vector3 translation; 
	public void Pressed(InputEventArgs e)
	{
		active = true;
	}
	public void Cancelled(InputEventArgs e)
	{
		active = false;
	}
	public void Start()
	{
		active = false;
		translation.Set (10, 0, 0);
	}
	
	public void Update()
	{
		if(active)
		{
			cam.gameObject.transform.Translate (translation * Time.deltaTime);
		}
	}
}
