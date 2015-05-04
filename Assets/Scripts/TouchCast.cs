using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Title: Touch Manager
//Purpose: This script is responsible for sending touch events.
//It achieves this with send message functions based on the phase of the touch.
//Note that a gesture recogniser was added, separating gestures from the manager. 

//Use AssemblyCSharp to access our Selected Object Class
using AssemblyCSharp;

public class TouchCast : MonoBehaviour 
{
	public LayerMask touchMask; // The layers which can be touched.
	
	static List<RegisteredTouch> SelectedObjects; // The list of objects which have been touched.
	
	private RegisteredTouch[] RegisteredTouches = new RegisteredTouch[10]; // Array of current touches.
	
	static List<Camera> ListCamera; // Used as the list of cameras. Important for moving. 
	
	bool initialised = false; //Will be used to ensure that the touchmanager is initialised.
	
	public CameraManager camMgr; // Camera manager. 
	
	void Start ()
	{	//Will be used as the initialiser.
		for (int i = 0; i < RegisteredTouches.Length; i++)
		{
			//Constructs all registered touches.
			RegisteredTouches[i] = new RegisteredTouch();
		} // for loop
		
		SelectedObjects = new List<RegisteredTouch>(); // construct selected objects list.
		
		ListCamera = Camera.allCameras.ToList(); // Fills the camera list with all active cameras
		
		ListCamera.OrderBy (Cam => Cam.depth ); // Order them by depth and reverse it.
		ListCamera.Reverse ();
		
		camMgr.Initialise (); // Initialise the camera manager
		
		initialised = true; // Initialised, touch functions will now be accepted.
	}//Start
	
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
	} // CheckList
	
	bool isRegistered(int ID)
	{
		//Returns true if the ID belongs to a registered touch.
		foreach(RegisteredTouch entry in RegisteredTouches)
		{
			if(entry.GetID()==ID)
			{
				return true;
			}
		}
		return false;
	} //is Registered
	
	void RegisterNewTouch(int ID)
	{	
		//If a registered touch object is not in use, register new touch.
		foreach(RegisteredTouch entry in RegisteredTouches)
		{
			if(!entry.inUse)
			{
				entry.RegisterTouch(ID);
			}
		}
	} //Register new touch
	
	RegisteredTouch GetRegisteredTouch(int ID)
	{
		//Returns registered touch based on the ID.
		foreach(RegisteredTouch aTouch in RegisteredTouches)
		{
			if(aTouch.GetID() == ID)
			{
				return aTouch;
			}
		}
		return null;
	}//GetRegisteredTouch
	
	// Update is called once per frame
	void Update () 
	{
		//Calls each frame.
		if(initialised)
		{
			//Safety.
			foreach(Touch ourTouch in Input.touches)
			{
				//Loops through all current touches. 
				//Checks if it has been registered. If not, register it.
				if(!isRegistered(ourTouch.fingerId))
				{
					RegisterNewTouch(ourTouch.fingerId);
				} // If statement
				RegisteredTouch rTouch = GetRegisteredTouch(ourTouch.fingerId);
				
				foreach(Camera cam in ListCamera)
				{
					if (cam.gameObject.activeSelf == false) continue; // Only run if the camera is active.
					
					Ray ray = cam.ScreenPointToRay (ourTouch.position); // Creates a ray based on the touch position
					
					RaycastHit hit;
					
					if(Physics.Raycast (ray, out hit, Mathf.Infinity, touchMask))
					{
						// There has been a collision with the raycast.
						// Handle logic. 
						
						// create event arguements to be passed to objects
						InputEventArgs e = new InputEventArgs();
						e.hitObject = hit.collider.gameObject;
						e.touchObject = ourTouch;
						e.touchCam = cam;
						
						//Switch statement to send registered touches the touchphase.
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
						
						//Break from the loop, a touch has been registered.
						break;
					} // If raycast
					else
					{
						//No touch recognised. 
						Debug.Log("No touch recognised");
					} //Else
				}// Camera for each Loop Camera
			}// Touch for each loop
		}
	}//Update
} //Touch Cast class