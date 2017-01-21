﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSurf : MonoBehaviour {

	public GameObject lineHolder;
	public float gravFactor;
	public float advSpeed;
	public float retrSpeed;
	public float airborneDrag;
	public float jumpFactor;

	private Vector2 velocity;


	private WavePointGenerator wavePointGen;
	private waveMotion waveMot;

	private bool airborne = false;

	// Use this for initialization
	void Start () {
		wavePointGen = lineHolder.GetComponent<WavePointGenerator> ();
		waveMot = lineHolder.GetComponent<waveMotion> ();

		velocity = new Vector2 (advSpeed, 0);
	}

	void Update() {

		// jump
		if (Input.GetKeyDown ("space")) { //TODO change this ;)
			if (!airborne) {
				airborne = true;
				velocity = transform.rotation * (Vector2.right * jumpFactor);
				//velocity.x = retrSpeed;
				velocity.x = 0;
			} else {
				airborne = false; //TODO check for line proximity
				velocity.x -= retrSpeed;
			}
		}

	}

	void FixedUpdate () {
		Vector2 pos = this.transform.position;

		if (isAirborne ()) {
			// drag 
			velocity.x -= airborneDrag;
		} else {
			velocity.x = advSpeed;
		}


		//snap
		if (!isAirborne ()) {
			velocity.y = 0;
			pos.y = waveMot.GetYAt (pos.x);
		}
		else {
			velocity.y += getGravity ();
		}
		pos = pos + velocity;

		this.transform.position = pos;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180f / Mathf.PI * Mathf.Atan(waveMot.GetSlopeAt (pos.x))));
	}

	bool isAirborne() {
		return airborne;
	}

	float getGravity() {
		Vector2 pos = this.transform.position;

		return Mathf.Sign(pos.y) * gravFactor * -1f;
	}
}