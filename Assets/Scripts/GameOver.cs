using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class GameOver : MonoBehaviour
{
    public Material transition;

    private float _transitionProgress = 1.0f;
    private bool _FadeIn = false;
    private bool _FadeOut = false;

    // Start is called before the first frame update
    async void Start()
    {
        transition.SetFloat("_Alpha", 1.0f);
        _FadeIn = true;
        await UniTask.Delay(TimeSpan.FromSeconds(10.0f));
        _FadeOut = true;
    }

    // Update is called once per frame
    async void Update()
    {
        if(_FadeIn)
        {
            _transitionProgress -= Time.deltaTime / 1.5f;
            transition.SetFloat("_Alpha", _transitionProgress);
            if(_transitionProgress <= 0.0f)
            {
                _FadeIn = false;
            }
        }

        if(_FadeOut)
        {
            _transitionProgress += Time.deltaTime / 1.5f;
            transition.SetFloat("_Alpha", _transitionProgress);
            if(_transitionProgress >= 1.0f)
            {
                _FadeOut = false;
                SceneManager.LoadScene("Start");
            }
        }
    }
}
