using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourchScript : MonoBehaviour {

    public GameObject torchParticles;
    public bool isTorchLit = false;

    [Header("Only use is isTorchLit = false")]
    public GameObject player;
    public AudioClip torchTriggerSFX;
    public float torchLifeTimer = 60;
    [Header("False is fireball / True is player")]
    public bool isPlayerTrigger = false;

    private bool isTorchActivated = false;
    private float currentLifeTimer = 0;

	// Use this for initialization
	void Start ()
    {
		if(isTorchLit == true)
        {
            isTorchActivated = true;
            torchParticles.SetActive(true);
        }
        else
        {
            torchParticles.SetActive(false);

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isTorchActivated == true && isTorchLit == false)
        {
            if(currentLifeTimer <= 0)
            {
                isTorchActivated = false;
                torchParticles.SetActive(false);
            }
            else
            {
                isTorchLit = true;
                //currentLifeTimer -= (5 * Time.deltaTime);
                //Debug.Log("Did a count.");
            }
        }
	}

    //Checks if player activates torch
    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player" && isPlayerTrigger == true) || (other.tag == "Fireball" && isPlayerTrigger == false) && isTorchActivated == false)
        {
            isTorchActivated = true;
            torchParticles.SetActive(true);
            currentLifeTimer = torchLifeTimer;
            player.GetComponent<FireScript>().SFXPlayer.PlayOneShot(torchTriggerSFX);
        }
    }
}
