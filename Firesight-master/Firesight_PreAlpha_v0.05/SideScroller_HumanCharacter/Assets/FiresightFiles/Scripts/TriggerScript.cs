using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    [Header("Use 'Trigger' for the name of the trigger in Animator")]
    public GameObject objectWithAnimation;
    public GameObject panTarget;
    public float timeBeforeActivation = 0;
    public bool isPan;
    public CameraFollow cFollow;
    public bool doesControlGoBackToPlayer;

    private bool isActivated = false;
    private bool hasActivated = false;
    private bool startPan;
    private float currentTime = 0;
    private float panTimer;

    // Use this for initialization
    void Start()
    {
        cFollow = Camera.main.GetComponent<CameraFollow>();
        panTimer = 0.0f;
        startPan = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated == true && hasActivated == false)
        {
            startPan = true;
            if (currentTime >= timeBeforeActivation)
            {
                objectWithAnimation.GetComponent<Animator>().SetTrigger("Trigger");
                hasActivated = true;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        if (startPan)
        {
            cFollow.followSpeed = 1.5f;
            cFollow.target = panTarget.transform;
            if (doesControlGoBackToPlayer)
            {
                FireScript fireScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>();
                fireScript.QuitFireball();
            }
            else
            {
                FireScript fireScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>();
                fireScript.enabled = false;
            }

            panTimer += Time.deltaTime;

            if (panTimer >= 4.0f)
            {
                if (doesControlGoBackToPlayer)
                {
                    cFollow.followSpeed = 1.5f;
                    cFollow.target = GameObject.FindGameObjectWithTag("Player").transform;
                    cFollow.followSpeed = 10;
                    startPan = false;
                }
                else
                {
                    cFollow.followSpeed = 1.5f;
                    cFollow.target = GameObject.FindGameObjectWithTag("Fireball").transform;
                    cFollow.followSpeed = 10;
                    startPan = false;
                    FireScript fireScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FireScript>();
                    fireScript.enabled = true;
                }
            }
        }
    }

    //Triggers animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            isActivated = true;
        }
    }

    //maybe add reset here if it is needed
}
