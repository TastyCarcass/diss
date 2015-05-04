using UnityEngine;
using System.Collections;

//Title: GestureManager
//Purpose; Recognises gestures and performs their function.

public class GestureManager : MonoBehaviour 
{
	//Gesture bool. Will be returned if rotation is active.
	bool gesturing = false;
	
	//rotation
	bool rotating; //Bool for if rotating
	Vector2 startVector; // Start vector of the gesture
	float rotAngleMinimum = 1f; // Minimum angle to register a rotation
	float previousAngle = 0; //Previous angle, needed to calculate difference
	public Formation form; //Moving formation
	
	bool CheckGesturing()
	{
		//Returns if a gesture is in process
		return gesturing;
	}//Check gesturing
	
	void Update()
	{
		//Two fingers required for a gesture
		if(Input.touchCount == 2)
		{
			gesturing = true; //bool for gesture
			
			Touch touch0 = Input.GetTouch (0); //Touches
			Touch touch1 = Input.GetTouch (1);
			if(!rotating)
			{
				//Initialises vectors and bools for beginning of gesture
				startVector = touch0.position - touch1.position;
				rotating = true;
				previousAngle = 0;
			} //if
			else
			{
				Vector2 currVector = touch0.position - touch1.position; //Current position vector.
				float angleOffset = Vector2.Angle (startVector, currVector); //Current angle.
				Vector3 LR = Vector3.Cross (startVector,currVector); //used to find if left or right
				
				if (angleOffset > rotAngleMinimum) //If distance is above minimum
				{
					float newAngle; //New angle to be applied. 
					if (LR.z > 0)
					{
						//Anticlockwise turn
						newAngle = angleOffset - previousAngle;
						Vector3 rotVector = new Vector3();
						rotVector.Set(0, -newAngle, 0);
						if(form != null)
						{
							form.transform.Rotate(rotVector);
						}//if
					}//if
					else if (LR.z < 0) 
					{ 
						//Clockwise turn
						newAngle = angleOffset - previousAngle;
						Vector3 rotVector = new Vector3();
						rotVector.Set(0, newAngle, 0);
						if(form != null)
						{
							form.transform.Rotate(rotVector);
						}//if
					}//if
				}//else
				previousAngle = angleOffset;
			} //else
		}
		else
		{
			gesturing = false;
			rotating = false;
		} //else
	}
}
