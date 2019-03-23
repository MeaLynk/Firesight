using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuControllerScript : MonoBehaviour
{
    public enum MenuStates { TRANSITION, START, MAINMENU } //Transition may be removed depending if needed

    public GameObject menuFireball = null;
    public GameObject areYouSurePopUp = null; //Prob gonna get rid of this or rework it
    public Camera mainCamera = null;
    public Transform camTar;
    [Header("Must Have 2 values. (For now)")]
    public Vector3[] camTarPos; //0 = start, 1 = mainMenu

    private bool isPossibleQuit;
    private MenuStates currentMenu = MenuStates.START;

    public MenuStates GetCurrentMenu() { return currentMenu; }

    //---------------------------------------------------------//
    // Used to Initialize script
    //---------------------------------------------------------//
    void Start()
    {
        isPossibleQuit = false;
        Cursor.visible = false;

        if (areYouSurePopUp == null)
        {
            areYouSurePopUp = GameObject.Find("AreYouSure"); 
        }

        if(menuFireball == null)
        {
            menuFireball = GameObject.FindGameObjectWithTag("Fireball");
        }

        if (mainCamera == null)
        {
            menuFireball = GameObject.FindGameObjectWithTag("MainCamera");
        }

        camTar.position = camTarPos[0];
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

        //Uses fireball as the cursor for the menu
        menuFireball.GetComponent<Transform>().position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8));

        //Menu States else if
        if(currentMenu == MenuStates.START)
        {
            if(Input.GetMouseButtonDown(0))
            {
                camTar.position = camTarPos[1];
                currentMenu = MenuStates.MAINMENU;
            }
        }
    }

    //---------------------------------------------------------//
    // Used when the user presses the start button
    //---------------------------------------------------------//
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Cursor.visible = true;
        Debug.Log("Level " + levelName + " loaded.");
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

    //Debug stuff
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 80, 30), currentMenu.ToString());
    }
}