using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasPunt : MonoBehaviour {

	// Use this for initialization
	public Text Tiempo,Punt;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Tiempo.text =  "Quedan " + Puntuacion.Tiempo.ToString("F2") +" s";
		Punt.text = "Has Ganado " + Puntuacion.Punt + " €";
	}
}
