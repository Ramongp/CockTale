﻿using UnityEngine;
using System.Collections;

public class Ingr : MonoBehaviour {

	// Use this for initialization
	public Transform Ing;
	public Transform IngHijo;
	public GameObject Señuelo;
	public int Pos1X, Pos2X, Pos3X, Pos1Z, Pos2Z;
	public int[] PosVect;
	public GameObject[] Ings;
	 public int i,PosIng;
	public static bool parado;
	void Start () {
		Pos1Z = 20;
		Pos2Z = 0;
		Pos1X = -8;
		Pos2X=0;
		Pos3X = 8;
		PosVect = new int[]{Pos1X, Pos2X, Pos3X};
		//IngHijo =Ings[i];
		CrearIngerd ();

	}
	
	// Update is called once per frame
	void Update () {
		if(!parado){
			if (Ing.position.z > Pos2Z) {
				Ing.Translate (new Vector3 (0, 0, -10) * Time.deltaTime);
				Señuelo.transform.position = new Vector3 (Ing.position.x, Señuelo.transform.position.y, Señuelo.transform.position.z);
			} else {
				Debug.Log("Mi posición: "+PosIng+" Posición Coctelera: "+GameObject.Find ("Main Camera").GetComponent<CocteleraMove> ().Pos );
				if (GameObject.Find ("Main Camera").GetComponent<CocteleraMove> ().Pos == PosIng) {
					Debug.Log ("Recogido ingrediente " + i);
					if (GameObject.Find ("Pedido").GetComponent<Pedido> ().Corroborar (i)) {
						CrearIngerd ();

					} else {
						Destroy (Señuelo);
					}
				}else {
					CrearIngerd ();
					}
			}
		}
	}


	public void CrearIngerd()
	{
		Debug.Log ("LLamo a crear Ingr");
		PosIng = Random.Range (0, 2);
	

		Destroy (Señuelo);
		//IngHijo =Ings[i];
		i = Random.Range (0, Ings.Length);
		//Transform prueba = IngHijo;
		//prueba.position = new Vector3 (PosVect [PosIng], prueba.position.y, 0);


		Señuelo = Instantiate (Ings [i], IngHijo)as GameObject;


		Ing.position = new Vector3 (PosVect [PosIng], 0, Pos1Z);
		//Señuelo.transform.position = new Vector3 (0, 0, 0);

	}
}