using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RenderPC : MonoBehaviour {
	public string fname;
	private List<Mesh> meshes;
	int numPoints = 65000;
	// Use this for initialization
	void Start () {
		//read csv
		var reader = new StreamReader(fname);

		while(!reader.EndOfStream){
		//put 60000 points
			Mesh mesh = new Mesh();
			GetComponent<MeshFilter> ().mesh = mesh;

			Vector3[] points = new Vector3[numPoints];
			int[] indices = new int[numPoints];
			Color[] colors = new Color[numPoints];
			int i = 0;
			while(i < numPoints){
				var line = reader.ReadLine ();
				var values = line.Split (',');
				//012 xyz, 345 bgr
				points[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
				indices [i] = i;
				colors [i] = new Color (float.Parse(values[5])/255,float.Parse(values[4])/255,float.Parse(values[3])/255);
				i++;
			}
			//do rendering
			mesh.vertices = points;
			mesh.colors = colors;
			mesh.SetIndices (indices, MeshTopology.Points, 0);
			meshes.Add (mesh);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
