using UnityEngine;

public class Fish
{
    //variables, their getter and setter
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        
        set
        {
            _name = value;
        }
    }
    
    private float _size;
    public float Size
    {
        get
        {
            return _size;
        }

        set
        {
            _size = value;
        }
    }

    private GameObject _model;
}
