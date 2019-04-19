using UnityEngine;
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
        cutScene.SetActive(false);
        if (hasFadeIn)
            startFadeInWait = true;
        else
            fade.color = fade.color + new Color(0.0f, 0.0f, 0.0f, -1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (hasFadeIn)
        {
            if (startFadeInWait)
            {
                fadeWaitTimer += Time.deltaTime;
                if (fadeWaitTimer >= 1.5f)
                {
                    startFadeInWait = false;
                    fadeIn = true;
                    fadeWaitTimer = 0.0f;
                }
            }
            if (fadeIn)
            {
                if (fade.color.a > 0)
                {
                    fade.color = fade.color + (new Color(0.0f, 0.0f, 0.0f, -fadeSpeed) * Time.deltaTime);
                }
                else
                    doneFadingIn = true;
                    
            }
        }
        if ((hasFadeIn && doneFadingIn) || !hasFadeIn)
        {
            creditsCamera.transform.position += (new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime);
            deltaY += -1.0f * Time.deltaTime;
        }
<<<<<<< HEAD
        if (deltaY <= -60.0f)
        {
            SceneManager.LoadScene(0);
=======
        if (deltaY * -1 >= 53.4f)
        {
            startFadeOutWait = true;
        }
        if (hasFadeOut)
        {
            if (startFadeOutWait)
            {
                fadeWaitTimer += Time.deltaTime;
                if (fadeWaitTimer >= 1.5f)
                {
                    startFadeOutWait = false;
                    fadeOut = true;
                    fadeWaitTimer = 0.0f;
                    deltaY = 50.0f;
                }
            }
            if (fadeOut)
            {
                if (fade.color.a < 1)
                {
                    fade.color = fade.color + (new Color(0.0f, 0.0f, 0.0f, fadeSpeed) * Time.deltaTime);
                }
                else
                    doneFadingOut = true;
            }
>>>>>>> parent of 25aed7e... updated again
        }
    }
}
