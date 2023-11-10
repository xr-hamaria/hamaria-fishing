using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class FishingManager : MonoBehaviour
{
    public GameObject wheel;
    public GameObject reelMeter;
    public GameObject interestLevel;
    public GameObject canvas;
    public GameObject knobCollider;
    public GameObject fFloat;
    public GameObject xRInteractionManager;
    public GameObject xRInteractor;

    private bool _isFloatOnWater = false;
    private bool _isFishOnLod = false;
    private bool _reduceGaugeFlag = false;

    private XRDirectInteractor _xrDirectInteractor;
    private XRInteractionManager _xrInteractionManager;
    private Fish _currentFish;
    private JsonManager _jsonManager;
    private XRKnob _xrKnob;
    private RectTransform _reelMeterTransform;
    private RectTransform _interestLevelTransform;
    private Animator _UiAnimator;
    private TriggerFloat _triggerFloat;
    private AudioSource _myAudioSource;
    
    private float _fishingTimer = 0.0f;
    private float _reelRotationAmount = 0.0f;
    private float _fishInterest = 0.0f;
    private float _failTime = 0.0f;
    private float _difficulty;
    
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

    public float FishInterest
    {
        get
        {
            return _fishInterest;
        }

        set
        {
            if(value < 0.0f)
            {
                value = 0.0f;
            }

            _fishInterest = value;
        }
    }

    // Decide Current Fish in Start function.
    private void Start()
    {
        _xrDirectInteractor = xRInteractor.GetComponent<XRDirectInteractor>();
        _xrInteractionManager = xRInteractionManager.GetComponent<XRInteractionManager>();
        _myAudioSource = this.GetComponent<AudioSource>();
        _jsonManager = new JsonManager();
        _xrKnob = wheel.GetComponent<XRKnob>();
        _reelMeterTransform = reelMeter.GetComponent<RectTransform>();
        _interestLevelTransform  = interestLevel.GetComponent<RectTransform>();
        _UiAnimator = canvas.GetComponent<Animator>();
        _triggerFloat = fFloat.GetComponent<TriggerFloat>();

        ChooseRandomFish();
        TestLog();
    }

    private void TestLog()
    {
        Debug.Log(_currentFish.name);
    }

    void Update()
    {
        if(_reduceGaugeFlag)
        {
            _fishInterest -= 0.01f;
            _reelRotationAmount -= 0.01f;

            if (_fishInterest <= 0.0f) { _fishInterest = 0.0f; }
            if (_reelRotationAmount <= 0.0f) { _reelRotationAmount = 0.0f; }
            if (_fishInterest <= 0.0f && _reelRotationAmount <= 0.0f)
            {
                _fishInterest = 0.0f;
                _reelRotationAmount = 0.0f;
                _reduceGaugeFlag = false;
            }
        }

        WaitForFishBite();
        UpdateReelRotationAmount();
        UpdateReelMeter();
        UpdateInterestLevel();
        ReelInFish();
    }

    private void UpdateReelRotationAmount()
    {
        if(_xrKnob.value >= 0)
        {
            _reelRotationAmount = _xrKnob.value / 20.0f;
        }
    }

    private void UpdateReelMeter()
    {
        if(_reelRotationAmount > 1.0f)
        {
            _reelMeterTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            _reelMeterTransform.localScale = new Vector3(1.0f, _reelRotationAmount, 1.0f);
        }
    }

    private void UpdateInterestLevel()
    {
        if(_fishInterest > 1.0f)
        {
            _interestLevelTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if(_fishInterest < 0.0f)
        {
            _interestLevelTransform.localScale = new Vector3(1.0f, 0.0f, 1.0f);
        }
        else
        {
            _interestLevelTransform.localScale = new Vector3(1.0f, _fishInterest, 1.0f);
        }
    }

    private void WaitForFishBite()
    {
        //After 5 sec Fish get the float
        if (_isFloatOnWater && !_isFishOnLod)
        {
            _fishingTimer += Time.deltaTime;
            if (_fishingTimer > 3.0f)
            {
                _UiAnimator.SetTrigger("Hit");
                ReduceGauges();
                _myAudioSource.Play();
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
            _fishInterest += _currentFish.difficulty * math.abs(Unity.Mathematics.noise.snoise(new float2(1.0f, Time.time))*0.0015f);

            if (_reelRotationAmount >= 1.0f)
            {
                Debug.Log("Success");
                Success();
            }
            else if(_fishInterest >= 1.0f)
            {
                Debug.Log("fail");
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
        ReduceGauges();
        ChooseRandomFish();
        
        //Events will be Here;
    }

    private void Success()
    {
        _triggerFloat.Success(_currentFish.name);
        ResetValuables();
        ReduceGauges();
        ChooseRandomFish();
        
        //Events will be Here
    }

    public void ReduceGauges()
    {
        _reduceGaugeFlag = true;
    }

    public void ResetValuables()
    {
        // knobCollider.SetActive(false);
        _xrInteractionManager.SelectExit(_xrDirectInteractor, _xrKnob);
        _isFloatOnWater = false;
        _isFishOnLod = false;
        _fishingTimer = 0.0f;
        _reelRotationAmount = 0.0f;
        _fishInterest = 0.0f;
        _failTime = 0.0f;
        _xrKnob.value = 0.0f;
        // knobCollider.SetActive(true);        
    }
}
