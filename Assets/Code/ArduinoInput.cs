using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;
using System.Globalization;

public class ArduinoInput : MonoBehaviour{

	SerialPort stream;
	Thread readThread;
	string value;
	bool threadRunning = false;

	/*public static int AcX;
	public static int AcY;
	public static int AcZ;
	public static int GcX;
	public static int GcY;
	public static int GcZ;*/
	public static string[] Datos;

	void Start () {
		string[] ports = SerialPort.GetPortNames ();

		if (ports.Length > 0) {
			stream = new SerialPort (ports [0], 9600);

			stream.Open ();
		}
	}

	void Update() {
		if (stream != null) {
			if (stream.IsOpen && !threadRunning) {
				threadRunning = true;
				readThread = new Thread (new ThreadStart (ReadInput));
				readThread.Start ();
			}
		}
	}

	void OnApplicationQuit() {
		if (stream != null) {
			if (stream.IsOpen) {
				threadRunning = false;
				if (readThread.IsAlive)
					readThread.Abort ();
				stream.Close ();
			}
		}
	}

	private void ReadInput() {
		while (threadRunning) {
			if (stream.IsOpen) {
				string a = stream.ReadLine();
				//string[] b = a.Split (';');
				Datos = a.Split (';');
				//Debug.Log (a);

				/*AcX = Convert.ToInt16 (b [0]);
				AcY = Convert.ToInt16 (b [1]);
				AcZ = Convert.ToInt16 (b [2]);

				GcX = Convert.ToInt16 (b [3]);
				GcY = Convert.ToInt16 (b [4]);
				GcZ = Convert.ToInt16 (b [5]);*/

		//		stream.BaseStream.Flush ();

				//Debug.Log (AcX + ", " + AcY + ", " + AcZ + ", " + GcX + ", " + GcY + ", " + GcZ);

			}
		}
	}
}
