using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager
{
    private FishData _fishData;

    public FishData FishDataJson
    {
        get
        {
            return _fishData;
        }
    }
    
    //When this class be instanced, it load the json file and store the data.
    public JsonManager()
    {
        string jsonFilePath = "Assets/Resources/Fishes.json";
        string jsonString = System.IO.File.ReadAllText(jsonFilePath);
        _fishData = JsonUtility.FromJson<FishData>(jsonString);
    }
}
