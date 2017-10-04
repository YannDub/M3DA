using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLine : MonoBehaviour {

	private List<Vector3> position;
	public Color color;

	// Use this for initialization
	void Start () {
		LineRenderer line = this.gameObject.AddComponent<LineRenderer> ();
		line.material = new Material(Shader.Find("Particles/Additive"));
		line.widthMultiplier = 0.2f;
		position = new List<Vector3> ();
		//this.setSegment ();
		this.setCircle(4);
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer line = this.gameObject.GetComponent<LineRenderer> ();
		line.material.color = color;
		line.positionCount = position.Count;
		line.SetPositions (position.ToArray ());
	}

	void setSegment() {
		position.Add (new Vector3 (-2,0,0));
		position.Add (new Vector3 (2,0,0));
	}

	void setCircle(float r) {
		float angle = 0;
		for (int i = 0; i < 30; i++) {
			position.Add (new Vector3(r * Mathf.Cos(Mathf.Deg2Rad * angle), r * Mathf.Sin(Mathf.Deg2Rad * angle), 0));
			angle += 360.0f / 30.0f;
		}
		position.Add (new Vector3 (r, 0, 0));
	}
}
