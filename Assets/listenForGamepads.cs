﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listenForGamepads : MonoBehaviour {

    int[] playerControllerIds = new int[4];

    public GameObject[] textFields;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 1; i < 12; i++)
        {
            if (Input.GetKeyDown("joystick " + i + " button 0"))
                AddPlayer(i);
        }
	}
    void AddPlayer(int joystickId)
    {
        for(int i = 0; i < 4; i++)
        {
            if (playerControllerIds[i] == joystickId)
                return;
        }

        for(int i = 0; i < 4; i++)
        {
            if (playerControllerIds[i] <= 0)
            {
                playerControllerIds[i] = joystickId;
                Debug.Log("Gamepad connected at id " + joystickId);

                textFields[i].SetActive(false);

                break;
            }
        }
    }

    public int[] GetJoystickIds()
    {
        return playerControllerIds;
    }
}