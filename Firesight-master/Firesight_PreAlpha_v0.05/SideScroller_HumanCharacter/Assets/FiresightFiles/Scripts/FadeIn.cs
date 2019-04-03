using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeToBlackImage;
    public float fadeTimer = 1;

    private float currentFadeTimer = 0;

    // Use this for initialization
    void Start ()
    {
        fadeToBlackImage.gameObject.SetActive(true);
        fadeToBlackImage.color = new Color(0, 0, 0, 225);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(currentFadeTimer >= fadeTimer)
        {
            fadeToBlackImage.color = new Color(0, 0, 0, 0);
            gameObject.GetComponent<FadeIn>().enabled = false;
        }
        else
        {
            currentFadeTimer += Time.deltaTime;
            float tempNewTranspo = ((fadeTimer - currentFadeTimer) / fadeTimer);
            fadeToBlackImage.color = new Color(0, 0, 0, tempNewTranspo);
        }
	}
}
