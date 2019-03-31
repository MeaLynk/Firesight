using UnityEngine;

public class DeathScript : MonoBehaviour
{

    [Header("Objects that will reset after death")]
    public GameObject[] pressurePlates;
    //maybe add reset animations trigger if it is needed

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Resets the all gameobjects to their default states
    public void ResetObjects()
    {
        for (int i = 0; i < pressurePlates.Length; i++)
        {
            if (pressurePlates[i] != null)
            {
                pressurePlates[i].GetComponent<PressurePlate>().ResetPresurePlate();
            }
        }
    }
}
