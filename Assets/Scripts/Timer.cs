using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public GameObject timerUI;
    public GameObject transition;
    public GameObject UICanvas;
    [SerializeField]
    public string sceneName;

    private float _timer = 0.0f;
    private const int timeLimit = 120;
    private float _timeLeft = 120.0f;
    private TMP_Text _text;
    private bool _sceneChangeFlag = false;
    private Material _transitionMaterial;
    private float _transitionProgress;
    private Animator _uiAnimator;

    void Start()
    {
        _timer = 0.0f;
        _uiAnimator = UICanvas.GetComponent<Animator>();
        _text = timerUI.GetComponent<TMP_Text>();
        _transitionMaterial = transition.transform.GetChild(0).gameObject.GetComponent<Renderer>().sharedMaterial;
        _transitionMaterial.SetFloat("_Alpha", 0.0f);
        transition.SetActive(false);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _timeLeft = timeLimit - _timer;
        UpdateUI();
        if(_timeLeft <= 0.0f && !_sceneChangeFlag)
        {
            StartTransition();
            StartCoroutine(LoadScene());

        }
        else if(_sceneChangeFlag)
        {
            _transitionProgress += Time.deltaTime;
            _transitionProgress = math.clamp(_transitionProgress/4.0f - 1.0f, 0, 1);
            _transitionMaterial.SetFloat("_Alpha", _transitionProgress);
        }
    }

    private void StartTransition()
    {
        transition.SetActive(true);
        _uiAnimator.SetTrigger("TimeUP");
    }

    private void UpdateUI()
    {
        int minute = (int) MathF.Floor(_timeLeft/60);
        int second = (int) _timeLeft - (60 * minute);
        _text.SetText($"{minute}:{second}");
    }

    private IEnumerator LoadScene() {
        var async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(5);
        async.allowSceneActivation = true;
    }

    private void OnDestroy()
    {
        if(_transitionMaterial != null)
        {
            Destroy(_transitionMaterial);
        }
    }
}
