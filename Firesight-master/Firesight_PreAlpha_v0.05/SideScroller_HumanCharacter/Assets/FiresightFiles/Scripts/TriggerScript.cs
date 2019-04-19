using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    public GameObject panTarget;
    public bool hasAnimationObjectSound;
    public bool hasAnimationObjectHint;
    public bool hasDoorPan;
    public CameraFollow cFollow;

    //make varibles like these that only change in the code private so this can't be messed with in unity
    private bool hasBeenActivatedAlready = false;
    private bool isPanned = true;
    public float delayTimer = 0.0f;
    public bool isActivated = false;

    // Use this for initialization
    void Start()
    {
        cFollow = Camera.main.GetComponent<CameraFollow>();
        if (hasDoorPan)
        {
            isPanned = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && !hasBeenActivatedAlready)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= 2.0f)
            {
                if (hasDoorPan && !isPanned)
                {
                    cFollow.followSpeed = 1.5f;
                    cFollow.target = panTarget.transform;
                } 
                objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
                objectWithAnimation.GetComponent<Animator>().Play("DoorOpen");
                if (hasAnimationObjectHint)
                    objectWithAnimation.GetComponent<Hint>().hintUsed = true;
                if (hasAnimationObjectSound)
                {
                    objectWithAnimation.GetComponent<AudioSource>().PlayOneShot(objectWithAnimation.GetComponent<AudioSource>().clip);
                    hasAnimationObjectSound = false;
                }
                if (delayTimer >= 5.0f)
                {
                    isPanned = true;
                }
                if (isPanned)
                {
                    delayTimer = 0.0f;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>().QuitFireball();

                    cFollow.followSpeed = 1.0f;
                    cFollow.target = GameObject.FindGameObjectWithTag("CamTar").transform;
                    cFollow.followSpeed = 10;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>().enabled = true;

                    hasBeenActivatedAlready = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball" || other.tag == "Player")
        {
            isActivated = true;
        }
    }

    public void ResetAnims()
    {
        isActivated = false;
        hasBeenActivatedAlready = false;
        delayTimer = 0;
    }
}
