using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [Header("LEVEL MUST BE IN BUILD SETTINGS TO WORK.")]
    public string levelName;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(levelName);
        //Cursor.visible = true;
        Debug.Log("Level " + levelName + " loaded.");
    }
}
