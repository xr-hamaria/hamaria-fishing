using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class FishingManager : MonoBehaviour
{
    private bool _isFloatOnWater = false;
    private bool _isFishOnLod = false;
    
    private Fish _currentFish;
    private JsonManager _jsonManager;
    private XRKnob _xrKnob;
    
    private float _fishingTimer = 0.0f;
    private float _reelRotationAmount = 0.0f;
    private float _fishInterest = 0.0f;
    private float _failTime = 0.0f;
    
    public bool IsFloatOnWater
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

    public bool IsFishOnLod
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
        _xrKnob = GameObject.Find("Wheel").GetComponent<XRKnob>();
        ChooseRandomFish();
    }

    void Update()
    {
        WaitForFishBite();
        ReelInFish();
    }

    private void WaitForFishBite()
    {
        //After 5 sec Fish get the float
        if (_isFloatOnWater　&& !_isFishOnLod)
        {
            _fishingTimer += Time.deltaTime;
            if (_fishingTimer > 60.0f)
            {
                IsFishOnLod = true;
            }
        }
        else if(_fishingTimer != 0.0f)
        {
            _fishingTimer = 0.0f;
        }
    }
    
    //function name should be changed
    private void ReelInFish()
    {
        if (_isFishOnLod)
        {
            _fishInterest += _currentFish.interestLevel * UnityEngine.Random.Range(0.2f, 1.0f);
            
            // if Reel too much, fail count will be increased
            if (_reelRotationAmount > _fishInterest)
            {
                _failTime += Time.deltaTime;
            }

            // if fail count accumulate above difficulty, fishing will fail
            if (_failTime > _currentFish.difficulty)
            {
                Fail();
            }
            else if (_reelRotationAmount > _currentFish.reelAmount)
            {
                Success();
            }
        }
        else
        {
            //Reel too much Before a fish bite, Fishing will be Fail
            //Threshold should be changed
            if (_reelRotationAmount > 30f)
            {
                Fail();
            }
        }
    }

    private void ChooseRandomFish()
    {
        int randomNum = UnityEngine.Random.Range(0, _jsonManager.FishDataJson.fishData.Length);
        _currentFish = _jsonManager.FishDataJson.fishData[randomNum];
    }
    
    private void Fail()
    {
        ResetValuables();
        ChooseRandomFish();
        
        //Events will be Here;
    }

    private void Success()
    {
        ResetValuables();
        ChooseRandomFish();
        
        //Events will be Here
    }

    private void ResetValuables()
    {
        _isFloatOnWater = false;
        _isFishOnLod = false;
        _fishingTimer = 0.0f;
        _reelRotationAmount = 0.0f;
        _fishInterest = 0.0f;
        _failTime = 0.0f;        
    }
}
