using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnRope : MonoBehaviour {

    public GameObject platform;
    [Header("Put audio source on the platform, NOT THE ROPE")]
    public AudioClip burningSFX;

	// Use this for initialization
	void Start ()
    {
        platform.GetComponent<Rigidbody>().useGravity = false;
        platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Triggers Object destruction
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            platform.GetComponent<AudioSource>().PlayOneShot(burningSFX);

            platform.GetComponent<Rigidbody>().useGravity = true;
            platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            Destroy(gameObject);
        }
    }
}
