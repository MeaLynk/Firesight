using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    //--------------------------------------------------------------------
    // Public Members
    public enum HintType { LOCKED_DOOR, DANGEROUS_PRESSURE_PLATE, NONE };
    public HintType hintType;
    public bool hintUsed;
    public bool showHint;

    //--------------------------------------------------------------------
    // Private Members
    private GameObject player;
    private bool hintAreaLeft;
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
        else if (!hintUsed && hintType == HintType.DANGEROUS_PRESSURE_PLATE)
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
            if (hintType == HintType.DANGEROUS_PRESSURE_PLATE)
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
            GUI.Box(new Rect(600, Screen.height - 190, Screen.width - 1200, 160), "");

            GUIStyle style = new GUIStyle();
            style.fontSize = 42;
            style.alignment = TextAnchor.MiddleCenter;
            style.font = (Font)Resources.Load("HintFont");
            style.fixedHeight = 0.5f;

            if (hintType == HintType.LOCKED_DOOR && !hintUsed)
            {
                GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "This door appears to be locked.\r\nI better look for a key\r\nor something on the ground.", style);
            }
            else if (hintType == HintType.DANGEROUS_PRESSURE_PLATE && !hintUsed)
            {
                GUI.Label(new Rect(800, Screen.height - 110, Screen.width - 1600, 80), "I better watch my step,\r\nThis place coule be trapped!", style);
            }
            else if (hintUsed)
            {
                //hintText.text = "";
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