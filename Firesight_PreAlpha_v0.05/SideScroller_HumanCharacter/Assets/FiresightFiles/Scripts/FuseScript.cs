using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseScript : MonoBehaviour {

    public GameObject fuseTriggerObject;
    public GameObject tntObject;
    public Animator fuseAnimation;

    private bool hasTNTTriggered = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hasTNTTriggered == false)   // 2 if statements to prevent no animator error after triggered
        {                               // note to self: find a better way to fix this later
            if (fuseAnimation.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                Destroy(fuseTriggerObject);
                tntObject.GetComponent<KaboomScript>().TriggerTNT(true);
                hasTNTTriggered = true;
            }
        }
    }

    public void TriggerFuse()
    {
        fuseAnimation.SetTrigger("TriggerFuse");
        fuseTriggerObject.GetComponent<BoxCollider>().enabled = false;
    }
}
