using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EggNameBehaviour : MonoBehaviour {

	Text nameEditor;

	void Start () {
		nameEditor = GetComponent<Text> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) 
		{

			Puntuacion.Name = nameEditor.text;

			Application.LoadLevel ("CocteleraMove");
		}
	}
}
