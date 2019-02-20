using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Public Variables
    public GameObject arrowPrefab;
    public enum EffectTypes { SHOOT_ARROWS_FROM_LEFT, SHOOT_ARROWS_FROM_RIGHT, SHOOT_ARROWS_FROM_TOP, NONE };
    public EffectTypes effect;
    #endregion

    #region Private Variable
    private GameObject[] arrows = new GameObject[3];
    public bool isActivated;
    private bool isLowered;
    private float lowerAmount = 0.11f;
    private float eventTimer;
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
            //determines what direction the arrows are being shot from
            if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT || effect == EffectTypes.SHOOT_ARROWS_FROM_RIGHT
                || effect == EffectTypes.SHOOT_ARROWS_FROM_TOP)
            {
                eventTimer += Time.deltaTime;
                if (eventTimer >= 2.0f && arrowIndex <= 2)
                {
                    ShootArrow(arrowIndex);
                    eventTimer = 0.0f;
                    arrowIndex++;
                }
                else if (arrowIndex > 2 && eventTimer >= 2.0f)
                {
                    arrowIndex = 0;
                    eventTimer = 0.0f;
                    isActivated = false;
                }
                for (int i = 0; i < arrowIndex; i++)
                {
                    if (effect == EffectTypes.SHOOT_ARROWS_FROM_LEFT)
                    {
                        arrows[i].transform.position = new Vector3(arrows[i].transform.position.x +
                            (10.0f * Time.deltaTime), arrows[i].transform.position.y,
                            arrows[i].transform.position.z);
                    }
                }

            }
        }
        this.GetComponent<Collider>().enabled = true;
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
                transform.position.y + 1.8f, transform.position.z);
        }
    }
}
