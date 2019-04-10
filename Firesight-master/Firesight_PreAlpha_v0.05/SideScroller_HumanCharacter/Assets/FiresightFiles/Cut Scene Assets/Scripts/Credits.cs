using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject cutScene;
    public Camera creditsCamera;
    // Use this for initialization
    void Start()
    {
        cutScene.SetActive(false);
        creditsCamera.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
