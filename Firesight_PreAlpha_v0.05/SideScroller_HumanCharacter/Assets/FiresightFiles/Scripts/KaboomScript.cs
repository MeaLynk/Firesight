using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomScript : MonoBehaviour {

    public AudioClip kaboomSFX;
    public float kaboomTimer = 10.0f;
    public bool isTntFuseActivated = false;

    [Header("Used if there IS a platform Launching from the TNT blast")]
    public GameObject tntPlatform;
    public GameObject player;
    public float launchPower = 50;

    private float currentKaboomTimer = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Timer for kaboom
        if(currentKaboomTimer > 0)
        {
            currentKaboomTimer -= (1.00f * Time.deltaTime);
        }
    }

    //Gets called on collision
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fireball" && currentKaboomTimer <= 0 && isTntFuseActivated== false)
        {
            TriggerTNT(false);
        }
    }

    //Triggers the TNT
    public void TriggerTNT(bool isTntDestoryed)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(kaboomSFX);
        currentKaboomTimer = kaboomTimer;

        if (tntPlatform != null)
        {
            Vector3 newVelocity = new Vector3(tntPlatform.GetComponent<Rigidbody>().velocity.x, launchPower, tntPlatform.GetComponent<Rigidbody>().velocity.z);
            tntPlatform.GetComponent<Rigidbody>().velocity = newVelocity;
            player.GetComponent<FireScript>().QuitFireball();
        }
        else if(isTntDestoryed == true)
        {
            Destroy(gameObject, 2);
        }
    }

    //Debug UI
    private void OnGUI()
    {
        if(currentKaboomTimer > 0)
        {
            GUI.Box(new Rect(10, 400, 120, 40), "TnT Timer:\n" + currentKaboomTimer.ToString());
        }
    }
}
