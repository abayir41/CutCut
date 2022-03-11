using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutma : MonoBehaviour
{
	private Transform trans;
	private Camera cam;
	RaycastHit[] hit,xddd;
	public bool SapAra;
	private int ParmakID;
	public int SapNo;
	public bool Ilkkeztutuldumu;
	public GameObject pick,ok;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		trans = GetComponent<Transform> ();
		SapAra = true;
	}
	void Update(){
		if (Input.touchCount == 0) {
			SapAra = true;
		}
		if (SapAra) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					Ray ray = cam.ScreenPointToRay (Input.GetTouch (i).position);
					hit = Physics.RaycastAll (ray, 100);
					for (int a = 0;a < hit.Length;a++){
						if (hit[a].collider.tag == "Sap" + SapNo) {
							if (!Ilkkeztutuldumu) {
								Controller.Basla++;
								Controller.BaslaTekKullan++;
								pick.SetActive (false);
								ok.SetActive (false);
								Ilkkeztutuldumu = true;
							}
							ParmakID = i;
							SapAra = false;
						}
					}

				}
			}
		} else {
			Vector3 pos = new Vector3 (Input.GetTouch (ParmakID).position.x, Input.GetTouch (ParmakID).position.y, 9);
			trans.position = cam.ScreenToWorldPoint (pos);
			if (Input.GetTouch(ParmakID).phase == TouchPhase.Ended){
				SapAra = true;
			}
		} 
	}
//	void OnMouseDrag(){
//		Vector3 mousepozisyon = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 9);
//
//		trans.position = cam.ScreenToWorldPoint (mousepozisyon);
//	}
}
