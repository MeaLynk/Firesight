using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour {

    [Header("2 = double speed.")]
    public float SprintSpeed = 2.0f;
    public bool showDebugUI = false;

    private bool isPlayerSprinting = false;
    private float storedMaxSpeed;
    private float storedAccl;

	// Use this for initialization
	void Start ()
    {
        storedMaxSpeed = gameObject.GetComponent<PlayerMove>().maxSpeed;
        storedAccl = gameObject.GetComponent<PlayerMove>().accel;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Sprint"))
        {
            gameObject.GetComponent<PlayerMove>().maxSpeed = storedMaxSpeed * SprintSpeed;
            gameObject.GetComponent<PlayerMove>().accel = storedAccl * SprintSpeed;
            Debug.Log("Player started sprinting.");
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            gameObject.GetComponent<PlayerMove>().maxSpeed = storedMaxSpeed;
            gameObject.GetComponent<PlayerMove>().accel = storedAccl;
        }
	}

    private void OnGUI()
    {
        if (showDebugUI == true)
        {
            GUI.Box(new Rect(10, 400, 120, 40), "Player Speed:\n" + gameObject.GetComponent<Rigidbody>().velocity.ToString());
        }
    }
}
