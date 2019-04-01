using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    [Header("Does object have sound")]
    public bool doesAnimationObjectHaveSound;
    [Header("Does object have hint")]
    public bool doesAnimationObjectHaveHint;
    public CameraFollow cFollow;

    public bool isActivated = false;
    public bool hasBeenActivatedAlready = false;
    public float delayTimer = 0.0f;
    public float delayTime = 0.0f;

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
        if (other.tag == "Fireball")
        {
            isActivated = true;
        }

        else if (other.tag == "Player")
        {
            isActivated = true;
        }
    }

    //maybe add reset here if it is needed
}
