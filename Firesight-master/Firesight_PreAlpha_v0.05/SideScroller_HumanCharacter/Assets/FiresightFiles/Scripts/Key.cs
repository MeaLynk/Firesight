using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isCollected;
	// Use this for initialization
	void Start ()
    {
        isCollected = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (isCollected)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Key for second area collected");
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isCollected = true;
        }
    }

}
