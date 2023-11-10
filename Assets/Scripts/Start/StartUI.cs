using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    public GameObject transition;
    public GameObject cube;
    public String leftSceneName;
    public String rightSceneName;

    private bool isOnTransition = false;
    private bool isRightHand;
    private Material material;
    private float transitionProgress = 0.0f;

    public void OnLeftHandButtonClicked()
    {
        cube.SetActive(true);
        isRightHand = false;
        StartTransition();
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(LoadLeftScene());
    }

    public void OnRightHandButtonClicked()
    {
        cube.SetActive(true);
        isRightHand = true;
        StartTransition();
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(LoadRightScene());
    }

    private void StartTransition()
    {
        if (material.HasProperty("_Alpha"))
        {
            isOnTransition = true;
        }
    }

    void Start()
    {
        material = transition.GetComponent<Renderer>().sharedMaterial;
        material.SetFloat("_Alpha", 0.0f);
        cube.SetActive(false);
    }

    private void OnDestroy()
    {
        if (material != null) {
            Destroy(material);
        }
    }

    void Update()
    {
        if(isOnTransition && transitionProgress < 3.0f)
        {
            transitionProgress += Time.deltaTime;
            material.SetFloat("_Alpha", transitionProgress/3.0f);
        }
    }

    private IEnumerator LoadLeftScene() {
        var async = SceneManager.LoadSceneAsync(leftSceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(5);
        async.allowSceneActivation = true;
    }

    private IEnumerator LoadRightScene() {
        var async = SceneManager.LoadSceneAsync(rightSceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(5);
        async.allowSceneActivation = true;
    }

}
