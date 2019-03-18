using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public GameObject player;
    public GameObject[] checkpointPyres;
    public bool showDebugUI = false;

    private int currentCheckpoint = -5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    //Gets called to activate checkpoint
    public void ActivateCheckpoint(GameObject inNewCheckpoint)
    {
        if (currentCheckpoint >= 0)
        {
            checkpointPyres[currentCheckpoint].GetComponent<PyreScript>().DisableCheckpoint();
        }
        for(int i = 0; i < checkpointPyres.Length; i++)
        {
            if(checkpointPyres[i] == inNewCheckpoint && inNewCheckpoint.tag == "Checkpoint")
            {
                currentCheckpoint = i;
                player.GetComponent<Health>().respawnPos = new Vector3(checkpointPyres[i].GetComponent<Transform>().position.x, checkpointPyres[i].GetComponent<Transform>().position.y, player.GetComponent<Transform>().position.z);
                break;
            }
        }

        Debug.Log("Checkpoint Activated.");
    }

    //Debug Menu
    private void OnGUI()
    {
        if (showDebugUI == true)
        {
            GUI.Box(new Rect(10, 50, 170, 40), "Current Checkpoint in array: \n" + currentCheckpoint);
        }
    }
}
