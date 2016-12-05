using UnityEngine;
using System.Collections;

public class Puntuacion : MonoBehaviour {

	public static string Name;
	public static float Punt;
	public static float Tiempo;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		Tiempo = 200;

	}
	
	// Update is called once per frame
	void Update () {

		Tiempo -= Time.deltaTime;
		if (Tiempo.Equals (0)) 
		{
			Application.LoadLevel ("EndingScene");
		}
	
	}
}
