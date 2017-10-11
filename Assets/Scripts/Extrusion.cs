using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extrusion : MonoBehaviour {

	private Mesh mesh;
	public InteractiveLine path, section;

	private Vector3[] position;
	private int lastCount, lastCountSec;

	// Use this for initialization
	void Start () {
		GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		mesh.name = "Extrusion";
		//path.setSegment ();
		path.setCircle(3);
		section.setCircle (1);
		this.ExtrudeLine ();
	}
	
	// Update is called once per frame
	void Update () {
		if (path.getPosition ().Count != lastCount) {
			mesh.Clear ();
			if (path.getPosition ().Count >= 2) {
				//this.ExtrudeLine ();
				this.ExtrudeSpline();
			}
			Debug.Log ("Test");
		}

		if (section.getPosition ().Count != lastCountSec) {
			mesh.Clear ();
			if (section.getPosition ().Count >= 2) {
				//this.ExtrudeLine ();
				this.ExtrudeSpline ();
			}
		}
		lastCount = path.getPosition ().Count;
		lastCountSec = section.getPosition ().Count;
	}

	void ExtrudeLine() {
		//section.setCircle (1);
		int stacks = path.getPosition ().Count;
		int slices = section.getPosition ().Count;
		int[] triangles = new int[3 * 2 * slices * (stacks - 1) * 2];

		position = new Vector3[stacks * slices];

		for (int y = 0; y < stacks; y++) {
			for (int x = 0; x < slices; x++) {
				Vector3 p = section.getPosition () [x];
				Vector3 p1 = path.getPosition () [y];

				Vector3 dir = path.tangentLine(y);

				Quaternion rotation = Quaternion.FromToRotation (Vector3.up, dir);

				Vector3 correctPoint = rotation * new Vector3 (p.x, 0, p.y);

				position [x + y * slices] = correctPoint + p1;
			}
		}

		int i = 0;
		for (int y = 0; y < stacks - 1; y++) {
			for (int x = 0; x < slices - 1; x++) {
				int bottomLeft = x + y * slices;
				int bottomRight = (x + 1) + y * slices;
				int topLeft = bottomLeft + slices;
				int topRight = bottomRight + slices;

				triangles [i] = topLeft;
				triangles [i + 1] = bottomLeft;
				triangles [i + 2] = bottomRight;

				i += 3;

				triangles [i] = topLeft;
				triangles [i + 1] = bottomRight;
				triangles [i + 2] = bottomLeft;

				i += 3;

				triangles [i] = bottomRight;
				triangles [i + 1] = topRight;
				triangles [i + 2] = topLeft;

				i += 3;

				triangles [i] = bottomRight;
				triangles [i + 1] = topLeft;
				triangles [i + 2] = topRight;

				i += 3;
			}
		}

		mesh.Clear ();
		mesh.vertices = position;
		mesh.triangles = triangles;
	}

	void ExtrudeSpline() {
		int stacks = 201;
		int slices = section.getPosition ().Count;
		int[] triangles = new int[3 * 2 * slices * (stacks - 1) * 2];

		position = new Vector3[stacks * slices];

		for (int y = 0; y < stacks; y++) {
			for (int x = 0; x < slices; x++) {
				Vector3 p = section.getPosition () [x];
				Vector3 p1 = path.PointSpline ((y * 1.0f) / (stacks * 1.0f));

				Vector3 dir = path.TangentSpline((y * 1.0f) / (stacks * 1.0f));
				Quaternion rotation = Quaternion.FromToRotation (Vector3.up, dir);

				Vector3 correctPoint = rotation * new Vector3 (p.x, 0, p.y);

				position [x + y * slices] = correctPoint + p1;
			}
		}

		int i = 0;
		for (int y = 0; y < stacks - 1; y++) {
			for (int x = 0; x < slices - 1; x++) {
				int bottomLeft = x + y * slices;
				int bottomRight = (x + 1) + y * slices;
				int topLeft = bottomLeft + slices;
				int topRight = bottomRight + slices;

				triangles [i] = topLeft;
				triangles [i + 1] = bottomLeft;
				triangles [i + 2] = bottomRight;

				i += 3;

				triangles [i] = topLeft;
				triangles [i + 1] = bottomRight;
				triangles [i + 2] = bottomLeft;

				i += 3;

				triangles [i] = bottomRight;
				triangles [i + 1] = topRight;
				triangles [i + 2] = topLeft;

				i += 3;

				triangles [i] = bottomRight;
				triangles [i + 1] = topLeft;
				triangles [i + 2] = topRight;

				i += 3;
			}
		}

		mesh.Clear ();
		mesh.vertices = position;
		mesh.triangles = triangles;
	}

	void OnDrawGizmos() {
		if (position == null)
			return;
		Gizmos.color = Color.red;
		for (int i = 0; i < position.Length; ++i) {
			Gizmos.DrawSphere (transform.TransformPoint(position [i]), 0.02f);
		}
	}
}
