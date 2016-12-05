using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ingr : MonoBehaviour {

	// Use this for initialization
	public Transform Ing;
	public Transform IngHijo;
	public GameObject Señuelo, IngCae, SeñueloCae;
	public int Pos1X, Pos2X, Pos3X, Pos1Z, Pos2Z, contRandom;
	public int[] PosVect;
	public GameObject[] Ings;
	 public int i,PosIng;
	public Animator Esfera;
	public static bool parado,cayendo;
	void Start () {
		contRandom = 0;
		Pos1Z = 20;
		Pos2Z = 0;
		Pos1X = -10;
		Pos2X=0;
		Pos3X = 10;
		PosVect = new int[]{Pos1X, Pos2X, Pos3X};
		//IngHijo =Ings[i];
		CrearIngerd ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!parado) {
			if (Ing.position.z > Pos2Z) {
				Ing.Translate (new Vector3 (0, 0, -10) * Time.deltaTime);
				Señuelo.transform.position = new Vector3 (Ing.position.x, 0, Señuelo.transform.position.z);
			} else {
				Debug.Log("Mi posición: "+PosIng+" Posición Coctelera: "+GameObject.Find ("Main Camera").GetComponent<CocteleraMove> ().Pos );
				if (GameObject.Find ("Main Camera").GetComponent<CocteleraMove> ().Pos == PosIng) {
				//	Debug.Log ("Recogido ingrediente " + i);
					if (GameObject.Find ("Pedido").GetComponent<Pedido> ().Corroborar (i)) {
						CrearIngerd ();

					} else {
						Destroy (Señuelo);
					}
				} else {
					Esfera.SetBool ("Parar", true);
					//Ing.transform.Translate (new Vector3 (0, -20, 0) * Time.deltaTime);
					CrearIngerd ();
					//	StartCoroutine ("Wait");
				}
			}
		}
	}

	public void CrearIngerd()
	{
		//Debug.Log ("LLamo a crear Ingr");
		PosIng = Random.Range (0, 2);

		Destroy (Señuelo);

		//IngHijo =Ings[i];
		i = Random.Range (0, Ings.Length);

		if(contRandom>2){
			while (!GameObject.Find ("Pedido").GetComponent<Pedido> ().CorroborarRandom (i)) {
				i = Random.Range (0, Ings.Length);
			}
			contRandom = 0;
				}
		contRandom++;
		Debug.Log (contRandom);
		//Transform prueba = IngHijo;
		//prueba.position = new Vector3 (PosVect [PosIng], prueba.position.y, 0);


		Señuelo = Instantiate (Ings [i], IngHijo)as GameObject;


		Ing.position = new Vector3 (PosVect [PosIng], 0, Pos1Z);
		//Señuelo.transform.position = new Vector3 (0, 0, 0);

	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (3);
	}
}
