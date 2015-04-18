using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class SoldierEnvColl : MonoBehaviour {

	public AICharacterControl cController;

	public void OnTriggerEnter()
	{
		cController.Entered ();
	}

	public void OnTriggerExit()
	{
		Debug.LogError ("Fucks given");
		cController.Exited();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = cController.transform.localPosition;
	}
}
