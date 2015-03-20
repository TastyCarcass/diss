using UnityEngine;
using System.Collections;

public class MeshUpdater : MonoBehaviour 
{
	public GameObject node0;
	public GameObject node1;
	public GameObject node2;
	public GameObject node3; 


	GameObject displayFormation;
	public Mesh fMesh;
	public Material mat;

	Vector3[] vertices;
	Vector3[] normals;
	Vector2[] uv;
	int[] tri;

	Vector3[] newVertices;
	Vector3[] newNormals;
	Vector2[] newuv;

	// Use this for initialization
	void Start () 
	{
		displayFormation = new GameObject();
		displayFormation.AddComponent<MeshFilter>();
		displayFormation.AddComponent<MeshRenderer>();

		fMesh = displayFormation.GetComponent<MeshFilter>().mesh;


		displayFormation.GetComponent<Renderer>().material.color = Color.yellow;
		//displayFormation.renderer.material = mat;

		vertices = new Vector3[4];
		normals = new Vector3[4];
		uv = new Vector2[4];
		tri = new int[6];

//
//		vertices[0] = new Vector3(1,1,1);
//		vertices[1] = new Vector3(1,1,-1);
//		vertices[2] = new Vector3(-1,1,1);
//		vertices[3] = new Vector3(-1,1,-1);

		vertices[0] = node0.transform.position;
		vertices[1] = node1.transform.position;
		vertices[2] = node2.transform.position;
		vertices[3] = node3.transform.position;
		
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);

//		tri[0] = 0;
//		tri[1] = 2;
//		tri[2] = 3;
//
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 3;
		tri[3] = 1;
		tri[4] = 2;
		tri[5] = 0;

		for(int i = 0; i < normals.Length; i++)
		{
			normals[i] = Vector3.up;
		}
		fMesh.vertices = vertices;
		fMesh.uv = uv;
		fMesh.normals = normals;
		fMesh.triangles = tri;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fMesh = displayFormation.GetComponent<MeshFilter>().mesh;
		fMesh.Clear();
	

		
		vertices[0] = node0.transform.position;
		vertices[0].y=0.05f;
		vertices[1] = node1.transform.position;
		vertices[1].y=0.05f;
		vertices[2] = node2.transform.position;
		vertices[2].y=0.05f;
		vertices[3] = node3.transform.position;
		vertices[3].y=0.05f;

		for(int i = 0; i < vertices.Length; i++)
		{
			vertices[i].y += 0.5f;
		}

//		tri[0] = 0;
//		tri[1] = 2;
//		tri[2] = 3;
//		tri[3] = 0;
//		tri[4] = 2;
//		tri[5] = 1;

		fMesh.vertices = vertices;
		fMesh.uv = uv;
		fMesh.triangles = tri;
	}
}
