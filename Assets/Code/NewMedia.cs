using UnityEngine;
using System.Collections;
using System.Globalization;
using System;

public class NewMedia : MonoBehaviour {
	//Booleano entre fases
	public int Fase;

	//Fase1 variables
	public static bool Izq=false,Der=false;

	//Objetos y variables

	private bool agitando = false;
	private bool tomando = false;
	private bool moviendoYneg = false;
	private bool moviendoYpos = false;

	//Variables Arduino
	public float AcX;
	public float AcY;
	public float AcZ;

	public float GcX;
	public float GcY;
	public float GcZ;

	public float maxAcX = 0f;
	public float maxAcY = 0f;
	public float maxAcZ = 0f;

	public float maxGcX = 0f;
	public float maxGcY = 0f;
	public float maxGcZ = 0f;

	public float minAcX = 0f;
	public float minAcY = 0f;
	public float minAcZ = 0f;

	public float minGcX = 0f;
	public float minGcY = 0f;
	public float minGcZ = 0f;

	void Update () {
		//Medida de valores
		GcX = Convert.ToInt16 (ArduinoInput.Datos [3]);
		GcY = Convert.ToInt16 (ArduinoInput.Datos [4]);
		GcZ = Convert.ToInt16 (ArduinoInput.Datos [5]);

		AcX = Convert.ToInt16 (ArduinoInput.Datos [0]);
		AcY = Convert.ToInt16 (ArduinoInput.Datos [1]);
		AcZ = Convert.ToInt16 (ArduinoInput.Datos [2]);

		//Máximos
		if (AcX > maxAcX)
			maxAcX = AcX;
		if (AcY > maxAcY)
			maxAcY = AcY;
		if (AcZ > maxAcZ)
			maxAcZ = AcZ;
		if (GcX > maxGcX)
			maxGcX = GcX;
		if (GcY > maxGcY)
			maxGcY = GcY;
		if (GcZ > maxGcZ)
			maxGcZ = GcZ;

		//Mínimos
		if (AcX < minAcX)
			minAcX = AcX;
		if (AcY < minAcY)
			minAcY = AcY;
		if (AcZ < minAcZ)
			minAcZ = AcZ;
		if (GcX < minGcX)
			minGcX = GcX;
		if (GcY < minGcY)
			minGcY = GcY;
		if (GcZ < minGcZ)
			minGcZ = GcZ;
		
		//Cambios al agitar
		if (GcX < -32700 && !agitando && !tomando) {
			Debug.Log ("VERTIENDO IZQ");
			StartCoroutine ("VertiendoIzq");
		}

		if (GcX > 32700 && !agitando && !tomando) {
			Debug.Log ("VERTIENDO DER");
			StartCoroutine ("VertiendoDer");
		}

		if (GcY > 32700 && !tomando && !agitando) {
			Debug.Log ("TOMANDO");
			if (Fase.Equals (1)) {
				GameObject.Find ("Pedido").GetComponent<Pedido> ().Beber ();
			}
			StartCoroutine ("Tomando");
		}
			
		//Cambios al mover 20000 
		if (AcY < -8000 && !moviendoYneg){
			Debug.Log ("Moviendo Y Neg");
			StartCoroutine ("MoviendoYneg");
		}

		if (AcY > 8000 && !moviendoYpos){
			Debug.Log ("Moviendo Y Pos");
			StartCoroutine ("MoviendoYpos");
		}

	}

	IEnumerator VertiendoIzq(){
		agitando = true;
		yield return new WaitForSeconds (1);
		yield return new WaitForSeconds (1);
		yield return new WaitForSeconds (1);
		Debug.Log("Vertido Izq");
		if (Fase.Equals (2)) {
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (1);
		}agitando = false;
	}

	IEnumerator VertiendoDer(){
		agitando = true;
		yield return new WaitForSeconds (1);
		yield return new WaitForSeconds (1);
		yield return new WaitForSeconds (1);
		Debug.Log("Vertido Der");
		if (Fase.Equals (2)) {
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (0);
		}agitando = false;
	}

	IEnumerator Tomando(){
		tomando = true;
		yield return new WaitForSeconds (1);
		yield return new WaitForSeconds (1);
		Debug.Log("Tomado");
		if (Fase.Equals (2)) {
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (2);
		}
			tomando = false;
	}

	IEnumerator MoviendoYneg(){
		moviendoYneg = true;
		if (Fase.Equals (1)) {
			Izq = true;
		}
		yield return new WaitForSeconds (1);
		moviendoYneg = false;
	}

	IEnumerator MoviendoYpos(){
		moviendoYpos = true;
		if(Fase.Equals(1))
		{	
			Der=true;}
		yield return new WaitForSeconds (1);
		moviendoYpos = false;
	}
}