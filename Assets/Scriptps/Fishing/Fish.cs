[System.Serializable]
public class Fish
{
    public string name;
    public string model;
    public int maxSize;
    public int minSize;
    public int difficulty;
}

[System.Serializable]
public class FishData
{
    public Fish[] fishData;
}
