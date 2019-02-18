using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainScript : MonoBehaviour {

    public int damage = 20;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().currentHealth -= damage;
        }
    }
}
