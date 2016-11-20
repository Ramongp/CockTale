using UnityEngine;
using System.Collections;
using System.Globalization;
using System;

public class Media : MonoBehaviour {

	public Transform cuboRot;
	private int cont = 0;
	private bool Midiendo = false;
	public bool primeraMedicion = true;

	private int Margen = 50;
	private int Intervalo = 10;


	private int origTotalAcX = 0;
	private int origTotalAcY = 0;
	private int origTotalAcZ = 0;

	private int origTotalGcX = 0;
	private int origTotalGcY = 0;
	private int origTotalGcZ = 0;

	private int sumTotalAcX = 0;
	private int sumTotalAcY = 0;
	private int sumTotalAcZ = 0;

	private int sumTotalGcX = 0;
	private int sumTotalGcY = 0;
	private int sumTotalGcZ = 0;

	private int mediaTotalAcX = 0;
	private int mediaTotalAcY = 0;
	private int mediaTotalAcZ = 0;

	private int mediaTotalGcX = 0;
	private int mediaTotalGcY = 0;
	private int mediaTotalGcZ = 0;

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		if (!ArduinoInput.Datos [0].Equals (0) && !Midiendo) {
			Midiendo = true;
		}
		if(Midiendo){
		sumTotalAcX += Convert.ToInt16(ArduinoInput.Datos [3]);
		sumTotalAcY += Convert.ToInt16(ArduinoInput.Datos [4]);
		sumTotalAcZ += Convert.ToInt16(ArduinoInput.Datos [5]);

		sumTotalGcX += Convert.ToInt16(ArduinoInput.Datos [0]);
		sumTotalGcY += Convert.ToInt16(ArduinoInput.Datos [1]);
		sumTotalGcZ += Convert.ToInt16(ArduinoInput.Datos [2]);

		cont++;

		//Debug.Log ("Contador: " + cont);
		//Debug.Log ("X: "+ sumTotalAcX);
		//Debug.Log ("Resultado: " + ArduinoInput.AcX + ", " + ArduinoInput.AcY + ", " + ArduinoInput.AcZ + ", " + ArduinoInput.GcX + ", " + ArduinoInput.GcY + ", " + ArduinoInput.GcZ);



			if (cont == Intervalo && primeraMedicion) {
				
				primeraMedicion = false;
				origTotalAcX = sumTotalAcX / cont;
				origTotalAcY = sumTotalAcY / cont;
				origTotalAcZ = sumTotalAcZ / cont;

				origTotalGcX = sumTotalGcX / cont;
				origTotalGcY = sumTotalGcY / cont;
				origTotalGcZ = sumTotalGcZ / cont;

				//cuboRot.eulerAngles = new Vector3 (mediaTotalGcX, mediaTotalGcY, mediaTotalGcZ);

			//	Debug.Log ("Primera medición realizada");
				cont = 0;

				sumTotalAcX = 0;
				sumTotalAcY = 0;
				sumTotalAcZ = 0;

				sumTotalGcX = 0;
				sumTotalGcY = 0;
				sumTotalGcZ = 0;


			//	Debug.Log ("Original: " + origTotalAcX + ", " + origTotalAcY + ", " + origTotalAcZ + ", " + origTotalGcX + ", " + origTotalGcY + ", " + origTotalGcZ);
			}




			if (cont == Intervalo  && !primeraMedicion) {
				primeraMedicion = true;
			mediaTotalAcX = sumTotalAcX / cont;
			mediaTotalAcY = sumTotalAcY / cont;
			mediaTotalAcZ = sumTotalAcZ / cont;

			mediaTotalGcX = sumTotalGcX / cont;
			mediaTotalGcY = sumTotalGcY / cont;
			mediaTotalGcZ = sumTotalGcZ / cont;

				if ((origTotalAcX - mediaTotalAcX) > Margen || (origTotalAcY - mediaTotalAcY) > Margen ||  (origTotalAcZ - mediaTotalAcZ) > Margen
					||  (origTotalGcX - mediaTotalGcX) > Margen ||  (origTotalGcY - mediaTotalGcY) > Margen ||  (origTotalGcZ - mediaTotalGcZ) > Margen) {
					float cuboX = cuboRot.position.x;
					float nuevaX = cuboX + (origTotalGcX-mediaTotalGcX)/ 200;
					//Debug.Log ("Cambios: "+ cuboX + " " + nuevaX);
					cuboRot.position= new Vector3(nuevaX,0,0);
					//cuboRot.eulerAngles = new Vector3 (origTotalAcX - mediaTotalGcX, 0, 0);
				}
				Debug.Log ("MEDIA: " + (origTotalAcX - mediaTotalAcX) + ", " + (origTotalAcY - mediaTotalAcY) + ", " + (origTotalAcZ - mediaTotalAcZ) + ", " + (origTotalGcX-mediaTotalGcX) + ", " + (origTotalGcY-mediaTotalGcY) + ", " + (origTotalGcZ-mediaTotalGcZ));


		//	Debug.Log ("Contador: " + cont);
			cont = 0;

				sumTotalAcX = 0;
				sumTotalAcY = 0;
				sumTotalAcZ = 0;

				sumTotalGcX = 0;
				sumTotalGcY = 0;
				sumTotalGcZ = 0;


				//Debug.Log ("MEDIA: " + (origTotalAcX-mediaTotalAcX) + ", " + (origTotalAcY-mediaTotalAcY) + ", " + (origTotalAcZ-mediaTotalAcZ) + ", " + mediaTotalGcX + ", " + mediaTotalGcY + ", " + mediaTotalGcZ);
			}
		}
	}
}
