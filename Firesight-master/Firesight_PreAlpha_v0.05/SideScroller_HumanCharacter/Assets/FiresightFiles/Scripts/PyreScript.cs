using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyreScript : MonoBehaviour {

    public bool isPyreOn = false;
    public GameObject pyreParticles;
    public AudioClip triggerSFX;

    private GameObject gameWorld;

    // Use this for initialization
    void Start ()
    {
        pyreParticles.SetActive(false);

        if (gameWorld == null)
        {
            gameWorld = GameObject.FindGameObjectWithTag("GameWorld");
        }
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
            gameWorld.GetComponent<PauseGame>().player.GetComponent<FireScript>().SFXPlayer.PlayOneShot(triggerSFX);
        }
    }
}
