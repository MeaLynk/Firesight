using UnityEngine;

public class BreakGround : MonoBehaviour
{
    public GameObject[] pieces;
    [Range(1, 10)] public int maxFallSpeed;

    private float[] fallSpeeds;
    public bool isFalling = false;

    // Use this for initialization
    void Awake()
    {
        fallSpeeds = new float[pieces.Length];

        for (int i = 0; i < pieces.Length; i++)
        {
            fallSpeeds[i] = Random.Range(15, maxFallSpeed + 15);
        }
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].transform.position = pieces[i].transform.position + new Vector3(0.0f, -fallSpeeds[i], 0.0f) * Time.deltaTime;
                pieces[i].transform.Rotate((fallSpeeds[i] + 10) * Time.deltaTime, (fallSpeeds[i] + 10) * Time.deltaTime, (fallSpeeds[i] + 10) * Time.deltaTime);
            }
        }
    }
}