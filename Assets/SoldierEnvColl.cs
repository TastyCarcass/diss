using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class SoldierEnvColl : MonoBehaviour 
{
	public AICharacterControl cController;

	public void OnTriggerEnter()
	{
		//Entered a collision trigger
		cController.Entered ();
	} //OnTriggerEnter

	public void OnTriggerExit()
	{
		//Left a collision trigger
		cController.Exited();
	} //OnTriggerExt
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition = cController.transform.localPosition;
	}//Update
}
