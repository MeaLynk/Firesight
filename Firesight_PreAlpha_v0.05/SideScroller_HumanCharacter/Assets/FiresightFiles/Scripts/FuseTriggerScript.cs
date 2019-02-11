using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseTriggerScript : MonoBehaviour {

    public GameObject mainFuseObject;

    private bool hasBeenHit = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Triggers fuse
    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenHit == false)
        {
            mainFuseObject.GetComponent<FuseScript>().TriggerFuse();
            hasBeenHit = true;
        }
    }
}
