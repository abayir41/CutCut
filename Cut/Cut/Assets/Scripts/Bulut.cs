using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulut : MonoBehaviour {
	private float sw;
	public float Soldan,Sagdan;
	private Vector3 SolVec,SagVec;
	public float Hiz;
	// Use this for initialization
	void Start () {
		sw = Screen.width;
		Vector3 pos = new Vector3 (sw * Soldan /100, 0, 0);
		SolVec = Camera.main.ScreenToWorldPoint (pos);
	    pos = new Vector3 (sw * Sagdan /100, 0, 0);
		SagVec = Camera.main.ScreenToWorldPoint (pos);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos2 = transform.position;
		transform.Translate (Hiz * Time.deltaTime,0,0);
		if (pos2.x <= SolVec.x ) {
			Hiz = 0.3f;
		}
		if (pos2.x >= SagVec.x) {
			Hiz = -0.3f;
		}
	}
}
