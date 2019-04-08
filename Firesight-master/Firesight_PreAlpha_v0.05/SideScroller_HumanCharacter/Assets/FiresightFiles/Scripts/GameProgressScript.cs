using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(GameObject.Find("SaveObject").GetComponent<SaveScript>().GetLevel1Beat() == false && other.tag == "Player")
        {
            GameObject.Find("SaveObject").GetComponent<SaveScript>().UnlockLevel2();
            Debug.Log("Unlocked Level 2.");
        }
    }
}
