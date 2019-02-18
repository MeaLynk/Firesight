using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour {

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Triggers animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
        }
    }
}
