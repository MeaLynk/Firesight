using UnityEngine;

public class FireScript : MonoBehaviour
{
    [Header("SCENE NEEDS \"GameWorld\" PREFAB")]
    [Header("IN ORDER TO WORK.")]
    public GameObject fireObject;
    public GameObject playableFirePrefab;
    public GameObject camera;
    public GameObject player;
    public GameObject cameraTarget;
    public AudioSource SFXPlayer;
    public float maxFireVelocity = 20f;
    public float fireBurnOutTimerLength = 15.0f;
    public bool showDebugUI = false;
    public Vector3 firePos = new Vector3(0, 2, 0);
    private bool isPlayerInControl;
    private float fireBurnOutTimer;
    private GameObject currentPlayableFirePrefab;
    private Vector3 fireDirection;

    public bool GetIsPlayerInControl() { return isPlayerInControl; }

    // Use this for initialization
    void Start()
    {
        fireBurnOutTimer = 0.0f;
        isPlayerInControl = true;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (cameraTarget == null)
        {
            cameraTarget = player;
        }

        camera.GetComponent<CameraFollow>().target = cameraTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInControl == true)
        {
            //Centers fireball during rotation
            fireObject.GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position + firePos;

            //Player activates fireball
            if (Input.GetButtonDown("Fireball") && gameObject.GetComponent<PlayerMove>().grounded == true && GameObject.Find("GameWorld").GetComponent<PauseGame>().GetPausedState() == false)
            {
                //Stops the player and player animations
                isPlayerInControl = false;

                gameObject.GetComponent<PlayerMove>().isPlayerInControl = false;
                fireObject.SetActive(false);

                //Creates the playable fireball prefab
                currentPlayableFirePrefab = Instantiate(playableFirePrefab, gameObject.GetComponent<Transform>().position + firePos, Quaternion.identity);

                Physics.IgnoreCollision(currentPlayableFirePrefab.GetComponent<Collider>(), GetComponent<Collider>());

                camera.GetComponent<CameraFollow>().isPlayerInUse = false;
                camera.GetComponent<CameraFollow>().target = currentPlayableFirePrefab.transform;

                fireBurnOutTimer = 0.0f;
            }
        }
        else
        {
            ControlFireball();

            //Player quits fireball
            if (Input.GetButtonDown("Fireball"))
            {
                QuitFireball();
            }
        }
    }

    //Lets player control fireball
    private void ControlFireball()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target == currentPlayableFirePrefab.transform)
        {
            fireBurnOutTimer += Time.deltaTime;
        }

        //-------------------------------------------------------------------------------------------------------------
        //NEW CODE

        fireDirection = new Vector3(0, 0, 0);
        Vector3 newVelocity = currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity;

        //Adjusting the Horizontal movement
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            fireDirection = new Vector3(horizontal * 20, fireDirection.y);

            newVelocity.x += (fireDirection.x * Time.deltaTime);

            //If fireball is over max velocity
            if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.x >= maxFireVelocity)
            {
                newVelocity.x = maxFireVelocity;
            }
            else if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.x <= -maxFireVelocity)
            {
                newVelocity.x = -maxFireVelocity;
            }
        }
        else
        {
            fireDirection.x = 0;

            //Checks if the velocity should slow down to zero or stay 0 for X
            if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.x >= 0.1f)
            {
                fireDirection.x = 20;
            }
            else if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.x <= -0.1f)
            {
                fireDirection.x = -20;
            }
            else
            {
                fireDirection.x = 0;
            }
            newVelocity.x -= (fireDirection.x * Time.deltaTime);
        }

        //Adjusting the Vertical movement
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            fireDirection = new Vector3(fireDirection.x, vertical * 20);

            newVelocity.y += (fireDirection.y * Time.deltaTime);

            //If fireball is over max velocity
            if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.y >= maxFireVelocity)
            {
                newVelocity.y = maxFireVelocity;
            }
            else if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.y <= -maxFireVelocity)
            {
                newVelocity.y = -maxFireVelocity;
            }
        }
        else
        {
            fireDirection.y = 0;

            //Checks if the velocity should slow down to zero or stay 0 for X
            if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.y >= 0.1f)
            {
                fireDirection.y = 20;
            }
            else if (currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.y <= -0.1f)
            {
                fireDirection.y = -20;
            }
            else
            {
                fireDirection.y = 0;
            }
            newVelocity.y -= (fireDirection.y * Time.deltaTime);
        }

        currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity = newVelocity;

        //Checks if the player has more time to control the fireball
        if (fireBurnOutTimer >= fireBurnOutTimerLength)
        {
            QuitFireball();
        }


    }

    //Gets called to quit the fireball gameplay
    public void QuitFireball()
    {
        isPlayerInControl = true;

        gameObject.GetComponent<PlayerMove>().isPlayerInControl = true;

        fireObject.SetActive(true);
        Destroy(currentPlayableFirePrefab);

        camera.GetComponent<CameraFollow>().isPlayerInUse = true;
        camera.GetComponent<CameraFollow>().target = cameraTarget.transform;

        fireBurnOutTimer = 0.0f;
    }

    //Debug GUI
    private void OnGUI()
    {
        if (currentPlayableFirePrefab != null && showDebugUI == true)
        {
            GUI.Box(new Rect(10, 300, 120, 40), "Fireball velocity: \n" + currentPlayableFirePrefab.GetComponent<Rigidbody>().velocity.ToString());
            GUI.Box(new Rect(10, 350, 120, 40), "Burn Out Timer: \n" + ((int)fireBurnOutTimerLength - (int)fireBurnOutTimer).ToString());
        }
    }
}