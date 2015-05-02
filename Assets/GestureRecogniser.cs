using UnityEngine;
using System.Collections;

public class GestureRecogniser : MonoBehaviour 
{
	//Gesture bool. Will be returned if rotation is active.
	bool gesturing = false;

	//rotation
	bool rotating;
	Vector2 startVector;
	float rotGestureWidth = 1000f;
	float rotAngleMinimum = 1f;
	float previousAngle = 0;
	public Formation form;

	bool CheckGesturing()
	{
		return gesturing;
	}

	void Update()
	{
		
		if(Input.touchCount == 2)
		{
			gesturing = true;

			Touch touch0 = Input.GetTouch (0);
			Touch touch1 = Input.GetTouch (1);
			if(!rotating)
			{
				startVector = touch0.position - touch1.position;
				rotating = true;
				previousAngle = 0;
			}
			else
			{
				Vector2 currVector = touch0.position - touch1.position; 
				float angleOffset = Vector2.Angle (startVector, currVector);
				Vector3 LR = Vector3.Cross (startVector,currVector);
				
				if (angleOffset > rotAngleMinimum)
				{
					float newAngle;
					if (LR.z > 0)
					{
						//Anticlockwise turn
						newAngle = angleOffset - previousAngle;
						Vector3 rotVector = new Vector3();
						rotVector.Set(0, -newAngle, 0);
						if(form != null)
						{
							form.transform.Rotate(rotVector);
						}
					}
					else if (LR.z < 0) 
					{ 
						//Clockwise turn
						newAngle = angleOffset - previousAngle;
						Vector3 rotVector = new Vector3();
						rotVector.Set(0, newAngle, 0);
						if(form != null)
						{
							form.transform.Rotate(rotVector);
						}
					}
				}
				previousAngle = angleOffset;
			} 
		}
		else
		{
			gesturing = false;
			rotating = false;
		}
	}
}
