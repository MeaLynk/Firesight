using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Public Variables
    public float lowerAmount = 11.0f;                  //the amount to lower the pressure plate by
    public float lowerSpeed = 55.0f;
    #endregion

    #region Private Variable
    private Vector3 startingPos;                        //Stores object's starting pos for resetting
    public bool isActivated;                            //bool for when the pressure plate is activated
    private bool isLowered;                             //bool for whether the presure plate has been lowered
    private float pressurePlateLowerAmount;
    #endregion

    // Use this for initialization
    void Start()
    {
        isActivated = false;
        isLowered = false;
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
            if (!isLowered)
            {
                this.transform.position = new Vector3(this.transform.position.x,
                    this.transform.position.y - (lowerSpeed * Time.deltaTime), this.transform.position.z);
                lowerAmount -= (lowerSpeed * Time.deltaTime);
                if (lowerAmount <= 0.0f)
                {
                    isLowered = true;
                    this.GetComponent<AudioSource>().Stop();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isActivated == false)
        {
            isActivated = true;
            this.GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Debug.Log("Presure Plate Activated");
        }
    }

    //Resets the presure plate and all of it's values for when the player respawns
    public void ResetPresurePlate()
    {

        isActivated = false;
        isLowered = false;
        GetComponent<Transform>().position = startingPos;
        lowerAmount = pressurePlateLowerAmount;
        //GetComponent<TriggerScript>().isActivated = false;
        //GetComponent<TriggerScript>().hasBeenActivatedAlready = false;
        GetComponent<MeshCollider>().enabled = true;
    }
}
