using UnityEngine;
using System.Collections;

public class moveCam : MonoBehaviour
{
	//doesn't work lol
	public Camera cam;
	private Vector2 origin;
	public void touched(InputEventArgs e)
	{
		Vector2 origin = e.touchObject.position;
	}
	public void OnDrag(InputEventArgs e)
	{
		//e.touchObject.
		Vector2 moved = e.touchObject.position;
		Vector2 buf = new Vector2();
		buf = origin - moved;
		Vector3 buf2 = new Vector3 ();
		buf2.Set (buf.x, 0, buf.y);
		;buf2.Set (buf.x*Time.deltaTime, 0, buf.y*Time.deltaTime);
		cam.transform.position = cam.transform.position + buf2;
	}
}
