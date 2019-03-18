using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRope : MonoBehaviour {

    public float timer = 0;

    private float currentTimer = 0;
    private bool isRopeHit = false;

    private void Update()
    {
        if (isRopeHit == true)
        {
            if (currentTimer <= 0)
            {
                gameObject.GetComponentInParent<BurnRope>().Trigger();
                Debug.Log("Call this");
            }
            else
            {
                currentTimer -= timer * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fireball")
        {
            currentTimer = timer;
            isRopeHit = true;
        }
    }

    public void ResetStuff()
    {
        currentTimer = 0;
        isRopeHit = false;
    }
}
