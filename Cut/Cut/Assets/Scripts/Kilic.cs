using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kilic : MonoBehaviour {
	public GameObject SolSap,SagSap,MetalKilic;
	private Transform SolSapChildTrans,SagSapChildTrans;
	private Transform SolSapTrans,SagSapTrans,KilicTrans;
	private float x,y,Kilicx,Kilicy;
	public float Aci;
	public float Kilicnere,Solx,Sagx;
	public GameObject Settings,Main;


	void Start () {
		SolSapTrans = SolSap.GetComponent<Transform> ();
		SagSapTrans = SagSap.GetComponent<Transform>();

		KilicTrans = MetalKilic.GetComponent<Transform> ();

		SolSapChildTrans = SolSap.transform.GetChild (0).GetComponent<Transform>();
		SagSapChildTrans = SagSap.transform.GetChild (0).GetComponent<Transform> ();
		float sh = Screen.height;
		float sw = Screen.width;
		Vector3 Pos = new Vector3 (sw * Solx / 100, sh * Kilicnere / 100, 9);
		Pos = Camera.main.ScreenToWorldPoint (Pos);
		SolSapTrans.position = Pos;
		Pos = new Vector3 (sw * Sagx / 100, sh * Kilicnere / 100, 9);
		Pos = Camera.main.ScreenToWorldPoint (Pos);
		SagSapTrans.position = Pos;
	}

	void Update () {
		//Açı Hesaplama
		y = SagSapTrans.transform.position.y - SolSapTrans.transform.position.y;
		x = SagSapTrans.transform.position.x - SolSapTrans.transform.position.x;
		Aci = Mathf.Rad2Deg * Mathf.Atan2 (y, x); 

		//Ortasını Hesaplama 
		Kilicy = SagSapTrans.transform.position.y + SolSapTrans.transform.position.y;
		Kilicx = SagSapTrans.transform.position.x + SolSapTrans.transform.position.x;

		SolSapChildTrans.eulerAngles = new Vector3(0,0,Aci);
		SagSapChildTrans.eulerAngles = new Vector3(0,0,Aci);

		KilicTrans.localScale = new Vector3 (100, 100, Vector3.Distance(SolSapTrans.position,SagSapTrans.position) * 50.5f);
		KilicTrans.eulerAngles = new Vector3(Aci * -1,90,0);
		KilicTrans.position = new Vector3 (Kilicx / 2,Kilicy / 2, KilicTrans.position.z);
	}
	public void Sifirla(){
		float sh = Screen.height;
		float sw = Screen.width;
		Vector3 Pos = new Vector3 (sw * Solx / 100, sh * Kilicnere / 100, 9);
		Pos = Camera.main.ScreenToWorldPoint (Pos);
		SolSapTrans.position = Pos;
		Pos = new Vector3 (sw * Sagx / 100, sh * Kilicnere / 100, 9);
		Pos = Camera.main.ScreenToWorldPoint (Pos);
		SagSapTrans.position = Pos;
	}
	public void Ayarlar (){
		SolSapTrans.position += new Vector3 (0, 1000, 0);
		SagSapTrans.position += new Vector3 (0, 1000, 0);
		Settings.SetActive (true);
		Main.SetActive (false);
	}
	public void AyarlarGeri(){
		Main.SetActive (true);
		Sifirla ();
		Settings.SetActive (false);
	}
}


/*Animator.GetCuurentAnimatorStateInfo(0).normalizedTime > 1  
Animasyon bittiğinin fonksiyonu */