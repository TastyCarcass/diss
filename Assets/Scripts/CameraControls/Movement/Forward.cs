
//Author: Ian McLeod
//Title: Movement

using UnityEngine;
using System.Collections;
using System;

public class Forward : MonoBehaviour
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
		translation.Set (0, 0, 10);
	}
	
	public void Update()
	{
		if(active)
		{
			cam.gameObject.transform.Translate (translation * Time.deltaTime);
		}
	}
}