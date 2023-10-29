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

    // Start is called before the first frame update
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
        Debug.Log(targetAcceleration.Acceleration);
        rb.AddForce(targetAcceleration.Acceleration * 1000.0f, ForceMode.Acceleration);
        // rb.AddForce(new Vector3(1000.0f, 0.0f, 0.0f), ForceMode.Acceleration);
    }

    public void Reset() {
        isTriggered = false;
        rb.isKinematic = true;
    }
}
