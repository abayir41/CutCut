using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public GameObject[] SpawnObjeleri;
	public float Timer,SpawnSuresi;
	public static int Basla,ScoreBirler,ScoreBinler,BaslaTekKullan,ElemeModCarpi,ScoreTexBirler;
	public GameObject SkorTextGo;
	private Text SkorText;
	public static float IkiKat,Yavaslat;
	public static bool IkiKatBool,YavaslatBool,Bitti;
	public static Color YaziRenk;
	private int Basladimi;
	public static int ModeInts;
	private bool BaslaBool,OrtayaGetir;
	private string ModeString;
	public GameObject SureUI,SolCarpi,Carpi,SagCarpi,Orijin,SureUIback;
	private Image SolCarpiImag,CarpiImag,SagCarpiImag;
	private Color SolCarpiColor,CarpiColor,SagCarpiColor;
	private float TimeModedk, TimeModesn;
	private Vector3 SkorVec,orijinVec;
	private Text TimeModSureText;
	private float yyonu, xyonu;
	private float AraTimer;
	private bool Bekle;
	public int ModSayisi;
	public Text ModText;
	public GameObject[] OyunBasladigindayokolcak;
	private float Startfadeout,EndFadein,ScoreFadeout;
	public Tutma Sol, Sag;
	public Text Freeze;
	private bool Kapaac;
	public GameObject InfoPanel;
	public Toggle Ses;
	public AudioClip Buttonaudio,Alarm,CarpiSes;
	public Text ZamanYuksek, ElemeYuksek;
	private int ZZamanBinler,ElemeBinler,ZamanBirler,ElemeBirler;
	public GameObject Pause,pausePanel,Ikikattetxt;
	public bool Durbool;
	private int reklamgoster;
	void Start(){
		//cache
		SkorText = SkorTextGo.GetComponent<Text>();
		SkorVec = SkorTextGo.transform.position;
		orijinVec = Orijin.transform.position;
		yyonu = SkorVec.y - orijinVec.y;
		xyonu = SkorVec.x - orijinVec.x;
		TimeModSureText = SureUI.GetComponent<Text> ();
		SolCarpiImag = SolCarpi.GetComponent<Image> ();
		CarpiImag = Carpi.GetComponent<Image> ();
		SagCarpiImag = SagCarpi.GetComponent<Image> ();
		SolCarpiColor = SolCarpiImag.color;
		CarpiColor = CarpiImag.color;
		SagCarpiColor = SagCarpiImag.color;
		Startfadeout = 1;
		ScoreFadeout = 1;
		//ses
		if (PlayerPrefs.GetInt("Ses") == 0){
			Ses.isOn = true;
		}else if (PlayerPrefs.GetInt("Ses") == 1){
			Ses.isOn = false;
		}
		//İşlem
		ScoreBirler = 0;
		ScoreBinler = 0;
		YaziRenk = new Color (1, 1, 1, 1);
		TimeModedk = 2;
		TimeModesn = 0;
		if (ModeInts == 0){
			ModeString = "Mode:\nTime";
			SureUIback.SetActive (true);
			SureUI.SetActive (true);
			SolCarpi.SetActive (false);
			Carpi.SetActive (false);
			SagCarpi.SetActive (false);
		}else if(ModeInts == 1){
			ModeString = "Mode:\nElenme";
			SureUIback.SetActive (false);
			SureUI.SetActive (false);
			SolCarpi.SetActive (true);
			Carpi.SetActive (true);
			SagCarpi.SetActive (true);
		}
		ModText.text = ModeString;
		ZamanBirler = PlayerPrefs.GetInt ("ZBirler");
		ElemeBirler = PlayerPrefs.GetInt ("EBirler");
	}
	void Update () {
		//skor
		if (ModeInts == 0){
			SpawnSuresi = 1;
			if (ScoreTexBirler > PlayerPrefs.GetInt("ZBirler")){
				PlayerPrefs.SetInt ("ZBirler", ScoreTexBirler);
				ZamanBirler = ScoreTexBirler;
			}
		}else if (ModeInts == 1){
			SpawnSuresi = 1.1f;
			if (ScoreTexBirler > PlayerPrefs.GetInt("EBirler")){
				PlayerPrefs.SetInt ("EBirler", ScoreTexBirler);
				ElemeBirler = ScoreTexBirler;
			}
		}
	    ZamanYuksek.text = ZamanBirler.ToString ();
		ElemeYuksek.text = ElemeBirler.ToString ();



		SkorText.color = YaziRenk;
		if (ScoreBirler >= 1000f){
			ScoreBinler++;
			ScoreBirler -= 1000;
		}
		if (ScoreBinler >= 1) {
			if (ScoreBirler < 10) {
				SkorText.text = ScoreBinler + ",00" + ScoreBirler;
			} else if (ScoreBirler < 100) {
				SkorText.text = ScoreBinler + ",0" + ScoreBirler;
			} else {
				SkorText.text = ScoreBinler + "," + ScoreBirler;
			}
		} else {
			SkorText.text = ScoreBirler.ToString ();
		}
		//Efektler
		if (IkiKat > 0){
			IkiKatBool = true;
		}
		if (IkiKatBool){
			Ikikattetxt.SetActive (true);
			IkiKat -= Time.deltaTime;
			if (IkiKat < 0){
				Ikikattetxt.SetActive (false);
				IkiKatBool = false;
			}
		}
		if (Yavaslat > 0){
			YavaslatBool = true;

		}
		if(!Durbool){
			if (YavaslatBool) {
				Yavaslat -= Time.deltaTime * 2;
				Time.timeScale = 0.5f;
				int Yaz = (int)Yavaslat;
				Freeze.text = Yaz.ToString();
				if (Yavaslat < 0) {
					Freeze.text = "";
					YavaslatBool = false;
				}
			} else {
				Time.timeScale = 1;
			}
		}

		//Oynanış
		if (Basla > 1 && !Bitti){
			Timer += Time.deltaTime;
			if (ModeInts == 0) {
				if (!OrtayaGetir){
					if (TimeModesn < 10) {
						TimeModSureText.text = TimeModedk + ":0" + (int)TimeModesn;
					} else {
						TimeModSureText.text = TimeModedk + ":" + (int)TimeModesn;
					}
					TimeModesn -= Time.deltaTime;
					if (TimeModesn < 0){
						if (TimeModedk == 0) {
							SkorText.alignment = TextAnchor.MiddleCenter;
							OrtayaGetir = true;
							Yavaslat = 0;
							if (PlayerPrefs.GetInt("Ses") == 0){
								GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
								camera.GetComponent<AudioSource> ().pitch = 1;
								camera.GetComponent<AudioSource> ().PlayOneShot (Alarm);	
							}
							AraTimer = 5;
							Bitti = true;
						} else {
							TimeModedk -= 1;
							TimeModesn = 60;
						}
					}	
				}
			}
			if (ModeInts == 1) {
				if (!OrtayaGetir) {
					if (ElemeModCarpi == 1) {
						SolCarpiImag.color = new Color (SolCarpiColor.r, SolCarpiColor.g, SolCarpiColor.b, 1);
					}else if(ElemeModCarpi == 2){
						SolCarpiImag.color = new Color (SolCarpiColor.r, SolCarpiColor.g, SolCarpiColor.b, 1);
						CarpiImag.color = new Color (CarpiColor.r, CarpiColor.g, CarpiColor.b, 1);
					}else if (ElemeModCarpi >= 3){
						SolCarpiImag.color = new Color (SolCarpiColor.r, SolCarpiColor.g, SolCarpiColor.b, 1);
						CarpiImag.color = new Color (CarpiColor.r, CarpiColor.g, CarpiColor.b, 1);
						SagCarpiImag.color = new Color (SagCarpiColor.r,SagCarpiColor.g,SagCarpiColor.b,1);
						SkorText.alignment = TextAnchor.MiddleCenter;
						OrtayaGetir = true;
						Yavaslat = 0;
						if (PlayerPrefs.GetInt("Ses") == 0){
							GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
							camera.GetComponent<AudioSource> ().pitch = 1;
							camera.GetComponent<AudioSource> ().PlayOneShot (CarpiSes);	
						}
						AraTimer = 5;
						Bitti = true;
					}
				}
			}
		}
		if (OrtayaGetir){
			if (!Bekle){
				AraTimer -= Time.deltaTime;
			}
			Pause.SetActive (false);
			pausePanel.SetActive (false);
			if (SkorTextGo.transform.position.x > orijinVec.x) {
				if (AraTimer < 3.5f){
					SkorTextGo.transform.Translate (xyonu * -Time.deltaTime, yyonu * -Time.deltaTime, 0);
					Bekle = true;
				}
			} else {
				Bekle = false;
				if (AraTimer < 3f) {
					SkorTextGo.transform.Translate (0, yyonu * Time.deltaTime / 7, 0);
					if (EndFadein < 1){
						EndFadein += Time.deltaTime;
					}
					EndFadein += Time.deltaTime;
					for (int i = 0; i < OyunBasladigindayokolcak.Length; i++) {
						if (EndFadein < 1) {
							if (OyunBasladigindayokolcak [i].GetComponent<Image> () != null) {
								OyunBasladigindayokolcak [i].SetActive (true);
								Color Renk = OyunBasladigindayokolcak [i].GetComponent<Image> ().color;
								OyunBasladigindayokolcak [i].GetComponent<Image> ().color = new Color (Renk.r, Renk.g, Renk.b, EndFadein);
							} else if (OyunBasladigindayokolcak [i].GetComponent<Text> () != null) {
								OyunBasladigindayokolcak [i].SetActive (true);
								Color Renk = OyunBasladigindayokolcak [i].GetComponent<Text> ().color;
								OyunBasladigindayokolcak [i].GetComponent<Text> ().color = new Color (Renk.r, Renk.g, Renk.b, EndFadein);
							}
						}
					}
					if (ScoreFadeout > 0) {
						ScoreFadeout -= Time.deltaTime / 2;
						SkorText.color = new Color (YaziRenk.r, YaziRenk.g, YaziRenk.b, ScoreFadeout);
					} else {

						// reset
						reklamgoster++;
						SkorTextGo.transform.position = SkorVec;
						SkorText.color = YaziRenk;
						Startfadeout = 1;
						ScoreFadeout = 1;
						SkorText.alignment = TextAnchor.MiddleRight;
						ScoreBirler = 0;
						ScoreBinler = 0;
						TimeModedk = 2;
						TimeModesn = 0;
						ElemeModCarpi = 0;
						IkiKat = 0;
						SolCarpiImag.color = new Color (SolCarpiColor.r, SolCarpiColor.g, SolCarpiColor.b, 0.5f);
						CarpiImag.color = new Color (CarpiColor.r, CarpiColor.g, CarpiColor.b, 0.5f);
						SagCarpiImag.color = new Color (SagCarpiColor.r,SagCarpiColor.g,SagCarpiColor.b,0.5f);
						AraTimer = 5;
						Sol.Ilkkeztutuldumu = false;
						Sag.Ilkkeztutuldumu = false;
						Sol.SapAra = true;
						Sag.SapAra = true;
						Bitti = false;
						Basla = 0;
						ScoreTexBirler = 0;
						GetComponent<Kilic> ().Sifirla ();
						EndFadein = 0;
						Pause.SetActive (false);
						OrtayaGetir = false;

					}
				} 
			}
		}

		//spawn
		if (Timer > SpawnSuresi) {
			Instantiate (SpawnObjeleri[Random.Range(0,SpawnObjeleri.Length)]);
			Timer = 0;
		}

		//fonsiyon
		if (BaslaTekKullan > 1){
			Pause.SetActive (true);
			Startfadeout -= Time.deltaTime;
			for (int i = 0; i < OyunBasladigindayokolcak.Length; i++) {
				if (Startfadeout > 0) {
					if (OyunBasladigindayokolcak [i].GetComponent<Image> () != null) {
						Color Renk = OyunBasladigindayokolcak [i].GetComponent<Image> ().color;
						OyunBasladigindayokolcak [i].GetComponent<Image> ().color = new Color (Renk.r, Renk.g, Renk.b, Startfadeout);
					} else if (OyunBasladigindayokolcak [i].GetComponent<Text> () != null) {
						Color Renk = OyunBasladigindayokolcak [i].GetComponent<Text> ().color;
						OyunBasladigindayokolcak [i].GetComponent<Text> ().color = new Color (Renk.r, Renk.g, Renk.b, Startfadeout);
					}
				} else {
					OyunBasladigindayokolcak [i].SetActive (false);
				}
			}
			if (Startfadeout < 0){
				BaslaTekKullan = 0;
			}
		}

		//Ses
		if (Ses.isOn) {
			PlayerPrefs.SetInt ("Ses", 0);
		} else {
			PlayerPrefs.SetInt ("Ses", 1);
		}
	}





	public void ModDeistir(){
		if (ModeInts == ModSayisi - 1) {
			ModeInts = 0;
		} else {
			ModeInts++;
		}
		ModDegisimi ();
	}
	void ModDegisimi(){
		if (ModeInts == 0){
			ModeString = "Mode:\nTime";
			SureUIback.SetActive (true);
			SureUI.SetActive (true);
			SolCarpi.SetActive (false);
			Carpi.SetActive (false);
			SagCarpi.SetActive (false);
			TimeModedk = 2;
			TimeModesn = 0;
		}else if(ModeInts == 1){
			ModeString = "Mode:\nNormal";
			SureUIback.SetActive (false);
			SureUI.SetActive (false);
			SolCarpi.SetActive (true);
			Carpi.SetActive (true);
			SagCarpi.SetActive (true);
		}
		ModText.text = ModeString;
	}
	public void Baslat(){
		Basla = 2;
		BaslaTekKullan = 2;
	}
	public void Info(){
		Kapaac = !Kapaac;
		InfoPanel.SetActive (Kapaac);
	}
	public void ButonSes(){
		if (PlayerPrefs.GetInt("Ses") == 0){
			GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
			camera.GetComponent<AudioSource> ().pitch = 1;
			camera.GetComponent<AudioSource> ().PlayOneShot (Buttonaudio);	
		}
	}
	public void Dur (){
		Time.timeScale = 0;
		Durbool = true;
		pausePanel.SetActive (true);
	}
	public void Devam(){
		Time.timeScale = 1;
		Durbool = false;
		pausePanel.SetActive (false);
	}
	public void Ayrıl(){
		SkorText.alignment = TextAnchor.MiddleCenter;
		OrtayaGetir = true;
		Durbool = false;
		Yavaslat = 0;
		AraTimer = 5;
		Bitti = true;
	}
}