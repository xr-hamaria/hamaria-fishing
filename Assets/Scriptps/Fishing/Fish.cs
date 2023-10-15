[System.Serializable]
public class Fish
{
    public string name;
    public string model;
    public float maxSize;
    public float minSize;
    public int difficulty;
    public float interestLevel;
    public float reelAmount;
}

[System.Serializable]
public class FishData
{
    public Fish[] fishData;
}
