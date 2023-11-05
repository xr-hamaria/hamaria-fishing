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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == waterCollider)
        {
            Debug.Log("Collide!");
            _fishingManager.IsFloatOnWater = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == waterCollider)
        {
            Debug.Log("DeCollide!");
            _fishingManager.IsFloatOnWater = false;
        }
    }
}
