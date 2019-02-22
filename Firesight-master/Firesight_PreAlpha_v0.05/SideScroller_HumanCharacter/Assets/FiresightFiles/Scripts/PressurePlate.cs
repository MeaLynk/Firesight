using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Public Variables
    public GameObject arrowPrefab;
    public GameObject spikesPrefab;
    public enum EffectTypes
    {
        SHOOT_ARROWS_FROM_LEFT,
        SHOOT_ARROWS_FROM_RIGHT,
        SPIKES,
        NONE
    };
    public EffectTypes effect;              //the effect the pressure plate will have
    public int arrowAmount;                 //the amount of arrows to be shot
    public float pressurePlateLowerSpeed;   //the speed the pressure plate will lower
    public bool isReactivatable;
    #endregion

    #region Private Variable
    private List<GameObject> arrows;
    private GameObject spikes;
    private Vector3 startingPos;            //stores object's starting pos for resetting
    private bool isActivated;               //bool for when the pressure plate is activated
    private bool isLowered;                 //bool for whether the presure plate has been lowered
    private bool spikesGenerated;           //bool for whether the spikes have been risen or not
    private float lowerAmount = 0.11f;      //the amount to lower the pressure plate by
    private float eventTimer;               //the timer for how long the event should last
    private float spikeHeight = 0.9f;
    private int arrowIndex = 0;
    #endregion

    // Use this for initialization
    void Start()
    {
        arrows = new List<GameObject>();
        isActivated = false;
        isLowered = false;
        spikesGenerated = false;
        eventTimer = 0.0f;
        startingPos = gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            //lowers the pressure plate and activates the sound it will use
            this.GetComponent<Collider>().enabled = false;
            if (!isLowered)
            {
                LowerPressurePlate(true);
            }
            #region Shoot Arrows Functionality
            if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT
                || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
            {
                eventTimer += Time.deltaTime;
                //continue to spawn arrows while we have less then specified
                if (eventTimer >= 1.5f && arrowIndex < arrowAmount)
                {
                    arrows.Add(SpawnArrow());
                    eventTimer = 0.0f;
                    arrowIndex++;
                }
                else if (eventTimer >= 1.5f && arrowIndex >= arrowAmount)
                {
                    isActivated = false;
                    if (isReactivatable)
                    {
                        for (int i = 0; i < arrows.Count; i++)
                        {
                            arrows.RemoveAt(i);
                        }
                        arrowIndex = 0;

                        eventTimer = 0.0f;
                    }
                }
                for (int i = 0; i < arrows.Count; i++)
                {
                    if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
                    {
                        arrows[i].transform.position = new Vector3(arrows[i].transform.position.x +
                            (10.0f * Time.deltaTime), arrows[i].transform.position.y,
                            arrows[i].transform.position.z);

                        if (arrows[i].transform.position.x >= GameObject.Find("Player").transform.position.x)
                        { 
                        Debug.Log("Arrow hit player");

                        arrows[i].SetActive(false);
                        arrows[i].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                        arrows.Remove(arrows[i]);
                        for (int x = i; x < arrows.Count - 1; x++)
                        {
                            arrows[x] = arrows[x + 1];
                        }
                    }
                }
                if (effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
                {
                    arrows[i].transform.position = new Vector3(arrows[i].transform.position.x -
                        (10.0f * Time.deltaTime), arrows[i].transform.position.y,
                        arrows[i].transform.position.z);
                    if (arrows[i].transform.position.x <= GameObject.Find("Player").transform.position.x)
                    {
                        Debug.Log("Arrow hit player");

                        arrows[i].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                        arrows[i].SetActive(false);
                        arrows.Remove(arrows[i]);
                        for (int x = i; x < arrows.Count - 1; x++)
                        {
                            arrows[x] = arrows[x + 1];
                        }
                    }
                }
            }
        }
        #endregion
        #region Spikes Functionality
        else if (effect == EffectTypes.SPIKES)
        {

            eventTimer += Time.deltaTime;
            if (!spikesGenerated && eventTimer >= 1.0f)

                eventTimer += Time.deltaTime;  //Spikes spawn a little too fast, try to wait a few miliseconds for the player to be in the middle of the presure plate to spawn them (test timing in the test scene)
            if (!spikesGenerated)
            {
                spikes = Instantiate(spikesPrefab);
                spikes.transform.position = new Vector3(this.transform.position.x - 0.7f,
                   transform.position.y - 1.2f, transform.position.z + 0.6f);
                spikesGenerated = true;
            }
            if (spikesGenerated && spikeHeight >= 0.0f)
            {
                spikes.transform.position = new Vector3(spikes.transform.position.x,
                    spikes.transform.position.y + (2.0f * Time.deltaTime), spikes.transform.position.z);
                spikeHeight -= (1.8f * Time.deltaTime);
            }
        }
        #endregion
    }
        if (!isActivated && isLowered && isReactivatable)
        {
            LowerPressurePlate(false);
            this.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.tag == "Player" && isActivated == false)
    {
        isActivated = true;
        Debug.Log("Presure Plate Activated");
    }
}

