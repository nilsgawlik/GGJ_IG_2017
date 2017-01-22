﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreTrail : MonoBehaviour {
	LineRenderer lineRend;

	private List<Vector2> pointList = new List<Vector2>();
	private const int maxPoints = 15;
	private waveMotion waveMot;
	private hasPoints points;

	private const int maxScore = 200;
	private Color playerColor;

	// Use this for initialization
	void Start () {
		lineRend = GetComponent<LineRenderer> ();
		lineRend.numPositions = maxPoints;
		lineRend.sortingOrder = -1;

		points = GetComponent<hasPoints> ();

		playerColor = GetComponent<SpriteRenderer> ().color;

		for (int x = 0; x < (maxPoints); x++) {
			pointList.Add(Vector2.zero);
		}

		waveMot = GameObject.FindGameObjectWithTag("Wave").GetComponent<waveMotion> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		pointList.Add (transform.position);

		pointList.RemoveAt (0);

		if (waveMot == null)
			return;

		int pointsToRender = maxPoints; //(int)Mathf.Round(((float)points.points / maxScore) * maxPoints);

		float progress = (float)points.points / maxScore * 2f;

		Color col;

		if (progress < 1) 
			col = Color.Lerp(new Color(playerColor.r, playerColor.g, playerColor.b, 0f),
				playerColor, progress);
		else
			col = Color.Lerp(playerColor,
				Color.white, progress-1f);
			

		lineRend.endColor = lineRend.startColor = col;

		lineRend.numPositions = pointsToRender;

		for (int i = maxPoints - pointsToRender; i < maxPoints; i++) {
			Vector2 vec = pointList.ElementAt (i);
			vec.x = vec.x - waveMot.getWaveSpeed () * (maxPoints-i);

			lineRend.SetPosition(maxPoints-i-1, vec);
		}
	}
}
