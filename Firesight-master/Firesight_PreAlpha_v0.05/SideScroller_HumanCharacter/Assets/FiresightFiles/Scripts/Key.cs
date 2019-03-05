using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isCollected;
    public string keyHint;

    private Vector3 startPos;

    //-----------------------------------------------------------
    // Use this for initialization
    //-----------------------------------------------------------
    void Start()
    {
        isCollected = false;
        startPos = transform.position;
        keyHint = "there must be a key around here";
    }

    //-----------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------
    void Update()
    {
        if (isCollected)
            gameObject.SetActive(false);
    }

    //-----------------------------------------------------------
    // Used when an object enters the collider
    //-----------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Key collected");
            isCollected = true;
        }
    }

    //-----------------------------------------------------------
    // Used to reset the key
    //-----------------------------------------------------------
    public void ResetKey()
    {
        isCollected = false;
        gameObject.SetActive(true);
        transform.position = startPos;
    }
}