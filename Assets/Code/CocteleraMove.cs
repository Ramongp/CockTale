using UnityEngine;
using System.Collections;

public class CocteleraMove : MonoBehaviour {


	public int Pos1, Pos2, Pos3, Pos, Vel;
	public int[] PosVect;
	public Transform Coct;
	public bool Moving;
	public float Margen;

	// Use this for initialization
	void Start () {
		Pos1 = -8;
		Pos2 = 0;
		Pos3 = 8;
		PosVect= new int[] {Pos1,Pos2,Pos3};
		Pos = 1;
		Vel = 15;
		Margen = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Moving) {
			if (NewMedia.Izq && Pos > 0) {
				NewMedia.Izq = false;
				NewMedia.Der = false;
				Debug.Log ("Restando");
				Pos--;
				Moving = true;
				//StartCoroutine ("Wait");
			} else {
				if (NewMedia.Der && Pos < PosVect.Length - 1) {
					NewMedia.Der = false;
					NewMedia.Izq = false;
					//Debug.Log ("Sumando");
					Pos++;
					Moving = true;
					//StartCoroutine ("Wait");
				}
			}
		}
		if(Moving){
			if (Coct.position.x >PosVect [Pos]-Margen && Coct.position.x <PosVect [Pos]+Margen) {
				Moving = false;
				NewMedia.Izq = false;
				NewMedia.Der = false;
			} else {
				if (Coct.position.x <= PosVect [Pos]) {
					Coct.Translate (Vector3.right * Time.deltaTime * Vel);
				} else {
					if (Coct.position.x >= PosVect [Pos]) {
						Coct.Translate (Vector3.left * Time.deltaTime * Vel);
					} 
				}
			}
		}
	}



	IEnumerator Wait(){
		Debug.Log ("Waiting");
		yield return new WaitForSeconds (3);
	}
}
