using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    private bool _isFloatOnWater = false;
    private bool _isFishOnLod = false;
    private Fish _currentFish;
    private JsonManager _jsonManager;
    private float _fishingTimer = 0.0f;
    private float _reelRotationAmount = 0.0f;

    public bool isFloatOnWater
    {
        get
        {
            return _isFloatOnWater;
        }
        
        set
        {
            _isFloatOnWater = value;
        }
    }

    public bool isFishOnLod
    {
        get
        {
            return _isFishOnLod;
        }

        set
        {
            //Add Events Here
            _isFishOnLod = value;
        }
    }

    public float ReelRotationAmount
    {
        get
        {
            return _reelRotationAmount;
        }

        set
        {
            _reelRotationAmount = value;
        }
    }
    
    // Decide Current Fish in Start function.
    private void Start()
    {
        _jsonManager = new JsonManager();
        int randomNum = UnityEngine.Random.Range(0, _jsonManager.FishDataJson.fishData.Length);
        _currentFish = _jsonManager.FishDataJson.fishData[randomNum];
    }

    void Update()
    {
        //After 5 sec Fish get the float
        if (_isFloatOnWater　&& !_isFishOnLod)
        {
            _fishingTimer += Time.deltaTime;
            if (_fishingTimer > 5.0f)
            {
                isFishOnLod = true;
            }
        }
        else if(_fishingTimer != 0.0f)
        {
            _fishingTimer = 0.0f;
        }

        if (isFishOnLod)
        {
            
        }
        
    }
}
