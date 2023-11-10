using Cysharp.Threading.Tasks;
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
    public Material transitionMaterial;
    [SerializeField]
    public string sceneName;
    public int timeLimit = 120;

    private float _timer = 0.0f;
    // private const int timeLimit = 120;
    private float _timeLeft = 120.0f;
    private TMP_Text _text;
    private bool _sceneChangeFlag = false;
    private float _transitionProgress = 0.0f;
    private Animator _uiAnimator;
    private AudioSource _myAudioSource;
    private bool _loadFlag;

    void Start()
    {
        _loadFlag = false;
        _timer = 0.0f;
        _uiAnimator = UICanvas.GetComponent<Animator>();
        _text = timerUI.GetComponent<TMP_Text>();
        _myAudioSource = this.GetComponent<AudioSource>();
        // transitionMaterial = transition.transform.GetChild(0).gameObject.GetComponent<Renderer>().sharedMaterial;
        transitionMaterial.SetFloat("_Alpha", 0.0f);
        transition.SetActive(false);

        _myAudioSource.Play();

    }

    async void Update()
    {
        if(_timeLeft > 0.0f)
        {
            _timer += Time.deltaTime;
            _timeLeft = timeLimit - _timer;
            UpdateUI();
        }
        else if(_timeLeft <= 0.0f && _sceneChangeFlag == false)
        {
            _myAudioSource.Play();
            StartTransition();
            _sceneChangeFlag = true;
            // StartCoroutine(LoadScene());
        }
        else if(_sceneChangeFlag)
        {
            // Debug.Log("SceneChange");
            _text.SetText($"0:0");
            _transitionProgress += Time.deltaTime/3.0f;
            // _transitionProgress = math.clamp(_transitionProgress/5.0f, 0, 1);
            transitionMaterial.SetFloat("_Alpha", _transitionProgress);
            if(_transitionProgress >= 1.0f)
            {
                StartCoroutine(LoadScene());
            }
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
        yield return new WaitForSeconds(0.1f);
        async.allowSceneActivation = true;
    }

    private void OnDestroy()
    {
        if(transitionMaterial != null)
        {
            Destroy(transitionMaterial);
        }
    }
}
