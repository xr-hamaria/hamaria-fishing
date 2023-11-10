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
        Instantiate(grade, defaultPos.transform);
    }

    public void Success(string fishName)
    {
        Debug.Log("Instantiate");
        GameObject prefab = null;
        if(fishName == "単位") prefab = Instantiate(grade, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "再試験") prefab = Instantiate(reTest, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "秀") prefab = Instantiate(good, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "論理回路") prefab = Instantiate(IC, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "しずっぴー") prefab = Instantiate(sizuppi, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "さば") prefab = Instantiate(saba, this.transform.position, Quaternion.identity) as GameObject;
        if(fishName == "まぐろ") prefab = Instantiate(tuna, this.transform.position, Quaternion.identity) as GameObject;

        if (prefab != null)
        {
            Debug.Log("Instantiate");
            Rigidbody prefabRb = prefab.GetComponent<Rigidbody>();
            prefabRb.AddForce((-this.transform.position + defaultPos.transform.position)*100f);
            rb.AddForce((-this.transform.position + defaultPos.transform.position)*150f);
        }
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
        // grade.SetActive(false);
        // reTest.SetActive(false);
        // good.SetActive(false);
        // IC.SetActive(false);
        // sizuppi.SetActive(false);
        // saba.SetActive(false);
        // tuna.SetActive(false);
    }
}
