using UnityEngine;
using System.Collections;

public class EditCamera : MonoBehaviour 
{
	public Formation form;

	void Pressed(InputEventArgs e)
	{
		Vector3 realPos;
		Vector2 camPos = e.touchObject.position;
		realPos = GetComponent<Camera> ().ScreenToWorldPoint (camPos);
		Debug.Log("Add posish");
		Debug.Log(realPos);
		form.AddNewUnit (realPos);
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
