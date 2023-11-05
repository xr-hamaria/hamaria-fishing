using System;
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

    public GameObject grade;
    public GameObject reTest;
    public GameObject good;
    public GameObject IC;
    public GameObject sizuppi;
    public GameObject tuna;
    public GameObject saba;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        targetAcceleration = defaultPos.GetComponent<CalcAcceleration>();
        this.gameObject.SetActive(false);
    }

    public void Success(string fishName)
    {
        if(fishName == "単位") grade.SetActive(true);
        if(fishName == "再試験") reTest.SetActive(true);
        if(fishName == "秀") good.SetActive(true);
        if(fishName == "論理回路") IC.SetActive(true);
        if(fishName == "しずっぴー") sizuppi.SetActive(true);
        if(fishName == "さば") saba.SetActive(true);
        if(fishName == "まぐろ") tuna.SetActive(true);

        rb.AddForce((-this.transform.position + defaultPos.transform.position)*300f);
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
        grade.SetActive(false);
        reTest.SetActive(false);
        good.SetActive(false);
        IC.SetActive(false);
        sizuppi.SetActive(false);
        saba.SetActive(false);
        tuna.SetActive(false);
    }
}
