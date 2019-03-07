using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    //--------------------------------------------------------------------
    // Public Members
    public enum HintType { LOCKED_DOOR, DANGEROUS_PRESSURE_PLATE, NONE };
    public HintType hintType;
    public bool hintUsed;

    //--------------------------------------------------------------------
    // Private Members
    private GameObject speechBubble;
    private GameObject player;
    private TextMesh hintText;
    private bool hintAreaLeft;
    private float hintTimer;

    //--------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------
    private void Start()
    {
        speechBubble = GameObject.Find("SpeechBubble");
        player = GameObject.FindGameObjectWithTag("Player");
        hintText = GameObject.Find("SpeechBubbleText").GetComponent<TextMesh>();
        hintAreaLeft = false;
        hintUsed = false;
        hintTimer = 0.0f;
    }

    //--------------------------------------------------------------------
    // Update called once per frame
    //--------------------------------------------------------------------
    private void Update()
    {
        //if speech bubble is enabled
        if (speechBubble.GetComponent<SpriteRenderer>().enabled == true)
        {
            //make speech bubble follow the player
            speechBubble.transform.position = player.transform.position + new Vector3(2, 2.25f, 0);

            if (hintType == HintType.LOCKED_DOOR && !hintUsed)
            {
                hintText.text = "This door appears to be \r\nlocked. I better look for a \r\nkey or something on the \r\nground.";
            }
            else if (hintType == HintType.DANGEROUS_PRESSURE_PLATE && !hintUsed)
            {
                hintText.text = "I better watch my step.";
            }
            else if (hintUsed)
            {
                hintText.text = "";
            }
        }
        //if the player has left the hint area and the hint hasn't been displayed yet
        if (hintAreaLeft == true && !hintUsed)
        {
            hintTimer += Time.deltaTime;
            if (hintTimer >= 3.0f)
            {
                if (hintType != HintType.LOCKED_DOOR)
                {
                    hintUsed = true;
                }
                hintTimer = 0.0f;
                speechBubble.GetComponent<SpriteRenderer>().enabled = false;
                hintText.text = "";
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
            speechBubble.GetComponent<SpriteRenderer>().enabled = true;
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
    // Used to reset the hints upon death 
    //--------------------------------------------------------------------
    public void ResetHint()
    {
        speechBubble.GetComponent<SpriteRenderer>().enabled = false;
        hintText.text = ""; ;
        hintAreaLeft = false;
        hintTimer = 0.0f;
        hintUsed = false;
    }
}