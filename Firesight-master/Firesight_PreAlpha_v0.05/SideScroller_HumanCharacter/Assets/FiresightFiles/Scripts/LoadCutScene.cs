using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCutScene : MonoBehaviour
{
    public int activeSceneIndex;
    // Use this for initialization
    void Start()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
            SceneManager.UnloadSceneAsync(activeSceneIndex);
        }
    }
}
