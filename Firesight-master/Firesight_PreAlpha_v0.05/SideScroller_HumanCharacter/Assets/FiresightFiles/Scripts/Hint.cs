using UnityEngine;

public class Hint : MonoBehaviour
{
    //--------------------------------------------------------------------
    // Public Members
    public enum HintType { LOCKED_DOOR, TRAP_DOOR, LOOK_AHEAD, DANGER_AHEAD, PYRE, BURNABLE_STRUCTURE, NONE };
    public HintType hintType;
    public bool hintUsed;
    public bool showHint;
    public bool IsPan;
    public GameObject panTarget;

    //--------------------------------------------------------------------
    // Private Members
    private GameObject player;
    private bool hintAreaLeft;
    private bool cameraPanned;
    private float hintTimer;

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
    }

    //--------------------------------------------------------------------
    // Update called once per frame
    //--------------------------------------------------------------------
    private void Update()
    {
        //if the player has left the hint area and the hint hasn't been displayed yet
        if (hintAreaLeft == true && !hintUsed && hintType == HintType.LOCKED_DOOR)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 3.0f)
            {
                hintUsed = false;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.DANGER_AHEAD)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 3.0f)
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.PYRE)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 4.0f)
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.BURNABLE_STRUCTURE)
        {
            CameraFollow cFollow = Camera.main.GetComponent<CameraFollow>();
            cFollow.followSpeed = 1.5f;
            cFollow.target = panTarget.transform;
            cameraPanned = true;
            hintTimer += Time.deltaTime;
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            if (hintTimer >= 5.5f)
            {
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
                cFollow.followSpeed = 1.5f;
                cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                cFollow.followSpeed = 10;
                cameraPanned = false;
                player.GetComponent<FireScript>().enabled = true;
            }
        }
        else if (showHint && hintType == HintType.TRAP_DOOR)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 3.0f)
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
            }
        }
        else if (showHint && hintType == HintType.LOOK_AHEAD)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 3.0f)
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
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
            if (hintType == HintType.DANGER_AHEAD || hintType == HintType.BURNABLE_STRUCTURE || hintType == HintType.LOOK_AHEAD)
            {
                this.GetComponent<BoxCollider>().enabled = false;
            }
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
            GUIStyle style = new GUIStyle();
            style.fontSize = 42;
            style.alignment = TextAnchor.MiddleCenter;
            style.font = (Font)Resources.Load("Enchanted Land");
            style.fixedHeight = 0.5f;
            style.normal.textColor = Color.white;
            //Texture scrollTex = (Texture)Resources.Load("backgroundtext");

            if (hintType == HintType.LOCKED_DOOR && !hintUsed)
            {
               // GUI.DrawTexture(new Rect(600, Screen.height - 190, Screen.width - 1200, 160), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "This door appears to be locked. Maybe my fireball could be used to open it.", style);
            }
            else if (hintType == HintType.DANGER_AHEAD && !hintUsed)
            {
              //  GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "I better watch my step, This place could be booby trapped!", style);
            }
            else if (hintType == HintType.PYRE && !hintUsed)
            {
              //  GUI.DrawTexture(new Rect(200, Screen.height - 180, Screen.width - 400, 160), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "This is a pyre, when you walk near it, it will light up. They serve as checkpoints when you perish. Good Luck!", style);
            }
            else if (hintType == HintType.BURNABLE_STRUCTURE && !hintUsed && cameraPanned)
            {
               // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "It's always a good idea to look for things that can be burnt!", style);
            }
            else if (hintType == HintType.TRAP_DOOR && !hintUsed)
            {
               // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "My fireball might be able to get to areas I can't fit...", style);
            }
            else if (hintType == HintType.LOOK_AHEAD && !hintUsed)
            {
               // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "That's a long drop, I better use my fireball to look ahead...", style);
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