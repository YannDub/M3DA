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

		LineRenderer line = this.gameObject.GetComponent<LineRenderer> ();
		line.numPositions = position.Count;
		line.SetPositions (position.ToArray ());
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
