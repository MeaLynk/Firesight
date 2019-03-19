using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Public Variables
    public GameObject arrowPrefab;
    public GameObject spikesPrefab;
    public enum EffectTypes { SHOOT_ARROWS_FROM_LEFT, SHOOT_ARROWS_FROM_RIGHT, SPIKES, OPEN_DOOR, NONE };
    public EffectTypes effect;      //the effect the pressure plate will have
    public int arrowAmount;
    public float lowerAmount = 11.0f;                  //the amount to lower the pressure plate by
    public float lowerSpeed = 55.0f;
    #endregion

    #region Private Variable
    private GameObject[] arrows;
    private GameObject spikes;
    private Vector3 startingPos;                        //Stores object's starting pos for resetting
    public bool isActivated;                            //bool for when the pressure plate is activated
    private bool isLowered;                             //bool for whether the presure plate has been lowered
    private bool spikesGenerated;                       //bool for whether the spikes have been risen or not
    private float eventTimer;                           //the timer for how long the event should last
    private float spikeHeight = 0.0f;
    private int arrowIndex = 0;
    private float pressurePlateLowerAmount;
    #endregion

    // Use this for initialization
    void Start()
    {
        arrows = new GameObject[arrowAmount];
        isActivated = false;
        isLowered = false;
        spikesGenerated = false;
        eventTimer = 0.0f;
        startingPos = gameObject.GetComponent<Transform>().position;
        lowerAmount = lowerAmount * 0.01f;
        lowerSpeed = lowerSpeed * 0.001f;
        pressurePlateLowerAmount = lowerAmount;
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
                if (this.GetComponent<AudioSource>().isPlaying == false)
                    this.GetComponent<AudioSource>().Play();
                this.transform.position = new Vector3(this.transform.position.x,
                    this.transform.position.y - (lowerSpeed * Time.deltaTime), this.transform.position.z);
                lowerAmount -= (lowerSpeed * Time.deltaTime);
                if (lowerAmount <= 0.0f)
                {
                    isLowered = true;
                    this.GetComponent<AudioSource>().Stop();
                }
            }
            #region Shoot Arrows Functionality
            if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
            {
                eventTimer += Time.deltaTime;
                if (eventTimer >= 2.0f && arrowIndex < arrowAmount)  //continue to spawn arrows while we have less then specified
                {
                    SpawnArrow(arrowIndex);
                    eventTimer = 0.0f;
                    arrowIndex++;
                }
                for (int i = 0; i < arrowIndex; i++)
                {
                    if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
                    {
                        arrows[i].transform.position = new Vector3(arrows[i].transform.position.x +
                            (10.0f * Time.deltaTime), arrows[i].transform.position.y,
                            arrows[i].transform.position.z);
                    }
                    if (effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
                    {
                        arrows[i].transform.position = new Vector3(arrows[i].transform.position.x -
                            (10.0f * Time.deltaTime), arrows[i].transform.position.y,
                            arrows[i].transform.position.z);
                    }
                }
            }
            #endregion
            #region Spikes Functionality
            else if (effect == EffectTypes.SPIKES)
            {
                if (!spikesGenerated)
                {
                    spikes = Instantiate(spikesPrefab);
                    spikes.transform.position = new Vector3(this.transform.position.x - 0.7f,
                       transform.position.y - 1.4f, transform.position.z + 0.6f);
                    spikesGenerated = true;
                }
                if (spikesGenerated && spikeHeight <= 1.1f)
                {
                    eventTimer += Time.deltaTime;
                    if (eventTimer >= 2.0f)
                    {
                        spikes.transform.position = new Vector3(spikes.transform.position.x,
                            spikes.transform.position.y + (1.8f * Time.deltaTime), spikes.transform.position.z);
                        spikeHeight += (1.8f * Time.deltaTime);
                    }
                }
            }
            #endregion
            #region Open Door Functionality
            else if (effect == EffectTypes.OPEN_DOOR)
            {

            }
            #endregion
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

    private void SpawnArrow(int index)
    {
        arrows[index] = Instantiate(arrowPrefab);
        if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
        {
            arrows[index].transform.position = new Vector3(this.transform.position.x - 10.0f,
                transform.position.y + 1.3f, transform.position.z);
            arrows[index].transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
        {
            arrows[index].transform.position = new Vector3(this.transform.position.x + 10.0f,
                transform.position.y + 1.3f, transform.position.z);
        }
    }

    //Resets the presure plate and all of it's values for when the player respawns
    public void ResetPresurePlate()
    {
        if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                Destroy(arrows[i]);
            }

            arrows = new GameObject[arrowAmount];
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

        isActivated = false;
        isLowered = false;
        eventTimer = 0.0f;
        gameObject.GetComponent<Transform>().position = startingPos;
        gameObject.GetComponent<Collider>().enabled = true;
        lowerAmount = pressurePlateLowerAmount;
    }
}
