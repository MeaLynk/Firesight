using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [Header("LEVEL MUST BE IN BUILD SETTINGS TO WORK.")]
    public string levelName;
    public float fadeTimer = 3;
    public Image fadeToBlackImage;

    private float currentFadeTimer = 0;
    private bool triggerActivated = false;

	// Use this for initialization
	void Start ()
    {
        //fadeToBlackImage.color = new Color(0,0,0,0);
        fadeToBlackImage.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(currentFadeTimer <= 0 && triggerActivated == true)
        {
            SceneManager.LoadScene(levelName);
            //Cursor.visible = true;
            Debug.Log("Level " + levelName + " loaded.");
        }
        else if(triggerActivated == true)
        {
            currentFadeTimer -= Time.deltaTime;
            float tempNewTranspo = ((fadeTimer - currentFadeTimer) / fadeTimer);
            fadeToBlackImage.color = new Color(0, 0, 0, tempNewTranspo);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        triggerActivated = true;
        currentFadeTimer = fadeTimer;
    }
}
