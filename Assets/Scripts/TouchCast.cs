using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//Use AssemblyCSharp to access our Selected Object Class
using AssemblyCSharp;

public class TouchCast : MonoBehaviour 
{
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
//		
//		if(initialised)
//		{
//		for (int i = 0; i<Input.touchCount; i++)
//		{ 
//			
//			if(Input.GetTouch(i).phase == TouchPhase.Began)
//			{
//				// Ray for storing a point in space and a directional vector. 
//				// ScreenPointToRay converts touch positions from the 2D space of the screen
//				// to the 2D space of the world. 
//				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(i).position);
//				RaycastHit hit;
//
//
//
//				if(Physics.Raycast(ray, out hit, Mathf.Infinity, touchMask))
//				{
//					//if ray intersected a collider and we are not already dragging
//					//Debug.Log("began drag", i);
//					Debug.Log(hit.collider.gameObject.name);
//					Debug.Log(hit.point);
//					SelectedObjects.Add(new RegisteredTouch(hit.transform.gameObject, i));
//
//
////
////					if (hit.collider.tag == "Draggable")
////					{
////						//theObjectCollection
////						//A draggable object has been touched. 
////						//sends a message to the game object to run 'Selected' function
////
////						SelectedObjects.Add(new SelectedObject(hit.transform.gameObject, i));
////
////						SelectedObjects[i].Node.SendMessage ("Selected");
////
////					}
////					if (hit.collider.tag == "Rotatable")
////					{
////						//theObjectCollection
////						//A draggable object has been touched. 
////						//sends a message to the game object to run 'Selected' function
////						
////						SelectedObjects.Add(new SelectedObject(hit.transform.gameObject, i));
////						SelectedObjects[i].Node.SendMessage ("Selected");
////						rotatable = true;
////						rotateID = i;
////					}
//				}
//			}
//			if(Input.GetTouch(i).phase == TouchPhase.Moved)
//			{
//
//				 //Input.GetTouch(i).deltaTime
//			}
//			if(Input.GetTouch(i).phase == TouchPhase.Canceled)
//			{
//
//			}
////			if(Input.GetTouch(i).phase == TouchPhase.Began && rotatable)
////			{
////				int originLocation = CheckList(rotateID); 
////				if(i!=originLocation)
////				{
////				//rotate object
////				Debug.Log("Rotating");
////				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(i).position);
////				RaycastHit hit;
////
////				Vector3 rotatePoint;
////				Vector3 originPoint; 
////				Vector3 rotateDir;
////				Vector2 rotateDirection;
////				originPoint = SelectedObjects[originLocation].GetPosition();
////
////
////				if(Physics.Raycast(ray, out hit, Mathf.Infinity))
////				{
////					rotateDir = hit.point - originPoint;
////					rotateDirection.x = rotateDir.x;
////					rotateDirection.y = rotateDir.z;
////					float angle = Mathf.Atan2(rotateDirection.x, rotateDirection.y) * Mathf.Rad2Deg;
////					SelectedObjects[originLocation].Rotate(angle);
////					Debug.Log(angle);
////				}
////				}
//			if(Input.GetTouch(i).phase == TouchPhase.Moved)
//			{
//				//drag the object
//				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(i).position);
//				RaycastHit hit;
//				if(Physics.Raycast(ray, out hit, Mathf.Infinity))
//				{
//					if(hit.collider.tag == "Ground")
//					{
//						int selected = CheckList(i);
//
//						if(selected == i)
//							SelectedObjects[selected].Move(hit.point);
//					}
//				}
//			}
//			if(Input.GetTouch(i).phase == TouchPhase.Ended)
//			{
//				int selected = CheckList(i);
//				SelectedObjects[selected].Node.SendMessage ("Deselected");
//				SelectedObjects.RemoveAt(selected);
//
//				rotatable = false;
//			}
//		}
//	}
//	}
	}
}