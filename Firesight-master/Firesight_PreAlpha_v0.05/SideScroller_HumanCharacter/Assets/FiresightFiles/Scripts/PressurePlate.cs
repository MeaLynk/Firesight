using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Public Variables
    public GameObject arrowPrefab;
    public enum EffectTypes { SHOOT_ARROWS_FROM_LEFT, SHOOT_ARROWS_FROM_RIGHT, SHOOT_ARROWS_FROM_TOP, NONE };
    public EffectTypes effect;      //the effect the pressure plate will have
    #endregion

    #region Private Variable
    private GameObject[] arrows = new GameObject[3];
    public bool isActivated;                            //bool for when the pressure plate is activated
    private bool isLowered;                             //bool for whether the presure plate has been lowered
    private float lowerAmount = 0.11f;                  //the amount to lower the pressure plate by
    private float eventTimer;                           //the timer for how long the event should last
    private int arrowIndex = 0;
    #endregion

    // Use this for initialization
    void Start()
    {
        isActivated = false;
        isLowered = false;
        eventTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            this.GetComponent<Collider>().enabled = false;
            //lowers the pressure plate and activates the sound it will use
            if (!isLowered)
            {
                if (this.GetComponent<AudioSource>().isPlaying == false)
                    this.GetComponent<AudioSource>().Play();
                this.transform.position = new Vector3(this.transform.position.x,
                    this.transform.position.y - (0.055f * Time.deltaTime), this.transform.position.z);
                lowerAmount -= (0.055f * Time.deltaTime);
                if (lowerAmount <= 0.0f)
                {
                    isLowered = true;
                    this.GetComponent<AudioSource>().Stop();
                }
            }
            #region Shoot Arrows Functionality
            if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT
                || effect == EffectTypes.SHOOT_ARROWS_FROM_TOP)
            {
                eventTimer += Time.deltaTime;
                if (eventTimer >= 2.0f && arrowIndex <= 2)  //continue to spawn arrows until 3 have been shot
                {
                    ShootArrow(arrowIndex);
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActivated = true;
        }
    }

    private void ShootArrow(int index)
    {
        arrows[index] = Instantiate(arrowPrefab);
        if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
        {
            arrows[index].transform.position = new Vector3(this.transform.position.x - 10.0f,
                transform.position.y + 1.6f, transform.position.z);
        }
        else if (effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT)
        {
            arrows[index].transform.position = new Vector3(this.transform.position.x + 10.0f,
                transform.position.y + 1.6f, transform.position.z);
        }
    }
}
