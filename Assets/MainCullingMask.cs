using UnityEngine;
using System.Collections;

public class MainCullingMask : MonoBehaviour 
{
	void Start()
	{
		Camera.main.cullingMask = 1 << 9 | 1 << 11;
	}
}
