using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRope : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fireball")
        {
            gameObject.GetComponentInParent<BurnRope>().Trigger();
        }
    }
}
