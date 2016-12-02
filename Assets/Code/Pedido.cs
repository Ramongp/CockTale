using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityStandardAssets.ImageEffects;

public class Pedido : MonoBehaviour {

	// Use this for initialization
	public int borracho;
	public string[] IngNames;
	public int[] Ings;
	public int[] pedido;
	public Text Ing1, Ing2,Ing3;
	public Text[] Textos;



	void Start () {
		borracho = 0;
		IngNames = new string[]{"Alcohol","Berries","Ice Cubes","Lima","Mint"};
		Ings= new int[]{0,1,2,3,4};
		pedido= new int[]{0,0,0};
		Textos= new Text[]{Ing1,Ing2,Ing3};
		CrearPedido ();
	}
	
	// Update is called once per frame
	void Update () {
		Borrachera ();
	}

	void CrearPedido(){
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

		foreach(Text t in Textos){
			t.color = Color.black;
		}

		pedido [0] = Ings [u];
		Ing1.text = IngNames [u];

		pedido [1] = Ings [i];
		Ing2.text = IngNames [i];

		pedido [2] = Ings [e];
		Ing3.text = IngNames [e];

		Debug.Log ("Pedido: " + u + " "+IngNames [u]+ " " + i + " "+IngNames [i] + " " +IngNames [e]+ " "+ e);
	}

	public bool Corroborar(int p){
		bool bien = false;
		for (int i = 0; i < pedido.Length; i++) {
			if (p == pedido [i]) {
				Debug.Log ("pos " + i + " Recogido " + p + " Ing " + pedido [i]); 
				Textos[i].color = Color.green;
				bien = true;
			}

			}
		if (!bien) {
			Ingr.parado = true;
			foreach(Text t in Textos){
				t.color = Color.red;
			}
			Debug.Log ("Parado");
		}
		bool nuevopedido = true;
		for (int i = 0; i < Textos.Length; i++) {
			if (Textos [i].color != Color.green) {
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
		CrearPedido ();
		GameObject.Find ("Main Camera").GetComponent<Ingr> ().CrearIngerd ();
		Ingr.parado = false;
		if (borracho < 3) {
			borracho++;
			StartCoroutine ("Metabolizar");
		}
	}

	void Borrachera()
	{
		switch (borracho) {

		case 1:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = true;
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().extraBlur = false;
			break;

		case 2:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().extraBlur = true;
			break;

		default:
			GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = false;
			break;
		}

	}

	IEnumerator Metabolizar(){

		yield return new WaitForSeconds (8);
		borracho--;
	}

}