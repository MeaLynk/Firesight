using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    public GameObject panTarget;
    public float timeBeforeActivation = 0;
    public bool isPan;
    public CameraFollow cFollow;

    private bool isActivated = false;
    private bool hasActivated = false;
    private float currentTime = 0;
    private float panTimer;

    // Use this for initialization
    void Start()
    {
        cFollow = Camera.main.GetComponent<CameraFollow>();
       // panTimer 
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated == true && hasActivated == false)
        {
            cFollow.followSpeed = 1.5f;
            cFollow.target = panTarget.transform;
            FireScript fireScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>();
            fireScript.QuitFireball();

            if (currentTime >= timeBeforeActivation)
            {

                objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
                hasActivated = true;

                cFollow.followSpeed = 1.5f;
                cFollow.target = GameObject.FindGameObjectWithTag("Player").transform;
                cFollow.followSpeed = 10;
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
