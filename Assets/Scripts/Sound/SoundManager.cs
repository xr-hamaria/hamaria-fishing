using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private AudioSource _myAudioSouece;
    private float _volume = 0.0f;
    private bool _isFadeIn = false;
    private bool _isFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        _myAudioSouece = this.GetComponent<AudioSource>();
        FadeIn();
    }

    private void FadeIn()
    {
        _isFadeIn = true;
    }

    private void FadeOut()
    {
        _isFadeOut = true;
    }

    private void UpdateVolume()
    {
        _myAudioSouece.volume = _volume;
    }

    void Update()
    {
        if(_isFadeIn)
        {
            _volume += 0.01f;
            if(_volume >= 1.0f)
            {
                _isFadeIn = false;
            }
        }

        if (_isFadeOut)
        {
            _volume -= 0.01f;
            if(_volume <= 1.0f)
            {
                _isFadeOut = false;
            }
        }
    }
}
