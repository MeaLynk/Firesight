using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspoObjects : MonoBehaviour {

    [Header("MATERIAL MUST BE SET TO TRANSPARENT TO WORK.")]
    public float fadeSpeed = 1.0f;

    private bool isPlayerInRange = false;
    private bool isProgressChanged = false;
    private Color defaultColor;
    private float currentTranspoTimer;
    private float transpoProgress = 1;

	// Use this for initialization
	void Start ()
    {
        defaultColor = gameObject.GetComponent<Renderer>().material.color;
        currentTranspoTimer = fadeSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        isProgressChanged = false;

        transpoTimer();

        if(isProgressChanged == true)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, transpoProgress);
        }
	}

    //Player enters range
    private void OnTriggerEnter(Collider other)
    {
        isPlayerInRange = true;
        Debug.Log("Player in object range");
    }

    //Player exits range
    private void OnTriggerExit(Collider other)
    {
        isPlayerInRange = false;
        //gameObject.GetComponent<Renderer>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1);
        Debug.Log("Player left object range");
    }

    //Timer for transpo
    private void transpoTimer()
    {
        if(isPlayerInRange == true && transpoProgress > 0)
        {
            currentTranspoTimer -= Time.deltaTime;
            transpoProgress = currentTranspoTimer/fadeSpeed;
            isProgressChanged = true;

            if(transpoProgress <= 0)
            {
                transpoProgress = 0;
            }
        }
        else if(isPlayerInRange == false && transpoProgress < 1)
        {
            currentTranspoTimer += Time.deltaTime;
            transpoProgress = currentTranspoTimer / fadeSpeed;
            isProgressChanged = true;

            if (transpoProgress >= 1)
            {
                transpoProgress = 1;
            }
        }
    }

    //Debug UI
    private void OnGUI()
    {
        //GUI.Box(new Rect(10, 250, 120, 40), "Transpo Progress:\n" + transpoProgress);
    }
}
