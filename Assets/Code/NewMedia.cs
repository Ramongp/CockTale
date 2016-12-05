using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using UnityEngine.UI;

public class NewMedia : MonoBehaviour {
	//Booleano entre fases
	public int Fase;


	//Fase1 variables
	public static bool Izq=false,Der=false, MidiendoMove =true;

	//Fase3 variables
	public Transform Cubo; 
	public Slider Cantidad;
	public Image Fondo;


	//Fase1 variables
	public static bool Izq=false,Der=false, MidiendoMove =true;

	//Fase3 variables
	public Transform Cubo; 
	public Slider Cantidad;
	public Image Fondo;
	public GameObject Liquido;
	//Objetos y variables

	private bool agitando = false;
	private bool tomando = false;
	private bool moviendoYneg = false;
	private bool moviendoYpos = false;
	private bool moviendoZpos = false;
	private bool moviendoZneg = false;

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


	public AudioClip audio1;
	public AudioClip audio2;


	void Start()
	{
		MidiendoMove = true;

		if (Fase.Equals (3)) {
			Cubo = GameObject.Find("cocteleraPrefab").transform; 
			Cantidad = GameObject.Find ("Cantidad").GetComponent<Slider>();
			Fondo = GameObject.Find ("FondoSlider").GetComponent<Image> ();

			Liquido = GameObject.Find ("Liquido");
			Liquido.transform.localScale = new Vector3 (0, 0, 0);

		}
	}

	void Update () {
		//Medida de valores
		//Debug.Log("MidiendoMove: " + MidiendoMove + ". Fase: " + Fase);
		GcX = Convert.ToInt16 (ArduinoInput.Datos [3]);
		GcY = Convert.ToInt16 (ArduinoInput.Datos [4]);
		GcZ = Convert.ToInt16 (ArduinoInput.Datos [5]);

		AcX = Convert.ToInt16 (ArduinoInput.Datos [0]);
		AcY = Convert.ToInt16 (ArduinoInput.Datos [1]);
		AcZ = Convert.ToInt16 (ArduinoInput.Datos [2]);

		//Fase3
		if (Fase.Equals (3)) {
			var angulos = Cubo.eulerAngles;
			if (AcZ < -8500) {
				angulos.z = 0;
				Cubo.rotation = Quaternion.Euler (angulos);
			}
			else {
				angulos.z = AcZ / 87 + 90;
				Cubo.rotation = Quaternion.Euler (angulos);

				if (Cantidad.value > 0.5) {
					
					if (Cubo.localEulerAngles.z > 50 && Cubo.localEulerAngles.z < 80) {
						Cantidad.value -= Time.deltaTime * 0.2f;
						Cubo.Translate (Vector3.right * Time.deltaTime * 0.5f);

						//Cubo.GetComponent<AudioSource> ().Stop ();
						//Cubo.GetComponent<AudioSource> ().clip = null;
						if (!Cubo.GetComponent<AudioSource> ().clip == audio1) {
							Cubo.GetComponent<AudioSource>().clip = audio1;
							Cubo.GetComponent<AudioSource> ().Play ();
						}
						Fondo.color = Color.blue;
						Liquido.transform.localScale = new Vector3 (Liquido.transform.localScale.x + Time.deltaTime * 0.15f, Liquido.transform.localScale.y + Time.deltaTime * 0.15f, Liquido.transform.localScale.z + Time.deltaTime * 0.15f);
						//Ganas Dinero
						Puntuacion.Punt+=Time.deltaTime * 0.2f;
						//Debug.Log ("Entra");
					} else {

						
					
					
						if (Cubo.localEulerAngles.z > 80 && Cubo.localEulerAngles.z < 100) {

							//Debug.Log (Cubo.GetComponent<AudioSource> ().clip.name);
							//Cubo.GetComponent<AudioSource> ().Stop ();
							//Cubo.GetComponent<AudioSource> ().clip = null;
							if (!Cubo.GetComponent<AudioSource> ().clip == audio2) {
								Cubo.GetComponent<AudioSource>().clip = audio2;
								Cubo.GetComponent<AudioSource> ().Play ();
							}

							Fondo.color = Color.red;
							Cantidad.value -= Time.deltaTime * 0.25f;
						}
						else {

							Cubo.GetComponent<AudioSource> ().Stop ();
							Cubo.GetComponent<AudioSource> ().clip = null;

							Fondo.color = Color.green;
						}
					}
				}


				else {
					if (Cubo.localEulerAngles.z > 80 && Cubo.localEulerAngles.z < 100) {
						Cantidad.value -= Time.deltaTime * 0.2f;

						//Cubo.GetComponent<AudioSource> ().Stop ();
						//Cubo.GetComponent<AudioSource> ().clip = null;
						if (!Cubo.GetComponent<AudioSource> ().clip == audio1) {
							Cubo.GetComponent<AudioSource>().clip = audio1;
							Cubo.GetComponent<AudioSource> ().Play ();
						}
						Fondo.color = Color.blue;
						Cubo.Translate (Vector3.right * Time.deltaTime * 0.5f);
						Liquido.transform.localScale = new Vector3 (Liquido.transform.localScale.x + Time.deltaTime * 0.15f, Liquido.transform.localScale.y + Time.deltaTime * 0.15f, Liquido.transform.localScale.z + Time.deltaTime * 0.15f);

						//Debug.Log ("Entra");
					} else {
						
						if (Cubo.localEulerAngles.z > 100 && Cubo.localEulerAngles.z < 300) {
							//Debug.Log ("Losing Money");

							//Cubo.GetComponent<AudioSource> ().Stop ();
							//Cubo.GetComponent<AudioSource> ().clip = null;
							if (!Cubo.GetComponent<AudioSource> ().clip == audio2) {
								Cubo.GetComponent<AudioSource>().clip = audio2;
								Cubo.GetComponent<AudioSource> ().Play ();
							}

							Fondo.color = Color.red;
							Cantidad.value -= Time.deltaTime * 0.25f;
						}

					else {

							Cubo.GetComponent<AudioSource> ().Stop ();
							Cubo.GetComponent<AudioSource> ().clip = null;

							Fondo.color = Color.green;
						}

					}

				}
				if (Cantidad.value.Equals (0)) {
					Application.LoadLevel ("CocteleraMove");
				}

			}
				
		}

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
		
		//Cambios al mover 
		if (Fase.Equals (1) || MidiendoMove) {
			if (AcY > 8000 && !moviendoYpos) { //IZQUIERDA
				MidiendoMove = false;
				Debug.Log ("Moviendo Y Pos");
				StartCoroutine ("MoviendoYpos");
			}
		
			if (AcY < -8000 && !moviendoYneg) { //DERECHA
				MidiendoMove = false;
				Debug.Log ("Moviendo Y Neg");
				StartCoroutine ("MoviendoYneg");
			}
		
			if (AcZ > 8000 && !moviendoZpos) { //ARRIBA
				MidiendoMove = false;
				Debug.Log ("Moviendo Z Pos");
				StartCoroutine ("MoviendoZpos");
			}
		
			if (AcZ < -20000 && !moviendoZneg) { // ABAJO
				MidiendoMove = false;
				Debug.Log ("Moviendo Z Pos");
				StartCoroutine ("MoviendoZneg");
			}
			//Cambios al girar
			if (GcX < -32700 && !agitando && !tomando) { //IZQUIERDA
				MidiendoMove = false;
				Debug.Log ("VERTIENDO IZQ");
				StartCoroutine ("VertiendoIzq");
			}

			if (GcX > 32700 && !agitando && !tomando) { //DERECHA
				MidiendoMove = false;
				Debug.Log ("VERTIENDO DER");
				StartCoroutine ("VertiendoDer");
			}

			if (GcY < -32700 && !tomando && !agitando) { //ADELANTE
				MidiendoMove = false;
				Debug.Log ("TIRANDO");
				StartCoroutine ("Tirando");
			}
		
			if (GcY > 32700 && !tomando && !agitando) { //ATRÁS
				MidiendoMove = false;
				Debug.Log ("TOMANDO");
				if (Fase.Equals (1)) {
					if (Ingr.parado) {
						GameObject.Find ("Pedido").GetComponent<Pedido> ().Beber ();
					}
				}
				StartCoroutine ("Tomando");
			}
		}
		
	}

