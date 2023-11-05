using System;
using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Float : MonoBehaviour
{
    public GameObject waterCollider;
    private FishingManager _fishingManager;

    // Start is called before the first frame update
    void Start()
    {
        _fishingManager = GameObject.Find("FishingManager").GetOrAddComponent<FishingManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == waterCollider)
        {
            Debug.Log("Collide!");
            _fishingManager.IsFloatOnWater = true;
        }
    }

}
