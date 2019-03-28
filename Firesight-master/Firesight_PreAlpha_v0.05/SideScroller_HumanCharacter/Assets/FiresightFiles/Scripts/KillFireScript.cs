using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFireScript : MonoBehaviour
{

    private GameObject player;
    public AudioClip killedFireballSFX;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            if (gameObject.GetComponent<AudioSource>())
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(killedFireballSFX);
            }

            player.GetComponent<FireScript>().QuitFireball();
            Debug.Log("Fireball Ended.");
        }
    }
}
