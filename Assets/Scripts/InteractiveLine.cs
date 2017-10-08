using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLine : MonoBehaviour {

	private List<Vector3> position;
	public Color color;

	// Use this for initialization
	void Start () {
		LineRenderer line = this.gameObject.AddComponent<LineRenderer> ();
		line.widthMultiplier = 0.2f;
		line.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name == "Path") {
			if (Input.GetKeyDown (KeyCode.X)) {
				position = new List<Vector3> ();
			}

			if (Input.GetMouseButtonDown (0)) {
				Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mouse.z = 0;
				position.Add (mouse);
			}
		}

		Vector3[] courbe = new Vector3[101];

		for (int i = 0; i < 101; i++) {
			courbe [i] = PointSpline ((i * 1.0f) / 100.0f);
			Debug.Log (courbe [i]);
		}

		LineRenderer line = this.gameObject.GetComponent<LineRenderer> ();
		//line.numPositions = position.Count;
		//line.SetPositions (position.ToArray ());
		line.numPositions = 101;
		line.SetPositions(courbe);
	}

	Vector3 PointSpline(float tNormalized) {
		int count = position.Count;
		if (tNormalized == 1) {
			return position [count - 1];
		} else {
			int i = (int)((count - 1) * tNormalized);

			Vector3 p0 = position [i];
			Vector3 p1 = position [i + 1];
			Vector3 t0 = tangentLine (i);
			Vector3 t1 = tangentLine (i + 1);

			float t = (tNormalized - (i / (count - 1.0f))) * (count - 1.0f);

			return (t * t * t) * (2.0f * p0 - 2.0f * p1 + t0 + t1) + 
				(t * t) * (-3.0f * p0 + 3.0f * p1 - 2.0f * t0 - t1) + 
				t * t0 + p0;
		}
	}

	Vector3 tangentLine(int i) {
		Vector3 tangent = new Vector3();
		Vector3 p = position [i];

		if (i == 0) {
			tangent = position [i + 1] - p;
		} else if (i == position.Count - 1) {
			tangent = p - position [i - 1];
		} else {
			Vector3 l = position [i - 1];
			Vector3 r = position [i + 1];
			tangent = r - l;
		}

		return tangent;
	}

	public void setSegment() {
		position = new List<Vector3> ();
		position.Add (new Vector3 (-2,-2,0));
		position.Add (new Vector3 (0,2,0));
		position.Add (new Vector3 (2,-1,0));
	}

	public void setCircle(float r) {
		position = new List<Vector3> ();
		float angle = 0;
		for (int i = 0; i < 30; i++) {
			position.Add (new Vector3(r * Mathf.Cos(Mathf.Deg2Rad * angle), r * Mathf.Sin(Mathf.Deg2Rad * angle), 0));
			angle += 360.0f / 30.0f;
		}
		position.Add (new Vector3 (r, 0, 0));
	}

	public List<Vector3> getPosition() {
		return this.position;
	}
}
