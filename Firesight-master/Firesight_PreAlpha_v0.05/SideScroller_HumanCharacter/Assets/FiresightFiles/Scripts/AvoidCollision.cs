using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{
    private float timer = 0.1f;
    private bool hasFireballPassedThrough = false;

    private void Update()
    {
        if(timer > 0 && hasFireballPassedThrough == true)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0 && hasFireballPassedThrough == true)
        {
            GetComponent<BoxCollider>().enabled = true;
            enabled = false; //stop script to save performance
        }
    }

    //Avoids collision for some objects
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fireball" && hasFireballPassedThrough == false)
        {
            GetComponent<BoxCollider>().enabled = false;
            hasFireballPassedThrough = true;
        }
    }
}
