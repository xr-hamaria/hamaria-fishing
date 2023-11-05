using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerFloat : MonoBehaviour
{
    private Rigidbody rb;
    private bool isTriggered = false;
    public GameObject defaultPos;
    private CalcAcceleration targetAcceleration;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        targetAcceleration = defaultPos.GetComponent<CalcAcceleration>();
        this.gameObject.SetActive(false);
    }

    public void OnTriggered() {

        isTriggered = true;
        this.transform.position = defaultPos.transform.position;
        rb.isKinematic = false;
        Debug.Log(targetAcceleration.acceleration);
        rb.AddForce(targetAcceleration.acceleration * 1000.0f, ForceMode.Acceleration);
    }

    public void Reset() {
        isTriggered = false;
        rb.isKinematic = true;
        this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);  
    }
}
