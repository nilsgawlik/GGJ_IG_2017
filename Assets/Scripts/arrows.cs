﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrows : MonoBehaviour {

	private Vector3 screenPos;
	private Camera cam;
	private GameObject dreieck;

	// Use this for initialization
	void Start () {
		dreieck = gameObject.transform.GetChild (2).gameObject;
		dreieck.SetActive (false);
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		screenPos = cam.WorldToViewportPoint(transform.position);
		dreieck.SetActive (screenPos.y > 1 || screenPos.y < 0);
//		if (screenPos.y > 1 || screenPos.y < 0) {
//			dreieck.SetActive (true);
//		} else {
//			dreieck.SetActive (false);
//		}
	}
}