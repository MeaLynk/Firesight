﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFireScript : MonoBehaviour {

    public GameObject player = null;
    public AudioClip killedFireballSFX;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fireball")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(killedFireballSFX);
            player.GetComponent<FireScript>().QuitFireball();
            Debug.Log("Fireball Ended.");
        }
    }
}
