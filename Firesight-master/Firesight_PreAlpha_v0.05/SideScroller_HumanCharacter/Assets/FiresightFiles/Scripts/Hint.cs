using UnityEngine;

public class Hint : MonoBehaviour
{
    //--------------------------------------------------------------------
    // Public Members
    public enum HintType { LOCKED_DOOR, TRAP_DOOR, LOOK_AHEAD, DANGER_AHEAD, PYRE, BURNABLE_STRUCTURE, FIREBALL_USE, NONE };
    public HintType hintType;
    public bool hintUsed;
    public bool showHint;
    public bool isPan;
    public GameObject panTarget;

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
            if (isPan && !cameraPanned && hintTimer >= 2.5f)
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

            if ((Input.GetKeyUp(KeyCode.LeftShift) && hintTimer >= 4.5f) || hintTimer >= 9.5f)
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
            if (hintTimer >= 4.0f || Input.GetKeyUp(KeyCode.LeftShift))
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
        else if (showHint && hintType == HintType.TRAP_DOOR)
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
        else if (showHint && hintType == HintType.FIREBALL_USE)
        {
            cFollow.target = panTarget.transform;
            cFollow.targetOffset = new Vector3(0.0f, 2.0f, -3.0f);
            cameraPanned = true;
            player.GetComponent<Rigidbody>().Sleep();
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                hintUsed = true;
                hintTimer = 0.0f;
                showHint = false;
                cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                cFollow.targetOffset = new Vector3(0.0f, 0.0f, -7.5f);
                cFollow.followSpeed = 10;
                cameraPanned = false;
                player.GetComponent<Rigidbody>().WakeUp();
                player.GetComponent<PlayerMove>().isPlayerInControl = true;
                player.GetComponent<FireScript>().enabled = true;
            }
        }
        else if (showHint && hintType == HintType.LOOK_AHEAD)
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
        if (showHint) //GUI IS FOR DEBUG, NOT UI. next time use canvases for UI
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 42;
            style.alignment = TextAnchor.MiddleCenter;
            style.font = (Font)Resources.Load("Enchanted Land");
            style.fixedHeight = 0.5f;
            style.normal.textColor = Color.white;

            GUIStyle newStyle = new GUIStyle();
            newStyle.fontSize = 38;
            newStyle.alignment = TextAnchor.MiddleCenter;
            newStyle.font = (Font)Resources.Load("Enchanted Land");
            newStyle.fixedHeight = 0.5f;
            newStyle.normal.textColor = Color.white;
            //Texture scrollTex = (Texture)Resources.Load("backgroundtext");

            if (hintType == HintType.LOCKED_DOOR && !hintUsed)
            {
                // GUI.DrawTexture(new Rect(600, Screen.height - 190, Screen.width - 1200, 160), scrollTex);
                if (hintTimer <= 3.0f)
                    GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "This door appears to be locked. Maybe my fireball could be used to open it.", style);
                if (hintTimer >= 4.5f)
                    GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.DANGER_AHEAD && !hintUsed)
            {
                //  GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "I better watch my step, This place could be booby trapped!", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.PYRE && !hintUsed)
            {
                //  GUI.DrawTexture(new Rect(200, Screen.height - 180, Screen.width - 400, 160), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "This is a pyre, when you walk near it, it will light up. They serve as checkpoints when you perish. Good Luck!", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.BURNABLE_STRUCTURE && !hintUsed && cameraPanned)
            {
                // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "It's always a good idea to look for things that can be burnt!", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.TRAP_DOOR && !hintUsed)
            {
                // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "My fireball might be able to get to areas I can't fit...", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.LOOK_AHEAD && !hintUsed)
            {
                // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "That's a long drop, I better use my fireball to look ahead...", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
            }
            else if (hintType == HintType.FIREBALL_USE && !hintUsed)
            {
                // GUI.DrawTexture(new Rect(400, Screen.height - 180, Screen.width - 800, 170), scrollTex);
                GUI.Label(new Rect(800, Screen.height - 100, Screen.width - 1600, 50), "I wonder what will happen if I press shift...", style);
                GUI.Label(new Rect(800, Screen.height - 60, Screen.width - 1600, 80), "SKIP (SHIFT)", newStyle);
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