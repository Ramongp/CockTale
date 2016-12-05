using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mixing : MonoBehaviour {

	public Sprite rightMove;
	public Sprite leftMove;
	public Sprite downMove;
	public Sprite upMove;
	
	public Sprite rightTurn;
	public Sprite leftTurn;
	public Sprite downTurn;
	public Sprite upTurn;

	public Image move0;
	public Image move1;
	public Image move2;

	public List<Image> moves = new List<Image>();
	private List<Sprite> allMoves = new List<Sprite>();
	private List<Sprite> actualMoves = new List<Sprite>();
	private List<int> actualPos = new List<int> ();

	private int pos;

	// Use this for initialization
	void Start () {
		allMoves.AddRange(new Sprite[]{leftMove, rightMove, upMove, downMove, leftTurn, rightTurn, upTurn, downTurn});
		moves.AddRange (new Image[]{move0, move1,  move2});
		GenerateMoves ();

		moves [0].sprite = actualMoves [0];
		moves [1].sprite = actualMoves [1];
		moves [2].sprite = actualMoves [2];
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void GenerateMoves()
	{
		for (int x = 0; x < 3; x++)
		{
			int i = UnityEngine.Random.Range (0, 7);
			actualMoves.Add (allMoves [i]);
			actualPos.Add (i);
		}
	}

	public void Comprobar(int checking){
	/*
	0 = MOVER IZQUIERDA
	1 = MOVER DERECHA
	2 = MOVER ARRIBA
	3 = MOVER ABAJO
	4 = GIRAR IZQUIERDA
	5 = GIRAR DERECHA
	6 = GIRAR ADELANTE
	7 = GIRAR ATRÁS
	*/
		if (actualPos [pos] == checking)
			moves [pos].color = Color.green;
		else
			moves [pos].color = Color.red;
		pos++;
		NewMedia.MidiendoMove = true;

		if(pos == 3)
			Application.LoadLevel ("Fase3");
	}
}