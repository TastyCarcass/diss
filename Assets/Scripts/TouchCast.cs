using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Use AssemblyCSharp to access our Selected Object Class
using AssemblyCSharp;

public class TouchCast : MonoBehaviour 
{
	public AddUnitButton addUnitButton;
	public LayerMask touchMask; // used for collisions, what 
	//can and cannot be hit. 


	//private GameObject[] selectedObjects;
	static List<RegisteredTouch> SelectedObjects;

	private RegisteredTouch[] RegisteredTouches = new RegisteredTouch[10];
	//private RegisteredTouches = new GameObject RegisteredTouch[10];
	//private cameras
	static List<Camera> ListCamera;

	//for rotation
	public bool rotatable; 
	private int rotateID;

	bool initialised = false;

	public CameraManager camMgr;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < RegisteredTouches.Length; i++)
		{
			RegisteredTouches[i] = new RegisteredTouch();
		}

		//create a list of size 10. Number of touches. 
		SelectedObjects = new List<RegisteredTouch>();


		ListCamera = Camera.allCameras.ToList();

		ListCamera.OrderBy (Cam => Cam.depth );
		ListCamera.Reverse ();
		
		camMgr.Initialise ();

		initialised = true;


	}

	int CheckList(int otherID)
	{
		//searches through list and finds the correct selected object based on touchID
		for(int i = 0; i < SelectedObjects.Count; i++)
		{
			if(SelectedObjects[i].GetID()==otherID)
			{
				return i;
			}
		}
		return 0; 
	}

	bool isRegistered(int ID)
	{
		foreach(RegisteredTouch entry in RegisteredTouches)
		{
			if(entry.GetID()==ID)
			{
				return true;
			}
		}
		return false;
	}

	void RegisterNewTouch(int ID)
	{		
		foreach(RegisteredTouch entry in RegisteredTouches)
		{
			if(!entry.inUse)
			{
				entry.RegisterTouch(ID);
			}
		}
	}

	RegisteredTouch GetRegisteredTouch(int ID)
	{
		foreach(RegisteredTouch aTouch in RegisteredTouches)
		{
			if(aTouch.GetID() == ID)
			{
				return aTouch;
			}
		}
		return null;
	}

	// Update is called once per frame
	void Update () 
	{
		if(initialised)
		{
			foreach(Touch ourTouch in Input.touches)
			{
				if(!isRegistered(ourTouch.fingerId))
				{
					RegisterNewTouch(ourTouch.fingerId);
				}
				RegisteredTouch rTouch = GetRegisteredTouch(ourTouch.fingerId);

				foreach(Camera cam in ListCamera)
				{
					if (cam.gameObject.activeSelf == false) continue;
					Ray ray = cam.ScreenPointToRay (ourTouch.position);
					RaycastHit hit;
					LayerMask mask = new LayerMask();
					mask.value = cam.cullingMask;

					if(Physics.Raycast (ray, out hit, Mathf.Infinity, touchMask))
					{
						// We need to handle our logic for sending out events, etc.
						InputEventArgs e = new InputEventArgs();
						e.hitObject = hit.collider.gameObject;
						e.touchObject = ourTouch;
						e.touchCam = cam;


						switch(ourTouch.phase)
						{
							case TouchPhase.Began:
								rTouch.Pressed(e);
								break;
							case TouchPhase.Moved:
								Debug.Log("Moved");
								rTouch.Moved(e);
								break;
							case TouchPhase.Canceled:
								rTouch.Cancelled(e);
								Debug.Log ("Released");
								break;
							case TouchPhase.Ended:
								rTouch.Cancelled(e);
								Debug.Log ("Released");
								break;

						};

						break;
					}
					else
					{
						Debug.Log("No touch recognised");
					}
				}
			}
		}
	}
}