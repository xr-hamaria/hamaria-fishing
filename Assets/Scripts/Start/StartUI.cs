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
    public String sceneName;

    private bool isOnTransition = false;
    private bool isRightHand;
    private Material material;
    private float transitionProgress = 0.0f;

    public void OnLeftHandButtonClicked()
    {
        cube.SetActive(true);
        isRightHand = false;
        StartTransition();
        StartCoroutine(LoadScene());
    }

    public void OnRightHandButtonClicked()
    {
        cube.SetActive(true);
        isRightHand = true;
        StartTransition();
        StartCoroutine(LoadScene());
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

    private IEnumerator LoadScene() {
        var async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(5);
        async.allowSceneActivation = true;
    }

}
