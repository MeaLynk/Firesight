using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    public GameObject player;
    public AudioClip pickupSFX;
    public float increasedFireTime = 2.0f;

	// Use this for initialization
	void Start ()
    {
		if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Triggers powerup
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            player.GetComponent<FireScript>().fireBurnOutTimerLength += increasedFireTime;
            player.GetComponent<FireScript>().SFXPlayer.PlayOneShot(pickupSFX);
            Destroy(gameObject);
        }
    }
}
