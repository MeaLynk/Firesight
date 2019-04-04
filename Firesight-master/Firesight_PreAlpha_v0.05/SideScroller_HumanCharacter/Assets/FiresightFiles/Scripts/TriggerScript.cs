using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    [Header("does Animation Object Have Sound")]
    public bool doesAnimationObjectHaveSound;
    [Header("does Animation Object Have Hint")]
    public bool doesAnimationObjectHaveHint;
    public CameraFollow cFollow;
    public float delayTime = 0.0f;

    //make varibles like these that only change in the code private so this can't be messed with in unity
    public bool hasBeenActivatedAlready = false;
    private float delayTimer = 0.0f;
    public bool isActivated = false;

    // Use this for initialization
    void Start()
    {
        cFollow = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && !hasBeenActivatedAlready)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayTime)
            {
                objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
                objectWithAnimation.GetComponent<Animator>().Play("DoorOpen");
                if (doesAnimationObjectHaveHint)
                    objectWithAnimation.GetComponent<Hint>().hintUsed = true;
                if (doesAnimationObjectHaveSound)
                    objectWithAnimation.GetComponent<AudioSource>().Play();

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
