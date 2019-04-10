using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    //-----------------------------------------------------
    // Public Members
    public GameObject cutScene;
    public Image fade;
    public Camera creditsCamera;
    public bool hasFadeIn = true;
    [Range(0.0f, 1.0f)] public float fadeSpeed = 1.0f;

    //-----------------------------------------------------
    // Private Members
    private bool fadeIn = false;
    private bool startFadeWait = false;
    private bool doneFading = false;
    private float fadeWaitTimer = 0.0f;

    // Use this for initialization
    void Awake()
    {
        cutScene.SetActive(false);
        if (hasFadeIn)
            startFadeWait = true;
        else
            fade.color = fade.color + new Color(0.0f, 0.0f, 0.0f, -1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (hasFadeIn)
        {
            if (startFadeWait)
            {
                fadeWaitTimer += Time.deltaTime;
                if (fadeWaitTimer >= 1.5f)
                {
                    startFadeWait = false;
                    fadeIn = true;
                }
            }
            if (fadeIn)
            {
                if (fade.color.a > 0)
                {
                    fade.color = fade.color + (new Color(0.0f, 0.0f, 0.0f, -fadeSpeed) * Time.deltaTime);
                }
                else
                    doneFading = true;
                    
            }
        }
        if ((hasFadeIn && doneFading) || !hasFadeIn)
        {
            creditsCamera.transform.position += (new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime);
        }
    }
}
