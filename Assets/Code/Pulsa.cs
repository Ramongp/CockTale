using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pulsa : MonoBehaviour {

	private RectTransform rT;
	private Vector2 originalPosition;
	private Text text;
	public Image fade;
	private bool pressedPlay = false;

	// Use this for initialization
	void Start () {
	
		rT = this.GetComponent<RectTransform> ();
		originalPosition = rT.anchoredPosition;
		text = this.GetComponent<Text> ();
		fade.color = new Color (0f, 0f, 0f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
	
		rT.anchoredPosition = originalPosition + new Vector2 (0f, Mathf.Sin (Time.realtimeSinceStartup*2.4f)*5f);
		text.color = Color.Lerp (text.color, new Color (1f, 1f, 1f, Mathf.Abs(Mathf.Sin (Time.realtimeSinceStartup))), Time.deltaTime*5f);

		if (!pressedPlay) {

			fade.color = Color.Lerp(fade.color, new Color(0f, 0f, 0f, -0.1f), Time.deltaTime*5f);

		} else {

			fade.color = Color.Lerp(fade.color, new Color(0f, 0f, 0f, 1.1f), Time.deltaTime*5f);
			if (fade.color.a >= 1f) {
				Application.LoadLevel ("NOMBRE ESCENA JUGAR");
			}

		}

	}

	public void Play() {

		pressedPlay = true;

	}

}
