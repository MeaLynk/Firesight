using UnityEngine;

public class PyreScript : MonoBehaviour
{

    public bool isPyreOn = false;
    public GameObject pyreParticles;
    public AudioSource audioSource;
    public AudioClip lightingSound;
    public AudioClip fireBlazingSound;

    private GameObject gameWorld;

    // Use this for initialization
    void Start()
    {
        pyreParticles.SetActive(false);

        if (gameWorld == null)
        {
            gameWorld = GameObject.FindGameObjectWithTag("GameWorld");
        }
    }

    //Gets called to disable the checkpoint
    public void DisableCheckpoint()
    {
        isPyreOn = false;
        pyreParticles.SetActive(false);
    }

    //Player triggers checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isPyreOn == false)
        {
            isPyreOn = true;
            pyreParticles.SetActive(true);
            gameWorld.GetComponent<CheckpointManager>().ActivateCheckpoint(gameObject);
            audioSource.PlayOneShot(lightingSound);
            audioSource.clip = fireBlazingSound;
            audioSource.PlayDelayed(2.0f);
        }
    }
}
