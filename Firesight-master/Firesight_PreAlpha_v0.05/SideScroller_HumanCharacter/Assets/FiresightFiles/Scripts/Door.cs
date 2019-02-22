using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] requirementObjects;
    public bool isLocked;

    // Use this for initialization
    void Start()
    {
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.name = "Door 2")
        for (int i = 0; i < requirementObjects.Length; i++)
        {
            if (requirementObjects[i].tag == "PressurePlate")
            {
             //  isLocked = requirementObjects[i].GetComponent<PressurePlate>().isActivated();
            }
           // else if (requirementObjects)
        }
    }
}
