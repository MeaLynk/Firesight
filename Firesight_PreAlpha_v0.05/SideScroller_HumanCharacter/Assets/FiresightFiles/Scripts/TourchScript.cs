using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourchScript : MonoBehaviour {

    public GameObject torchParticles;
    public bool isTorchLit = false;
    public float torchLifeTimer = 60;

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
                currentLifeTimer -= (5 * Time.deltaTime);
                //Debug.Log("Did a count.");
            }
        }
	}

    //Checks if player activates torch
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isTorchActivated == false)
        {
            isTorchActivated = true;
            torchParticles.SetActive(true);
            currentLifeTimer = torchLifeTimer;
        }
    }
}
