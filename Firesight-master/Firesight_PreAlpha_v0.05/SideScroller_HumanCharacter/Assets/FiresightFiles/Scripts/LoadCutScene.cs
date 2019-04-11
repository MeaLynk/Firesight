using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCutScene : MonoBehaviour
{
    public int activeSceneIndex;
    public Image fade;
    public bool startFade = false;
    public bool doneFading = false;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            if (fade.color.a < 1)
            {
                fade.color = (fade.color) + (new Color(0.0f, 0.0f, 0.0f, 0.5f) * Time.deltaTime);
            }
            else
            {
                startFade = false;
                doneFading = true;
            }
        }
        if (doneFading)
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            startFade = true;
            player.GetComponent<PlayerMove>().canPlayerMove = false;
            player.GetComponent<PlayerMove>().isPlayerInControl = false;
            player.GetComponent<FireScript>().enabled = false;
            GameObject.Find("Ozin").GetComponent<Animator>().Play("Run");
        }
    }
}
