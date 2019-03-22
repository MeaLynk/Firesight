using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFireScript : MonoBehaviour {

    public GameObject player;
    public AudioClip killedFireballSFX;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
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
