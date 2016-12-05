using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityStandardAssets.ImageEffects;

public class Pedido : MonoBehaviour {

	// Use this for initialization
	public int borracho, angulo;
	public string[] IngNames;
	public int[] Ings;
	public int[] pedido;
	public Image Ing1, Ing2,Ing3;
	public Sprite[] IngsIm;
	public Image[] IngPant;
	public float TimerBo;
	public Transform camara, original;
	public Sprite[] IngSprites; 
	public int[] Cogidos;


	public AudioClip audio1;
	public AudioClip audio2;




	void Start () {
		angulo = 0;
		borracho = 0;
		TimerBo = 0;
		original = camara;
		IngPant = new Image[]{ Ing1, Ing2, Ing3 };
		IngNames = new string[]{"Alcohol","Berries","Ice Cubes","Lima","Mint"};
		Ings= new int[]{0,1,2,3,4};
		pedido= new int[]{0,0,0};
		Cogidos= new int[]{0,0,0};
		CrearPedido ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (TimerBo > 0) {
			TimerBo -= Time.deltaTime;
			camara.position = new Vector3(camara.position.x,3+Mathf.Sin(TimerBo*2)*0.25f,-10);
			var angulos = original.eulerAngles;
			angulos.z = Mathf.Sin (TimerBo*2)*5;
			camara.rotation = Quaternion.Euler(angulos);

		}
		
	}
	void CrearPedido(){

		Cogidos= new int[]{0,0,0};

		int u, i, e;
		u = Random.Range (0, Ings.Length);
		i = Random.Range (0, Ings.Length);
		e = Random.Range (0, Ings.Length);

		while (u == i) {
			i = Random.Range (0, Ings.Length);
		}
		while (u == e || i==e ) {
			e = Random.Range (0, Ings.Length);
		}

		foreach(Image t in IngPant){
			t.color = Color.white;
		}

		pedido [0] = Ings [u];
		Ing1.sprite = IngsIm [u];

		pedido [1] = Ings [i];
		Ing2.sprite = IngsIm [i];

		pedido [2] = Ings [e];
		Ing3.sprite = IngsIm [e];

		Debug.Log ("Pedido: " + u + " "+IngNames [u]+ " " + i + " "+IngNames [i] + " " +IngNames [e]+ " "+ e);
	}

	public bool Corroborar(int p){
		bool bien = false;
		for (int i = 0; i < pedido.Length; i++) {
			if (p == pedido [i]) {
				Cogidos [i] = 1;
			//	Debug.Log ("pos " + i + " Recogido " + p + " Ing " + pedido [i]); 
				IngPant[i].color = Color.green;

				camara.GetComponent<AudioSource>().clip = audio2;
				camara.GetComponent<AudioSource> ().Play ();

				bien = true;
			}

			}
		if (!bien) {
			Ingr.parado = true;
			foreach(Image t in IngPant){
				t.color = Color.red;
			}
			//Debug.Log ("Parado");
		}
		bool nuevopedido = true;
		for (int i = 0; i < IngPant.Length; i++) {
			if (IngPant[i].color != Color.green) {
				nuevopedido = false;
			}
		}
			if (nuevopedido) {

				//meter puntuacion
				//CrearPedido();  //Pasar a la siguiente escena



			Application.LoadLevel("Mezclar");

			}
		return bien;
	}
		

	public void Beber()
	{
		Debug.Log ("Has bebido");

		camara.GetComponent<AudioSource>().clip = audio1;
		camara.GetComponent<AudioSource> ().Play ();

		CrearPedido ();
		GameObject.Find ("Main Camera").GetComponent<Ingr> ().CrearIngerd ();
		Ingr.parado = false;
		if (borracho < 3) {
			borracho++;
			Borrachera ();
			StartCoroutine ("Metabolizar");

		}
	}

	void Borrachera()
	{
		switch (borracho) {

		case 1:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = true;
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().extraBlur = true;
			break;

		case 2:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().extraBlur = true;
			break;

		default:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = false;
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().extraBlur = false;
			break;
		}

	}

	IEnumerator Metabolizar(){
		TimerBo = 8;
		yield return new WaitForSeconds (8);
		borracho--;
		Borrachera ();
		camara.position = new Vector3(camara.position.x,3,-10);
		camara.rotation = original.rotation;
	}

	public bool CorroborarRandom(int p){
		bool bien = false;
		for (int i = 0; i < pedido.Length; i++) {
			if (p == pedido [i] && Cogidos [i].Equals (0)) {
				bien = true;
			}
		}
		Debug.Log(bien.ToString());
		return bien;
	}


}