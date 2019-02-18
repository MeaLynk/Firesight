using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    #region Public Variables
    public GameObject player;
    public string mainMenuSceneName;
    public GameObject pauseCanvas;
    public GameObject menuPopup;
    public GameObject quitPopup;

    [Header("Used for hiding pause menu buttons for popups")]
    public GameObject[] pauseMenuButtons;
    #endregion

    #region Private Variables
    private Vector3 storedVelocity;
    private int playerHealth;
    private bool isPaused;
    #endregion

    //---------------------------------------------------------//
    // Gets whether the game is paused
    //---------------------------------------------------------//
    public bool GetPausedState()
    {
        return isPaused;
    }

    //---------------------------------------------------------//
    // Sets the paused state of the game
    //---------------------------------------------------------//
    public void SetPausedState(bool inState)
    {
        isPaused = inState;
    }

    //---------------------------------------------------------//
    // Used to initialize script
    //---------------------------------------------------------//
    void Start()
    {
        // Set the possible menu to hidden and make the game
        // not paused when game starts
        pauseCanvas.SetActive(false);
        isPaused = false;
        playerHealth = player.GetComponent<Health>().currentHealth;
        pauseCanvas.SetActive(false);
        menuPopup.SetActive(false);
        quitPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player pauses or unpauses the game
        if (Input.GetButtonDown("Pause") && isPaused == false)
        {
            // If the game is not paused, the escape key pauses the game
            isPaused = true;

            pauseCanvas.SetActive(true);
            storedVelocity = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<PlayerMove>().enabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.GetComponent<Health>().enabled = false;
            player.GetComponent<FireScript>().enabled = false;   
            
            if(player.GetComponent<FireScript>().GetIsPlayerInControl() == false)
            {
                player.GetComponent<FireScript>().QuitFireball();
                Debug.Log("Fireball destoryed from pause.");
            }
        }
        else if (Input.GetButtonDown("Pause") && isPaused == true)
        {
            // If the game is paused, the escape key resumes the game
            ResumeGame();
        }
    }

    //---------------------------------------------------------//
    // Used when the player clicks the resume button
    //---------------------------------------------------------//
    public void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        player.GetComponent<Health>().enabled = true;
        player.GetComponent<FireScript>().enabled = true;
        player.GetComponent<Rigidbody>().velocity = storedVelocity;
    }

    //---------------------------------------------------------//
    // Used when the player clicks the quit button
    //---------------------------------------------------------//
    public void ChangeQuitPopup()
    {
        quitPopup.SetActive(!quitPopup.activeInHierarchy);

        for (int i = 0; i < pauseMenuButtons.Length; i++)
        {
            pauseMenuButtons[i].SetActive(!quitPopup.activeInHierarchy);
        }
    }

    //---------------------------------------------------------//
    // Used when the player clicks the menu button
    //---------------------------------------------------------//
    public void ChangeMenuPopup()
    {
        menuPopup.SetActive(!menuPopup.activeInHierarchy);

        for(int i = 0; i < pauseMenuButtons.Length; i++)
        {
            pauseMenuButtons[i].SetActive(!menuPopup.activeInHierarchy);
        }
    }

    //---------------------------------------------------------//
    // Used when the player clicks the menu button cancel
    //---------------------------------------------------------//
    public void LoadMenuButton()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    //---------------------------------------------------------//
    // Used when the player clicks the quit button confirmation
    //---------------------------------------------------------//
    public void QuitGameButton()
    {
        Application.Quit();
        Debug.Log("Player Quit Game");
    }

    //Debug Menu
    private void OnGUI()
    {
        //GUI.Box(new Rect(10, 100, 170, 40), "Player Velocity:\n" + player.GetComponent<Rigidbody>().velocity);
    }
}