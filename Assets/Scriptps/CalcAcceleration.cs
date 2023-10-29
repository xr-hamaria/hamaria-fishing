using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcAcceleration : MonoBehaviour
{
    private Vector3 prevPos;
    public Vector3 Acceleration;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Acceleration = this.transform.position - prevPos;
        prevPos = this.transform.position;
        // Debug.Log(Acceleration);
    }
}
