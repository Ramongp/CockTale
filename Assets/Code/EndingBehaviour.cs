using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndingBehaviour : MonoBehaviour {

	[SerializeField] List<GameObject> incomes;
	[SerializeField] Text eggName;
	public int score = 27000;
	int cont = 1;
	float timer;
	float timeBetweenScore = 1f;

	void Start () {
		incomes [6].transform.GetChild (0).GetComponent<Text> ().text = score.ToString ();
	}
	
	void Update () {
		timer += Time.deltaTime;

		CheckScore ();
	}

	void CheckScore() {
		if (score >= 100 && cont == 1) {
			incomes [0].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (score >= 700 && cont == 2) {
			incomes [1].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (score >= 1900 && cont == 3) {
			incomes [2].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (score >= 5400 && cont == 4) {
			incomes [3].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (score >= 12400 && cont == 5) {
			incomes [4].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (score >= 26400 && cont == 6) {
			incomes [5].SetActive (true);
			if (timer >= timeBetweenScore) {
				timer = 0f;
				cont++;
			}
		}

		if (timer >= 2f) {
			incomes [6].SetActive (true);
			incomes [7].SetActive (true);
		}
	}
}
