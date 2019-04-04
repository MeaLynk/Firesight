using UnityEngine;

public class Hint : MonoBehaviour
{
    //--------------------------------------------------------------------
    // Public Members
    public enum HintType { LOCKED_DOOR, DANGER_AHEAD, PYRE, BURNABLE_STRUCTURE, FIREBALL_USE, NONE };
    public HintType hintType;
    public bool hintUsed;
    public bool showHint;
    public bool isPan;
    public bool lightUpFireball = false;
    public bool fireballLit = false;
    public GameObject panTarget;
    public GameObject fireBallParticles;

    //--------------------------------------------------------------------
    // Private Members
    private GameObject player;
    private bool hintAreaLeft;
    private bool cameraPanned;
    private float hintTimer;
    private CameraFollow cFollow;

    //--------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hintAreaLeft = false;
        hintUsed = false;
        showHint = false;
        hintTimer = 0.0f;
        cFollow = Camera.main.GetComponent<CameraFollow>();
    }

    //--------------------------------------------------------------------
    // Update called once per frame
    //--------------------------------------------------------------------
    private void Update()
    {
        //if the player has left the hint area and the hint hasn't been displayed yet
        if (showHint && !hintUsed && hintType == HintType.LOCKED_DOOR)
        {
            player.GetComponent<PlayerMove>().grounded = false;
            player.transform.position = new Vector3(player.transform.position.x, -62.994f, player.transform.position.z);
            hintTimer += Time.deltaTime;
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            if (isPan && !cameraPanned && hintTimer >= 3.0f)
            {
                cFollow.followSpeed = 3.0f;
                cFollow.target = panTarget.transform;
                cFollow.target.position = panTarget.transform.position + new Vector3(0.0f, 5.0f, 0.0f);
                cameraPanned = true;

            }
            if (isPan && cameraPanned)
            {
                cFollow.targetOffset = new Vector3(-3.0f, 0.0f, -1.5f);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
                if (isPan && cameraPanned)
                {
                    cFollow.followSpeed = 1.5f;
                    cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                    cFollow.targetOffset = new Vector3(0.0f, 0.0f, -7.5f);
                    cFollow.followSpeed = 10;
                    cameraPanned = false;
                }
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
            }
        }
        else if (showHint && hintType == HintType.DANGER_AHEAD)
        {
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;

            hintTimer += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.PYRE)
        {
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;

            hintTimer += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.BURNABLE_STRUCTURE)
        {
            hintTimer += Time.deltaTime;
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            if (isPan)
            {
                cFollow.followSpeed = 1.5f;
                cFollow.target = panTarget.transform;
                cameraPanned = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
                if (isPan)
                {
                    cFollow.followSpeed = 1.5f;
                    cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                    cFollow.followSpeed = 10;
                    cameraPanned = false;
                }
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
            }
        }
        else if (showHint && hintType == HintType.FIREBALL_USE)
        {
            if (panTarget != null)
            {
                cFollow.target = panTarget.transform;
                cFollow.targetOffset = new Vector3(0.0f, 2.0f, -3.0f);
                cameraPanned = true;
            }
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
                player.GetComponent<FireScript>().enabled = true;
                fireBallParticles.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                lightUpFireball = true;
                if (panTarget != null)
                {
                    cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                    cFollow.targetOffset = new Vector3(0.0f, 0.0f, -7.5f);
                    cFollow.followSpeed = 10;
                    cameraPanned = false;
                }
            }
        }
        if (lightUpFireball)
        {
            if (fireBallParticles.transform.localScale.x < 2.5f ||
                fireBallParticles.transform.localScale.y < 2.5f ||
                fireBallParticles.transform.localScale.z < 2.5f)
            {
                fireBallParticles.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
            if (fireBallParticles.transform.localScale.x >= 0.25f &&
                fireBallParticles.transform.localScale.y >= 0.25f &&
                fireBallParticles.transform.localScale.z >= 0.25f)
            {
                lightUpFireball = false;
                fireballLit = true;
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
            }
        }
    }

    //--------------------------------------------------------------------
    // What happens when something enters the collider
    //--------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        else if (!hintUsed)
        {
            showHint = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    //--------------------------------------------------------------------
    // What happens when something exits the collider
    //--------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        hintAreaLeft = true;
    }

    //--------------------------------------------------------------------
    // Display UI stuff to the user
    //--------------------------------------------------------------------
    private void OnGUI()
    {
        if (showHint)
        {
            GUIStyle playerText = new GUIStyle();
            playerText.fontSize = 42;
            playerText.alignment = TextAnchor.MiddleCenter;
            playerText.font = (Font)Resources.Load("Enchanted Land");
            playerText.fixedHeight = 0.5f;
            playerText.normal.textColor = Color.yellow;

            GUIStyle gameText = new GUIStyle();
            gameText.fontSize = 42;
            gameText.alignment = TextAnchor.MiddleCenter;
            gameText.font = (Font)Resources.Load("Enchanted Land");
            gameText.fixedHeight = 0.5f;
            gameText.normal.textColor = Color.white;
            Texture textBackground = (Texture)Resources.Load("textBackground");

            if (hintType == HintType.LOCKED_DOOR && !hintUsed)
            {
                //GUI.DrawTexture(new Rect((Screen.width / 4), Screen.height - 125, Screen.width / 2, 200), textBackground);

                if (hintTimer <= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), @"""Looks like a dead end. I should try looking around...""", playerText);
                if (hintTimer >= 4.5f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "Your Fireball can reach places you can't...\nPress Shift to continue.", gameText);
            }
            else if (hintType == HintType.DANGER_AHEAD && !hintUsed)
            {
                //GUI.DrawTexture(new Rect((Screen.width / 4), Screen.height - 125, Screen.width / 2, 200), textBackground);

                if (hintTimer <= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), @"""This place is a deathtrap! I better be cautious...""", playerText);
                if (hintTimer >= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "The crypt is filled with fatal traps. Avoid them if you want to survive!\nPress Shift to continue.", gameText);
            }
            else if (hintType == HintType.PYRE && !hintUsed)
            {
                //GUI.DrawTexture(new Rect((Screen.width / 4), Screen.height - 125, Screen.width / 2, 200), textBackground);

                if (hintTimer <= 3.5f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), @"""This place looks safe... If I run into trouble I'll make my way back here!""", playerText);
                if (hintTimer >= 3.5f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "When you die, you'll respawn at the last Pyre you activated!\nPress Shift to continue.", gameText);
            }
            else if (hintType == HintType.BURNABLE_STRUCTURE && !hintUsed && cameraPanned)
            {
                //GUI.DrawTexture(new Rect((Screen.width / 4), Screen.height - 125, Screen.width / 2, 200), textBackground);

                if (hintTimer <= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), @"""Those scaffolds look weak...""", playerText);
                if (hintTimer >= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "Some scaffolds can be burnt down to create a path!\nPress Shift to continue.", gameText);
            }
            else if (hintType == HintType.FIREBALL_USE && !hintUsed)
            {
                //GUI.DrawTexture(new Rect((Screen.width / 4), Screen.height - 125, Screen.width / 2, 200), textBackground);

                if (hintTimer <= 4.0f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "\"Can't see a thing in here... Time to make some light!\"\nPress Shift to activate your fireball.", playerText);
                // if (showGUI)
                //    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "Press shift to light\nPress Shift to continue.", gameText);
            }
        }
    }

    //--------------------------------------------------------------------
    // Used to reset the hints upon death 
    //--------------------------------------------------------------------
    public void ResetHint()
    {
        hintAreaLeft = false;
        hintTimer = 0.0f;
        hintUsed = false;
        showHint = false;
    }
}