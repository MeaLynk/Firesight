﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    //-----------------------------------------------------
    // Public Members
    public GameObject cutScene;
    public Image fade;
    public Camera creditsCamera;
    public bool hasFadeIn = true;
    [Range(0.0f, 1.0f)] public float fadeSpeed = 1.0f;
    public bool hasFadeOut = true;
    public float deltaY = 0.0f;

    //-----------------------------------------------------
    // Private Members
    private bool fadeIn = false;
    private bool startFadeInWait = false;
    private bool doneFadingIn = false;
    public bool fadeOut = false;
    public bool startFadeOutWait = false;
    public bool doneFadingOut = false;
    public float fadeWaitTimer = 0.0f;

    // Use this for initialization
    void Awake()
    {
        creditsCamera.transform.position = new Vector3(-7.81f, 3.69f, 97.5f);
        cutScene.SetActive(false);
        fade.GetComponent<Animator>().Play("FadeOutAnim");
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsCamera.enabled)
        {
            creditsCamera.transform.position += (new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime);
            deltaY += -1.0f * Time.deltaTime;
        }
        if (deltaY <= -60.0f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
