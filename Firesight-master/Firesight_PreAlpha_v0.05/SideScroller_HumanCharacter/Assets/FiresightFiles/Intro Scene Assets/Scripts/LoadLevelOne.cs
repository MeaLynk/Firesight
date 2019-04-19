using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOne : MonoBehaviour
{
    [Header("YOU DON'T HAVE TO WORRY ABOUT SCENE NAMES GUYS, DON'T WORRY!!!")]
    private int currentSceneIndex;

    // Use this for initialization
    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
