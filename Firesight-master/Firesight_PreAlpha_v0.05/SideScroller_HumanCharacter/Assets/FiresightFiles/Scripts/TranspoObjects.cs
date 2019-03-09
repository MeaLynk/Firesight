using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspoObjects : MonoBehaviour {

    [Header("MATERIAL MUST BE SET TO TRANSPARENT TO WORK.")]
    public float fadeSpeed = 1.0f;
    public bool affectChildObjects = false;
    [Header("0.0 = Transparent, 1.0 = Solid")]
    public float maxTranspo = 0.0f;

    private Renderer[] childObjects;
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

        if(affectChildObjects == true)
        {
            childObjects = gameObject.GetComponentsInChildren<Renderer>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        isProgressChanged = false;

        transpoTimer();

        if(isProgressChanged == true)
        {
            if(affectChildObjects == true)
            {
                //Affects all child objects and parent with script applied
                for (int i = 0; i < gameObject.transform.childCount + 1; i++)
                {
                    childObjects[i].material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, transpoProgress);
                }
            }
            else
            {
                //Affects only object with script applied
                gameObject.GetComponent<Renderer>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, transpoProgress);
            }
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
        if(isPlayerInRange == true && transpoProgress > maxTranspo)
        {
            currentTranspoTimer -= Time.deltaTime;
            transpoProgress = currentTranspoTimer/fadeSpeed;
            isProgressChanged = true;

            if(transpoProgress <= maxTranspo)
            {
                transpoProgress = maxTranspo;
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
