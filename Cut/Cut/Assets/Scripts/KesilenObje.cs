using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KesilenObje : MonoBehaviour {
	private Transform trans,ChildTrans;
	private float Sw,Sh;
	private Camera cam; 
	private Animator anim;
	public AudioClip Bicak;
	private Kilic Aci;
	private bool KilicaDeydimi = false;
	public Color ustboya,Altboya,SpriteColor;
	public Color[] ustcocukboya, altcocukboya;
	private float Sure;
	private Transform Ust, Alt,ustcocugu,altcocugu;
	public Transform[] UstCocuklarineklentiler, AltCocuklarieklentileri;
	public GameObject Parentobje,Splash,MeshGo;
	private Transform ParentObje,SplashTrans;
	private SpriteRenderer SplashSprite;
	private bool SplashAc;
	public Transform CizgiGo;
	private LineRenderer Cizgi;
	public bool Belirleyici,Grup;
	public float EkranYuksekDusuk,EkranYuksekYuksek,EkranGenisDusuk,EkranGenisYuksek;
	public int Skor;
	public bool Yavaslatici,Katlandirici;


	void Start(){
		//Cached işlemi
		SplashTrans = Splash.GetComponent<Transform>();
		SplashSprite = Splash.GetComponent<SpriteRenderer>();
		SpriteColor = SplashSprite.color;
		trans = GetComponent<Transform> ();
		ChildTrans = trans.GetChild(0).GetComponent<Transform> ();
		cam = Camera.main;
		Sw = Screen.width;
		Sh = Screen.height;
		anim = trans.GetChild (0).GetComponent<Animator> ();
		Aci = GameObject.FindGameObjectWithTag ("Controller").GetComponent<Kilic> ();
		Sure = 1;
		Ust = ChildTrans.GetChild (0);
		Alt = ChildTrans.GetChild (1);
		ustcocugu = Ust.GetChild (0);
		altcocugu = Alt.GetChild (0);
		Cizgi = CizgiGo.GetComponent<LineRenderer> ();

		//fade out için gerekli
		ustboya = ustcocugu.GetComponent<MeshRenderer> ().material.color;
		Altboya = altcocugu.GetComponent<MeshRenderer> ().material.color;
		UstCocuklarineklentiler = new Transform[ustcocugu.childCount];
		AltCocuklarieklentileri = new Transform[altcocugu.childCount];
		ustcocukboya = new Color[ustcocugu.childCount];
		altcocukboya = new Color[altcocugu.childCount];
		if(Belirleyici){
			ParentObje = Parentobje.GetComponent<Transform> ();
		}
		for (int i = 0;i < ustcocugu.childCount;i++){
			UstCocuklarineklentiler [i] = ustcocugu.GetChild (i);
			ustcocukboya [i] = ustcocugu.GetChild (i).GetComponent<MeshRenderer> ().material.color;
		}
		for (int i = 0;i < altcocugu.childCount;i++){
			AltCocuklarieklentileri [i] = altcocugu.GetChild (i);
			altcocukboya [i] = altcocugu.GetChild (i).GetComponent<MeshRenderer> ().material.color;
		}
		if (Belirleyici){
			Grup = true;
		}
			
		//boya
//		boya = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
//		ChildTrans.GetChild (0).GetComponent<MeshRenderer> ().material.color = boya;
//		ChildTrans.GetChild (1).GetComponent<MeshRenderer> ().material.color = boya;

		//screen ölç
		float rsw = Random.Range (Sw * EkranGenisDusuk / 100, Sw * EkranGenisYuksek / 100);
		float rsh = Random.Range (Sh * EkranYuksekDusuk / 100, Sh * EkranYuksekYuksek / 100);

		//pos işle
		Vector3 Pos = new Vector3 (rsw,rsh,0);
		Pos = cam.ScreenToWorldPoint (Pos);
		Pos = new Vector3 (Pos.x, Pos.y - 27, 71);
		if (Belirleyici) {
			ParentObje.position = Pos;
		} else if (!Grup) {
			trans.position = Pos;
		}
			
		//açı ver
		rsw = Random.Range (Sw * EkranGenisDusuk / 100, Sw * EkranGenisYuksek / 100);
		Pos = new Vector3 (rsw, 0, 0);
		Pos = cam.ScreenToWorldPoint (Pos);
		Pos = new Vector3 (Pos.x, trans.position.y, 1);
		if (Belirleyici) {
			ParentObje.LookAt (Pos);
			ParentObje.eulerAngles = new Vector3 (180,trans.eulerAngles.y,180);
		} else if (!Grup){
			trans.LookAt (Pos);
			trans.eulerAngles = new Vector3 (180,trans.eulerAngles.y,180);
		}
			
		//anim speed
		anim.speed = 0.15f;

	}

	void Update (){
		anim.speed += Time.deltaTime / 5;
		if (KilicaDeydimi) {
			Sure -= Time.deltaTime;
			for (int i = 0; i < ustcocukboya.Length; i++) {
				ustcocukboya [i] = new Color (ustcocukboya [i].r, ustcocukboya [i].g, ustcocukboya [i].b, Sure);
				UstCocuklarineklentiler [i].GetComponent<MeshRenderer> ().material.color = ustcocukboya [i];
			}
			for (int i = 0; i < altcocukboya.Length; i++) {
				altcocukboya [i] = new Color (altcocukboya [i].r, altcocukboya [i].g, altcocukboya [i].b, Sure);
				AltCocuklarieklentileri [i].GetComponent<MeshRenderer> ().material.color = altcocukboya [i];
			}
			ustboya = new Color (ustboya.r, ustboya.g, ustboya.b, Sure);
			Altboya = new Color (Altboya.r, Altboya.g, Altboya.b, Sure);
			ustcocugu.GetComponent<MeshRenderer> ().material.color = ustboya;
			altcocugu.GetComponent<MeshRenderer> ().material.color = Altboya;
			Ust.position += new Vector3 (0, 1, 0) * Time.deltaTime / 2;
			Alt.position += new Vector3 (0, -1, 0) * Time.deltaTime / 2;
			Ust.localScale += new Vector3 (1, 1, 1) * Time.deltaTime / 4;
			Alt.localScale += new Vector3 (1, 1, 1) * Time.deltaTime / 4;
			if (Sure <= 0) {
				if (Belirleyici){
					Destroy (Parentobje,7f);
				}
				Destroy (gameObject);
			}


		} else{


			float z = 1 / Mathf.Abs(ChildTrans.position.z);
			z *= 10;
			if (z > 1){
				z = 1;
			}
			if (z > 0.50f) {
				Cizgi.SetPosition (1, new Vector3 (0, 0, z * 15));
			} else {
				Cizgi.SetPosition (1, new Vector3 (0, 0, z * 20));
			}
			Cizgi.widthMultiplier = z * 1.2f;
			ChildTrans.localScale = new Vector3 (z, z, z);


		}
		if (SplashAc) {
			Sure -= Time.deltaTime;
			SplashSprite.color = new Color (SpriteColor.r, SpriteColor.g, SpriteColor.b, Sure);
			if (Sure <= 0) {
				if (Belirleyici) {
					Destroy (Parentobje, 1f);
				}
				Destroy (gameObject);
			}
		} else {
			SplashTrans.position = ChildTrans.position;
		}
		if(Controller.Bitti){
			if (Belirleyici) {
				Destroy (Parentobje);
			}
			Destroy (gameObject);
		}
	}



	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.tag == "Kilic") {
			if (!KilicaDeydimi){
				if (Aci.Aci > 90) {
					ChildTrans.eulerAngles = new Vector3 (0, 0, Aci.Aci - 180);
				} else if (Aci.Aci < -90) {
					ChildTrans.eulerAngles = new Vector3 (0, 0, 180 + Aci.Aci);
				} else {
					ChildTrans.eulerAngles = new Vector3 (0, 0,Aci.Aci);
				}
				if (PlayerPrefs.GetInt("Ses") == 0){
					GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
					camera.GetComponent<AudioSource> ().pitch = Random.Range (1, 1.2f);
					camera.GetComponent<AudioSource> ().PlayOneShot (Bicak);	
				}
				Ust.eulerAngles = new Vector3 (Ust.eulerAngles.x + 10f, Ust.eulerAngles.y, Ust.eulerAngles.z);
				Alt.eulerAngles = new Vector3 (Alt.eulerAngles.x - 10f, Alt.eulerAngles.y, Alt.eulerAngles.z);
				anim.enabled = false;
				ChildTrans.position += new Vector3 (0, 0, -4);
				Controller.YaziRenk = ustboya;
				if (Controller.IkiKatBool) {
					Controller.ScoreBirler += Skor * 2;
					Controller.ScoreTexBirler += Skor * 2;
				} else {
					Controller.ScoreTexBirler += Skor;
					Controller.ScoreBirler += Skor;
				}
				if (Katlandirici){
					Controller.IkiKat = 10;
				}
				if (Yavaslatici){
					Controller.Yavaslat = 10;
				}
				if (Belirleyici){
					Destroy (Parentobje, 5f);
				}
			}
			KilicaDeydimi = true;
		} else if (!KilicaDeydimi){
			Splash.SetActive (true);
			MeshGo.SetActive (false);
			SplashAc = true;
			if (Controller.ModeInts == 1){
				Controller.ElemeModCarpi++;
			}
		}
	}
}
