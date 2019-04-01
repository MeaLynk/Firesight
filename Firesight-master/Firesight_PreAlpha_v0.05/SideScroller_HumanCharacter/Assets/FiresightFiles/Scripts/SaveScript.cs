using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {

    //Add saving and loading later https://unity3d.com/learn/tutorials/topics/scripting/introduction-saving-and-loading

    private int numOfDeaths = 0;

    public void AddDeath() { numOfDeaths++; }
    public int GetNumOfDeaths() { return numOfDeaths; }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //When player quits game
    private void OnApplicationQuit()
    {
        
    }
}
