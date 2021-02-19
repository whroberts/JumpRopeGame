using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private GameObject _player;
    private GameObject _rope;
    private GameObject _arrowContainer;
    private GameObject _effectsContainer;
    private GameObject _gameController;
    [SerializeField] AudioController _audioController;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _gameCanvas;
    [SerializeField] AudioClip _deadMusic;

    bool _begin = true;
    bool _first = true;

    AudioSource _menuMusic;
    AudioSource _audioSource;
    AudioSource _audioSourcePlayer;
    UIController _uiController;
    GameObject _menuSound;
    GameController _gameControllerScript;

    private void Awake()
    {
        _player = GameObject.Find("jumpingNinja");
        _rope = GameObject.Find("JumpRope");
        _arrowContainer = GameObject.Find("arrowContainer");
        _effectsContainer = GameObject.Find("EffectsContainer");
        _gameController = GameObject.Find("GameController");
        _audioSource = _gameController.GetComponent<AudioSource>();
        _audioSourcePlayer = _player.GetComponent<AudioSource>();
        _gameControllerScript = GetComponent<GameController>();
        _uiController = FindObjectOfType<UIController>();
        //_menuSound = GameObject.Find("MenuSound");
        //_menuMusic = _menuSound.GetComponent<AudioSource>();

        Pause();
        StopAudio();
    }

    private void Start()
    {
        _uiController._difficultyChoice.onValueChanged.AddListener(delegate { _uiController.ValueChangeCheck(); });
    }

    private void Update() {

        SendToUpdate();

        if (Input.GetKeyDown("escape"))
        {
            if (_first)
            {
                _pauseMenu.SetActive(true);
                Pause();
                _first = false;
            } else if (!_first)
            {
                _pauseMenu.SetActive(false);
                Play();
                _first = true;
            }
        }
    }

    public void SendToUpdate()
    {
        _uiController.ValueChangeCheck();
        _audioController.SetSound();
        if (Input.GetKeyDown("p"))
        {
            StartCoroutine(_audioController.WaitForRestart());
            return;
        }
    }
    
    public void PlayAudio()
    {
        _audioSource.enabled = true;
        _audioSourcePlayer.enabled = true;
        _audioController.enabled = true;
        _audioSource.Play();
    }

    public void StopAudio()
    {
        _audioSource.enabled = false;
    }
    public void Play()
    {
        //_menuMusic.Pause();
        _audioSource.Play();
        _player.SetActive(true);
        _rope.SetActive(true);
        _arrowContainer.SetActive(true);
        _effectsContainer.SetActive(true);
        _gameControllerScript.enabled = true;
        _audioSourcePlayer.enabled = true;
        _audioController.enabled = true;
        _gameCanvas.SetActive(true);

    }
    public void Pause()
    {
        //_menuMusic.Play();
        _audioSource.Pause();
        _player.SetActive(false);
        _rope.SetActive(false);
        _arrowContainer.SetActive(false);
        _effectsContainer.SetActive(false);
        _gameControllerScript.enabled = false;
        _gameCanvas.SetActive(false);
    }

    public void Kill()
    {
        //_menuMusic.clip = _deadMusic;
        //_menuMusic.volume = 0.3f;
        //_menuMusic.Play();
        _audioSourcePlayer.enabled = false;
        _arrowContainer.SetActive(false);
        _effectsContainer.SetActive(false);
        _audioSource.Pause();
        _gameControllerScript.enabled = false;
        _audioController.enabled = false;
        _gameCanvas.SetActive(false);
    }
}