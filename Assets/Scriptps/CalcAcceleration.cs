using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcAcceleration : MonoBehaviour
{
    private Vector3 prevPos;
    public Vector3 acceleration;

    void Start()
    {
        prevPos = this.transform.position;
    }

    void FixedUpdate()
    {
        acceleration = this.transform.position - prevPos;
        prevPos = this.transform.position;
    }
}
