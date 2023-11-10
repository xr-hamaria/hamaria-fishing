using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WheelSound : MonoBehaviour
{
    private AudioSource _myAudioSource;
    private XRKnob _xrKnob;

    void Start()
    {
        _myAudioSource = this.GetComponent<AudioSource>();
        _xrKnob = this.GetComponent<XRKnob>();
    }

    public void Play()
    {
        if(!_myAudioSource.isPlaying && _xrKnob.isGrabbed)
        {
            _myAudioSource.Play();
        }
    }
}
