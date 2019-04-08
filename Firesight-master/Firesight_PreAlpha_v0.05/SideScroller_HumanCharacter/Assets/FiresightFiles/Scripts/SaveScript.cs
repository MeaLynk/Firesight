using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveScript : MonoBehaviour {

    //Save help from: https://www.youtube.com/watch?v=6JpNwnkaUx0

    private SaveClass currentSave;
    private string fileName = "Firesight_Save.json";
    private string filePath;

    private static SaveClass _instance;
    public SaveClass Instance
    {
        get { return _instance; }
        //set { _instance = value; }
    }

    public void AddDeath() { currentSave.numOfDeaths++; }
    public int GetNumOfDeaths() { return currentSave.numOfDeaths; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(_instance == null)
        {
            _instance = new SaveClass();
        }

        if (currentSave == null)
        {
            currentSave = new SaveClass();
        }

        filePath = Path.Combine(Application.dataPath, fileName);

        Debug.Log("Save file at: " + filePath);
    }

    // Use this for initialization
    void Start ()
    {
        LoadGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //When player quits game
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //Saves the game
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(currentSave);

        if(!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        File.WriteAllText(filePath, json);

        Debug.Log("Game Saved at: " + filePath);
    }

    //Loads the game
    public void LoadGame()
    {
        string json;

        if(File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            currentSave = JsonUtility.FromJson<SaveClass>(json);
            Debug.Log("Game Loaded at: "+ filePath);
        }
        else
        {
            Debug.Log("File is missing: " + filePath);
        }
    }

    public void DeleteSave()
    {
        currentSave = new SaveClass();
        SaveGame();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 250, 50, 30), currentSave.numOfDeaths.ToString());
    }
}
