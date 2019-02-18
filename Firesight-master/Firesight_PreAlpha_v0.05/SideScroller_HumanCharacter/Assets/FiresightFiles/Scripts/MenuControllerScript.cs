using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuControllerScript : MonoBehaviour
{
    #region Private Variables
    private GameObject areYouSurePopUp;
    private bool isPossibleQuit;
    #endregion

    //---------------------------------------------------------//
    // Used to Initialize script
    //---------------------------------------------------------//
    void Start()
    {
        isPossibleQuit = false;
        areYouSurePopUp = GameObject.Find("AreYouSure");
    }

    //---------------------------------------------------------//
    // Update is called once per frame
    //---------------------------------------------------------//
    void Update()
    {
        if (isPossibleQuit)
        {
            // Show the quit confirmation message
            areYouSurePopUp.SetActive(true);
        }
        else if (!isPossibleQuit)
        {
            // Hide the quit confirmation message
            areYouSurePopUp.SetActive(false);
        }
    }

    //---------------------------------------------------------//
    // Used when the user presses the start button
    //---------------------------------------------------------//
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //---------------------------------------------------------//
    // Used when the user presses the quit button
    //---------------------------------------------------------//
    public void QuitPress()
    {
        // If the user has hit the button, trigger this bool
        // to control the confirmation message
        isPossibleQuit = true;
    }

    //---------------------------------------------------------//
    // Used when user presses the quit button but presses the
    // back button on the confirmation
    //---------------------------------------------------------//
    public void CancelQuit()
    {
        // If the user presses the back button on the quit
        // confirmation message, this bool will allow it to 
        // be closed
        isPossibleQuit = false;
    }

    //---------------------------------------------------------//
    // Used when the user presses the quit button is confirms 
    // the quit in the confirmation screen
    //---------------------------------------------------------//
    public void EngageQuit()
    {
        Application.Quit();
    }
    
}