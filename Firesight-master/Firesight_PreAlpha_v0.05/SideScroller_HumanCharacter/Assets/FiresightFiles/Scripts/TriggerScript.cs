using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    public float timeBeforeActivation = 0;

    private bool isActivated = false;
    private bool hasActivated = false;
    private float currentTime = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isActivated == true && hasActivated == false)
        {
            if (currentTime >= timeBeforeActivation)
            {
                objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
                hasActivated = true;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
	}

    //Triggers animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            isActivated = true;
        }
    }

    //maybe add reset here if it is needed
}
