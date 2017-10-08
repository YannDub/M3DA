using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Camera orthoCam;
	public Camera perspCam;
	public GameObject section;
	public GameObject path;
	public GameObject extrusion;

	// Use this for initialization
	void Start () {
		orthoCam.enabled = true;
		perspCam.enabled = false;
		section.GetComponent<LineRenderer> ().enabled = true;
		path.GetComponent<LineRenderer> ().enabled = false;
		extrusion.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)) {
			orthoCam.enabled = true;
			perspCam.enabled = false;
			section.GetComponent<LineRenderer> ().enabled = true;
			path.GetComponent<LineRenderer> ().enabled = false;
			extrusion.GetComponent<MeshRenderer> ().enabled = false;
		} if (Input.GetKeyDown (KeyCode.F2)) {
			orthoCam.enabled = true;
			perspCam.enabled = false;
			section.GetComponent<LineRenderer> ().enabled = false;
			path.GetComponent<LineRenderer> ().enabled = true;
			extrusion.GetComponent<MeshRenderer> ().enabled = false;
		} if (Input.GetKeyDown (KeyCode.F3)) {
			orthoCam.enabled = false;
			perspCam.enabled = true;
			section.GetComponent<LineRenderer> ().enabled = false;
			path.GetComponent<LineRenderer> ().enabled = false;
			extrusion.GetComponent<MeshRenderer> ().enabled = true;
		}
	}
}
