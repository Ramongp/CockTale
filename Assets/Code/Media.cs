using UnityEngine;
using System.Collections;
using System.Globalization;
using System;

public class Media : MonoBehaviour {

	public Transform cuboRot;
	private int cont = 0;
	private bool Midiendo = false;
	public bool primeraMedicion = true;

	private int MargenAc = 1000;
	private int MargenGc = 100;

	private int Calibrando = 50;
	private int Intervalo = 2;


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

	//Variables
	public int GcX;
	public int GcY;
	public int GcZ;

	public int AcX;
	public int AcY;
	public int AcZ;

	//Rotacion
	private float AngX;
	private float AngY;
	private float AngZ;

	//Posicion
	private float PosX;
	private float PosY;
	private float PosZ;
	// Use this for initialization
	void Start () {
		AngX = cuboRot.rotation.eulerAngles.x;
		AngY = cuboRot.rotation.eulerAngles.y;
		AngZ = cuboRot.rotation.eulerAngles.z;

		PosX = cuboRot.position.x;
		PosY = cuboRot.position.y;
		PosZ = cuboRot.position.z;
	}


	
	// Update is called once per frame
	void Update () {
		
		AcX = Convert.ToInt16(ArduinoInput.Datos [3]);
		AcY = Convert.ToInt16(ArduinoInput.Datos [4]);
		AcZ = Convert.ToInt16(ArduinoInput.Datos [5]);

		GcX = Convert.ToInt16(ArduinoInput.Datos [0]);
		GcY = Convert.ToInt16(ArduinoInput.Datos [1]);
		GcZ = Convert.ToInt16(ArduinoInput.Datos [2]);



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



		if (cont == Calibrando && primeraMedicion) {
				
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


				Debug.Log ("Original: " + origTotalAcX + ", " + origTotalAcY + ", " + origTotalAcZ + ", " + origTotalGcX + ", " + origTotalGcY + ", " + origTotalGcZ);
			}




			if (cont == Intervalo  && !primeraMedicion) {

			mediaTotalAcX = sumTotalAcX / cont;
			mediaTotalAcY = sumTotalAcY / cont;
			mediaTotalAcZ = sumTotalAcZ / cont;

			mediaTotalGcX = sumTotalGcX / cont;
			mediaTotalGcY = sumTotalGcY / cont;
			mediaTotalGcZ = sumTotalGcZ / cont;


			if (Math.Abs(mediaTotalGcX-origTotalGcX) > MargenGc )
				AngX= (AngX + (mediaTotalGcX-origTotalGcX)*Time.deltaTime*Time.deltaTime*Mathf.Rad2Deg)*0.3f;
				
			if(Math.Abs(mediaTotalGcY-origTotalGcY) > MargenGc)
				AngY= (AngY + (mediaTotalGcY-origTotalGcY)*Time.deltaTime*Time.deltaTime*Mathf.Rad2Deg)*0.3f;
				
			if(Math.Abs(mediaTotalGcZ-origTotalGcZ) > MargenGc)
				AngZ=( AngZ + (mediaTotalGcZ-origTotalGcZ)*Time.deltaTime*Time.deltaTime*Mathf.Rad2Deg)*0.3f;
				
				cuboRot.rotation = Quaternion.Euler(AngX, AngY, AngZ);


			/*if (Math.Abs(mediaTotalAcX-origTotalAcX)> MargenAc)
				PosX = PosX + (mediaTotalAcX-origTotalAcX)*Time.deltaTime*Time.deltaTime;

			if(Math.Abs(mediaTotalAcY-origTotalAcY) > MargenAc)
				PosY = PosY + (mediaTotalAcY-origTotalAcY)*Time.deltaTime*Time.deltaTime;

			if( Math.Abs(mediaTotalAcZ-origTotalAcZ) > MargenAc)
				PosZ = PosZ + (mediaTotalAcZ-origTotalAcZ)*Time.deltaTime*Time.deltaTime;
			
					//Debug.Log ("Cambios: "+ cuboX + " " + nuevaX);
				cuboRot.position= new Vector3(PosX,PosY,PosZ);*/

				//Debug.Log ("Cambiando: "+ Time.deltaTime);
					//cuboRot.eulerAngles = new Vector3 (origTotalAcX - mediaTotalGcX, 0, 0);


			Debug.Log ("Variación: " + (mediaTotalAcX-origTotalAcX) + ", " + (mediaTotalAcY-origTotalAcY) + ", " + (mediaTotalAcZ-origTotalAcZ) + ", " + (mediaTotalGcX-origTotalGcX) + ", " + (mediaTotalGcY-origTotalGcY) + ", " + (mediaTotalGcZ-origTotalGcZ));


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
