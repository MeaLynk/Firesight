using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnRope : MonoBehaviour {

    public GameObject platform;
    public GameObject rope;
    [Header("Put audio source on the platform, NOT THE ROPE")]
    public AudioClip burningSFX;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    // Use this for initialization
    void Start ()
    {
        if(platform == null)
        {
            Debug.LogError("NO PLATFORM IN BurnRope SCRIPT");
        }
        else
        {
            platform.GetComponent<Rigidbody>().useGravity = false;
            platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            defaultPos = platform.GetComponent<Transform>().position;
            defaultRot = platform.GetComponent<Transform>().rotation;
        }

        if(rope == null)
        {
            Debug.LogError("NO ROPE IN BurnRope SCRIPT");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Triggers Object destruction
    public void Trigger()
    {
        rope.GetComponent<BoxCollider>().enabled = false;
        rope.SetActive(false);

        platform.GetComponent<AudioSource>().PlayOneShot(burningSFX);
        platform.GetComponent<Rigidbody>().useGravity = true;
        platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;



    }

    //Gets called to reset ropes
    public void ResetRopes()
    {
        rope.SetActive(true);
        rope.GetComponent<BoxCollider>().enabled = true;
        rope.GetComponent<TriggerRope>().ResetStuff();

        platform.GetComponent<Rigidbody>().useGravity = false;
        platform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        platform.GetComponent<Transform>().position = defaultPos;
        platform.GetComponent<Transform>().rotation = defaultRot;
    }

    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 400, 120, 40), defaultPos.ToString());
    //}
}
