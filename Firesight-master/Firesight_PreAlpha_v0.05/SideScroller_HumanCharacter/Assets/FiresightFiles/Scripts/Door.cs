using UnityEngine;

public class Door : MonoBehaviour
{
    #region Public Members
    public GameObject[] requirementObjects;
    public bool isLocked;
    public bool isClosed;
    public float doorLiftLimit = 0.0f;
    public int collisionCounter;
    public GameObject hint;
    #endregion

    #region Private Members
    private string hintString;
    private float liftAmount;
    private Vector3 startPos;
    private bool keyActivated = false;
    private bool pressurePlateActivated = false;
    private bool triggerLeave = false;
    private float triggerLeaveTimer;
    #endregion

    //-----------------------------------------------------------
    // Use this for initialization
    //-----------------------------------------------------------
    void Start()
    {
        hintString = "This door appears to be locked, ";
        for (int i = 0; i < requirementObjects.Length; i++)
        {
            if (requirementObjects[i].tag == "Key" && !keyActivated)
            {
                hintString += requirementObjects[i].GetComponent<Key>().keyHint;
                keyActivated = true;
            }
            else if (requirementObjects[i].tag == "PressurePlate" && !pressurePlateActivated)
            {
                if (!keyActivated)
                    hintString += (" or " + requirementObjects[i].GetComponent<PressurePlate>().pressurePlateHint);
                else
                    hintString += requirementObjects[i].GetComponent<PressurePlate>().pressurePlateHint;
                pressurePlateActivated = true;
            }
        }
        isLocked = true;
        isClosed = true;
        collisionCounter = 0;
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
        hint.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position
            + new Vector3(1.5f, 2, 0);

        if (triggerLeave == true)
        {
            triggerLeaveTimer += Time.deltaTime;
            if (triggerLeaveTimer > 2.0f)
            {
                triggerLeave = false;
                triggerLeaveTimer = 0.0f;
                hint.SetActive(false);
            }
        }

    }

    private void OnGUI()
    {
        if (hint.activeSelf)
        {
            GUI.Label(new Rect(600, 50, 150, 100), hintString);
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
            if (isLocked == true)
                collisionCounter++;

            if (collisionCounter > 5)
            {
                hint.SetActive(true);
            }
        }
    }

    //-----------------------------------------------------------
    // Used when an object exits the collider
    //-----------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && hint.activeSelf)
        {
            triggerLeave = true;
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