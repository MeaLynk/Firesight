using UnityEngine;

public class Door : MonoBehaviour
{
    #region Public Members
    public GameObject[] requirementObjects;
    public Hint hint;
    public bool isLocked;
    public bool isClosed;
    public float doorLiftLimit = 0.0f;
    #endregion

    #region Private Members
    private float liftAmount;
    private Vector3 startPos;
    private bool triggerLeave = false;
    private float triggerLeaveTimer;
    #endregion

    //-----------------------------------------------------------
    // Use this for initialization
    //-----------------------------------------------------------
    void Start()
    {
        isLocked = true;
        isClosed = true;
        liftAmount = 0.0f;
        doorLiftLimit = doorLiftLimit * 0.1f;
        startPos = transform.position;
        triggerLeave = false;
        triggerLeaveTimer = 0.0f;

    }

    //-----------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------
    void Update()
    {
        //if door is unlocked, open it
        if (!isLocked && isClosed)
        {
            if (GetComponent<AudioSource>().isPlaying == false)
                GetComponent<AudioSource>().Play();
            this.transform.position = new Vector3(this.transform.position.x,
                this.transform.position.y + (3 * Time.deltaTime), this.transform.position.z);
            liftAmount += (3 * Time.deltaTime);
            if (liftAmount >= doorLiftLimit)
            {
                isClosed = false;
                Debug.Log("Door Open");
                GetComponent<AudioSource>().Stop();
            }
        }
        if (!isLocked)
        {
            hint.hintUsed = true;
            GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    //-----------------------------------------------------------
    // Used when an object enters the collider
    //-----------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isLocked)
        {
            isLocked = CheckForDoorLock();
        }
    }
    //-----------------------------------------------------------
    // Checks to see if the door is still locked
    //-----------------------------------------------------------
    public bool CheckForDoorLock()
    {
        for (int i = 0; i < requirementObjects.Length; i++)
        {
            if (requirementObjects[i].tag == "PressurePlate")
            {
                if (!requirementObjects[i].GetComponent<PressurePlate>().isActivated)
                {
                    Debug.Log("Door is locked");
                    return true;
                }
            }
            else if (requirementObjects[i].tag == "Key")
            {
                if (!requirementObjects[i].GetComponent<Key>().isCollected)
                {
                    Debug.Log("Door is locked");
                    return true;
                }
            }
        }
        Debug.Log("Door Unlocked");
        return false;
    }

    //-----------------------------------------------------------
    // Used to reset the door when needed
    //-----------------------------------------------------------
    public void ResetDoor()
    {
        transform.position = startPos;
        isLocked = true;
        isClosed = true;
        liftAmount = 0.0f;
    }
}