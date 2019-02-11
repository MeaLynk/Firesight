using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyreScript : MonoBehaviour {

    public GameObject gameWorld;
    public GameObject pyreParticles;

    private bool isPyreOn = false;

	// Use this for initialization
	void Start ()
    {
        pyreParticles.SetActive(false);
	}

    //Gets called to disable the checkpoint
    public void DisableCheckpoint()
    {
        isPyreOn = false;
        pyreParticles.SetActive(false);
    }

    //Player triggers checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isPyreOn == false)
        {
            isPyreOn = true;
            pyreParticles.SetActive(true);
            gameWorld.GetComponent<CheckpointManager>().ActivateCheckpoint(gameObject);
        }
    }
}
