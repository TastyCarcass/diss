using UnityEngine;
using System.Collections;

public class FormationNode : MonoBehaviour {

	private bool selected = false;

	public void Selected()
	{
		Debug.Log("selected.");
		selected = true;
	}
	
	public void Deselected()
	{
		Debug.Log("deselected.");
		selected = false;
	}

	public void Move(Vector3 trans)
	{
		this.transform.position = trans;
	}

	public void Rotate(float dir)
	{


	}

	// Update is called once per frame
	void Update () 
	{
		if(selected)
		{
			GetComponent<Renderer>().material.color = Color.yellow;
		}
		if(!selected)
		{
			GetComponent<Renderer>().material.color = Color.red;
		}
	}
}
