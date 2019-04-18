using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuControllerScript : MonoBehaviour
{
    public enum MenuStates { START, MAINMENU, OPTIONS, LEVELSELECT, SHOWUI, EXITING } //0 start, 1 mainmenu, 2 options, 3 level select

    public GameObject menuFireball = null;
    //public GameObject areYouSurePopUp = null; //Prob gonna get rid of this or rework it
    public Camera mainCamera = null;
    public Transform camTar;
    public AudioClip buttonClickSFX;
    public AudioClip errorSFX;
    [Header("Must Have 4 values. (For now)")]
    public Vector3[] camTarPos; //0 start, 1 mainmenu, 2 options, 3 level select, 4 load level anim
    [Header("MUST HAVE 2 IMAGES")] // for both levels
    public Sprite[] levelImages;
    public GameObject levelImageObject;
    public TextMeshPro levelText;
    public Image fadeToBlackImage;
    public float fadeTimer = 1;
    public GameObject saveGame;
    public GameObject numOfDeathsTxt;

    //[HideInInspector]
    //public bool hasSaveLoaded = false;

    private bool isPossibleQuit;
    private AudioSource sfxPlayer;
    private MenuStates currentMenu = MenuStates.START;
    private float currentFadeTimer;

    //Show UI stuff
    private MenuStates storedState;
    private GameObject currentImage;
    private string currentLevel = "IntroCutScene";

    public MenuStates GetCurrentMenu() { return currentMenu; }

    private void Awake()
    {
        //if (hasSaveLoaded == false && GameObject.Find("SaveObject") == null)
        //{
        //    DontDestroyOnLoad(Instantiate(saveGamePrefab, transform));
        //    hasSaveLoaded = true;
        //}

        //if (hasSaveLoaded == true)
        //{
        //    Destroy(saveGame);
        //}
    }

    //---------------------------------------------------------//
    // Used to Initialize script
    //---------------------------------------------------------//
    void Start()
    {

        if(menuFireball == null)
        {
            menuFireball = GameObject.FindGameObjectWithTag("Fireball");
        }

        if (mainCamera == null)
        {
            menuFireball = GameObject.FindGameObjectWithTag("MainCamera");
        }

        isPossibleQuit = false;
        Cursor.visible = false;
        sfxPlayer = mainCamera.GetComponent<AudioSource>();
        camTar.position = camTarPos[0];
    }

    //---------------------------------------------------------//
    // Update is called once per frame
    //---------------------------------------------------------//
    void Update()
    {
        //if (isPossibleQuit)
        //{
        //    // Show the quit confirmation message
        //    areYouSurePopUp.SetActive(true);
        //}
        //else if (!isPossibleQuit)
        //{
        //    // Hide the quit confirmation message
        //    areYouSurePopUp.SetActive(false);
        //}

        //Uses fireball as the cursor for the menu
        menuFireball.GetComponent<Transform>().position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8));

        //Menu States else if
        if (currentMenu == MenuStates.START)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sfxPlayer.PlayOneShot(buttonClickSFX);
                camTar.position = camTarPos[1];
                currentMenu = MenuStates.MAINMENU;
            }
        }
        else if(currentMenu == MenuStates.SHOWUI)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                currentMenu = storedState;
                currentImage.SetActive(false);
            }
        }
        else if(currentMenu == MenuStates.OPTIONS)
        {
            numOfDeathsTxt.GetComponent<TextMeshPro>().text = "Number Of Deaths: " + GameObject.Find("SaveObject").GetComponent<SaveScript>().GetNumOfDeaths();
        }
        else if(currentMenu == MenuStates.EXITING)
        {
            if (currentFadeTimer <= 0)
            {
                //GameObject.FindGameObjectWithTag("Save").GetComponent<SaveScript>().SaveGame();
                SceneManager.LoadScene(currentLevel);
                //Cursor.visible = true;
                Debug.Log("Level " + currentLevel + " loaded.");
            }
            else
            {
                currentFadeTimer -= Time.deltaTime;
                float tempNewTranspo = ((fadeTimer - currentFadeTimer) / fadeTimer);
                fadeToBlackImage.color = new Color(0, 0, 0, tempNewTranspo);
            }
        }
    }

    //---------------------------------------------------------//
    // Changes selected Level
    //---------------------------------------------------------//
    public void ChangeLevel() //rework if changed to 3 levels
    {
        if (GameObject.Find("SaveObject").GetComponent<SaveScript>().GetLevel1Beat() == true)
        {
            if (currentLevel == "IntroCutScene")
            {
                currentLevel = "FireSightLevel2";
                levelImageObject.GetComponent<SpriteRenderer>().sprite = levelImages[1];
                levelText.text = "Level 2";
            }
            else if (currentLevel == "FireSightLevel2")
            {
                currentLevel = "IntroCutScene";
                levelImageObject.GetComponent<SpriteRenderer>().sprite = levelImages[0];
                levelText.text = "Level 1";
            }
        }
        else
        {
            //Error sfx for feedback that they can't change level.
            sfxPlayer.PlayOneShot(errorSFX);
        }
    }


    //---------------------------------------------------------//
    // Used when the user presses the start button
    //---------------------------------------------------------//
    public void LoadLevel()
    {
        sfxPlayer.PlayOneShot(buttonClickSFX);
        currentMenu = MenuStates.EXITING;
        camTar.position = camTarPos[4];

        currentFadeTimer = fadeTimer;
    }

    //---------------------------------------------------------//
    // Changes Scene
    //---------------------------------------------------------//
    public void ChangeScene(int newLevelStateInt)
    {
        sfxPlayer.PlayOneShot(buttonClickSFX);
        MenuStates newMenuState = (MenuStates)newLevelStateInt;
        currentMenu = newMenuState;

        if(newMenuState == MenuStates.MAINMENU)
        {
            camTar.position = camTarPos[1];
        }
        else if(newMenuState == MenuStates.OPTIONS)
        {
            camTar.position = camTarPos[2];
        }
        else if(newMenuState == MenuStates.LEVELSELECT)
        {
            camTar.position = camTarPos[3];
        }
    }

    //---------------------------------------------------------//
    // Shows UI
    //---------------------------------------------------------//
    public void ShowUI(GameObject showUIObject)
    {
        storedState = currentMenu;
        currentMenu = MenuStates.SHOWUI;
        currentImage = showUIObject;

        currentImage.SetActive(true);
    }

    //---------------------------------------------------------//
    // Starts New Save
    //---------------------------------------------------------//
    public void NewSave(GameObject text)
    {
        GameObject.Find("SaveObject").GetComponent<SaveScript>().DeleteSave();
        text.GetComponent<TextMeshPro>().text = "New Save\nstarted.";

        //Resets level select
        currentLevel = "IntroCutScene";
        levelImageObject.GetComponent<SpriteRenderer>().sprite = levelImages[0];
        levelText.text = "Level 1";
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
        sfxPlayer.PlayOneShot(buttonClickSFX);
        Application.Quit();
        Debug.Log("Quit Game");
    }

    //Debug stuff
    private void OnGUI()
    {
        //GUI.Box(new Rect(10, 10, 150, 30), "Menu State:" + currentMenu.ToString());
    }
}