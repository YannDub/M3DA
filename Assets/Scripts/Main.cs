using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Camera orthoCam;
	public Camera perspCam;
	public GameObject section;
	public GameObject path;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)) {
			orthoCam.enabled = true;
			perspCam.enabled = false;
			section.GetComponent<LineRenderer> ().enabled = true;
			path.GetComponent<LineRenderer> ().enabled = false;
		} if (Input.GetKeyDown (KeyCode.F2)) {
			orthoCam.enabled = false;
			perspCam.enabled = true;
			section.GetComponent<LineRenderer> ().enabled = false;
			path.GetComponent<LineRenderer> ().enabled = true;
		}
	}
}