private GameObject SpawnArrow()
{
    GameObject newArrow = Instantiate(arrowPrefab);
    if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
    {
        newArrow.transform.position = new Vector3(this.transform.position.x - 10.0f,
            transform.position.y + 1.4f, GameObject.FindGameObjectWithTag("Player").transform.position.z);
    }
    else if (effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
    {
        newArrow.transform.position = new Vector3(this.transform.position.x + 10.0f,
            transform.position.y + 1.4f, GameObject.FindGameObjectWithTag("Player").transform.position.z);
    }
    return newArrow;
}

//Resets the presure plate and all of it's values for when the player respawns
public void ResetPresurePlate()
{
    if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            Destroy(arrows[i]);
        }

        arrows = new List<GameObject>();
        arrowIndex = 0;
        Debug.Log("Arrows Reset.");
    }
    else if (effect == EffectTypes.SPIKES)
    {
        Destroy(spikes);
        spikeHeight = 0.0f;
        spikesGenerated = false;
        Debug.Log("Spikes Reset.");
    }

    arrows = new List<GameObject>();
    spikes = new GameObject();
    isActivated = false;
    isLowered = false;
    eventTimer = 0.0f;
    gameObject.GetComponent<Transform>().position = startingPos;
    gameObject.GetComponent<Collider>().enabled = true;
    lowerAmount = 0.11f;
}

//---------------------------------------------------------------------//
//Used to lower or raise the pressure plate
private void LowerPressurePlate(bool lower)
{
    if (lower)
    {
        //play the audio for the pressure plate
        if (this.GetComponent<AudioSource>().isPlaying == false)
            this.GetComponent<AudioSource>().Play();

        //move the pressure plate down when the player steps on it
        this.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y - ((pressurePlateLowerSpeed / 100.0f) * Time.deltaTime), this.transform.position.z);
        lowerAmount -= ((pressurePlateLowerSpeed / 100.0f) * Time.deltaTime);

        if (lowerAmount <= 0.0f)
        {
            //when it is lowered, set isLowered true, and stop the audio
            isLowered = true;
            this.GetComponent<AudioSource>().Stop();
            lowerAmount = 0.11f;
        }
    }
    else if (!lower)
    {
        //play the audio for the pressure plate
        if (this.GetComponent<AudioSource>().isPlaying == false)
            this.GetComponent<AudioSource>().Play();

        //move the pressure plate down when the player steps on it
        this.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y + ((pressurePlateLowerSpeed / 100.0f) * Time.deltaTime), this.transform.position.z);
        lowerAmount -= ((pressurePlateLowerSpeed / 100.0f) * Time.deltaTime);

        if (lowerAmount <= 0.0f)
        {
            //when it is lowered, set isLowered true, and stop the audio
            isLowered = false;
            this.GetComponent<AudioSource>().Stop();
            lowerAmount = 0.11f;
        }
    }
}
}
