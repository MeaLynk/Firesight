﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

    [Header("Objects that will reset after death")]
    public GameObject[] presurePlates;
    public GameObject[] doors;
    public GameObject[] keys;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Resets the all gameobjects to their default states
    public void ResetObjects()
    {
        for (int i = 0; i < presurePlates.Length; i++)
        {
            if (presurePlates[i] != null)
            {
                presurePlates[i].GetComponent<PressurePlate>().ResetPresurePlate();
            }
        }
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i] != null)
            {
                doors[i].GetComponent<Door>().ResetDoor();
            }
        }
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] != null)
            {
                keys[i].GetComponent<Key>().ResetKey();
            }
        }
    }
}
