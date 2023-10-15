using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishDataStruct
{
    public string name;
    public float maxSize;
    public float minSize;
    public int difficulty;
}

public class JsonManager
{
    private FishDataStruct _fishData;

    public FishDataStruct FishData
    {
        get
        {
            return _fishData;
        }
    }
    
    //When this class be instanced, it load the json file and store the data.
    public JsonManager()
    {
        string jsonFilePath = "Assets/Resources/fishData.json";
        string jsonString = System.IO.File.ReadAllText(jsonFilePath);
        _fishData = JsonUtility.FromJson<FishDataStruct>(jsonString);
    }
}
