using UnityEngine;

public class Hint : MonoBehaviour
{
    public GameObject[] objectsWithHints;
    public bool[] usedHint;

    private string hintObject;
    private float hintVisibilityTime;

    // Use this for initialization
    void Start()
    {
        hintObject = "";
        hintVisibilityTime = 0.0f;
        usedHint = new bool[objectsWithHints.Length];
        for (int i = 0; i < usedHint.Length; i++)
        {
            usedHint[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position
            + new Vector3(2, 2.25f, 0);

        if (hintObject == "")
        {
            CheckForHintCollision();
        }
        else if (hintObject == "dangerous pressure plate")
        {
            GetComponent<SpriteRenderer>().enabled = true;
            hintVisibilityTime += Time.deltaTime;
            GameObject.Find("SpeechBubbleText").GetComponent<TextMesh>().text = "I better watch my step!";
        }
        else if (hintObject == "locked door")
        {
            GetComponent<SpriteRenderer>().enabled = true;
            hintVisibilityTime += Time.deltaTime;
            GameObject.Find("SpeechBubbleText").GetComponent<TextMesh>().text = "This door appears to be \r\nlocked. Maybe I should \r\nlook for a key or something \r\non the ground.";
        }

        if (hintVisibilityTime >= 1.5f)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            hintVisibilityTime = 0.0f;
            hintObject = "";
            GameObject.Find("SpeechBubbleText").GetComponent<TextMesh>().text = "";
        }
    }

    public void CheckForHintCollision()
    {
        for (int i = 0; i < objectsWithHints.Length; i++)
        {
            if (objectsWithHints[i].tag == "Key" && !usedHint[i])
            {
                if (GameObject.FindGameObjectWithTag("Player").transform.position.x <= objectsWithHints[i].transform.position.x + 2 ||
                    GameObject.FindGameObjectWithTag("Player").transform.position.x >= objectsWithHints[i].transform.position.x - 2)
                {
                    usedHint[i] = true;
                    hintObject = "key";
                }
            }
            else if (objectsWithHints[i].tag == "PressurePlate" && !usedHint[i])
            {
                if (objectsWithHints[i].GetComponent<PressurePlate>().effect != PressurePlate.EffectTypes.OPEN_DOOR)
                {
                    if (GameObject.FindGameObjectWithTag("Player").transform.position.x <= objectsWithHints[i].transform.position.x + 5 &&
                        GameObject.FindGameObjectWithTag("Player").transform.position.x >= objectsWithHints[i].transform.position.x - 5)
                    {
                        usedHint[i] = true;
                        hintObject = "dangerous pressure plate";
                    }
                }
            }
            else if (objectsWithHints[i].tag == "Door")
            {
                if (objectsWithHints[i].GetComponent<Door>().isLocked)
                {
                    if (GameObject.FindGameObjectWithTag("Player").transform.position.x <= objectsWithHints[i].transform.position.x + 5 &&
                        GameObject.FindGameObjectWithTag("Player").transform.position.x >= objectsWithHints[i].transform.position.x - 5)
                    {
                        hintObject = "locked door";
                    }
                }
            }
        }
    }

    public void ResetHint()
    {
        hintObject = "";
        hintVisibilityTime = 0.0f;
        for (int i = 0; i < usedHint.Length; i++)
        {
            usedHint[i] = false;
        }
    }
}