	//Movimientos

	IEnumerator MoviendoYpos(){ //MOVER IZQUIERDA
		moviendoYpos = true;
		if(Fase.Equals(1))
			Der=true;
		yield return new WaitForSeconds (1);
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (1);
		moviendoYpos = false;
	}
	
	IEnumerator MoviendoYneg(){ //MOVER DERECHA
		moviendoYneg = true;
		if (Fase.Equals (1))
			Izq = true;
		yield return new WaitForSeconds (1);
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (0);
		moviendoYneg = false;
	}
	
	IEnumerator MoviendoZpos(){ //MOVER ARRIBA
		moviendoZpos = true;
		yield return new WaitForSeconds (1);
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (2);
		moviendoZpos = false;
	}
	
	IEnumerator MoviendoZneg(){ //MOVER ABAJO
		moviendoZneg = true;
		yield return new WaitForSeconds (1);
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (3);
		moviendoZneg = false;
	}
	
	//Giros
	IEnumerator VertiendoIzq(){ //GIRO IZQUIERDA
		agitando = true;
		yield return new WaitForSeconds (2);
		Debug.Log("Vertido Izq");
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (4);
		agitando = false;
	}

	IEnumerator VertiendoDer(){ //GIRO DERECHA
		agitando = true;
		yield return new WaitForSeconds (2);
		Debug.Log("Vertido Der");
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (5);
		agitando = false;
	}

	IEnumerator Tirando(){ //GIRO ADELANTE
		tomando = true;
		yield return new WaitForSeconds (2);
		Debug.Log("Tirando");
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (6);
		tomando = false;
	}
	
	IEnumerator Tomando(){ //GIRO ATRÁS
		tomando = true;
		yield return new WaitForSeconds (2);
		Debug.Log("Tomado");
		if (Fase.Equals (2))
			GameObject.Find ("Main Camera").GetComponent<Mixing> ().Comprobar (7);
		tomando = false;
	}
}