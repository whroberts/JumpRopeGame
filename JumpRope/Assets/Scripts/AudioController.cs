using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] GameObject _gameController;
    [SerializeField] AudioClip _clip0;
    [SerializeField] AudioClip _clip1;
    [SerializeField] AudioClip _clip2;
    [SerializeField] AudioClip _clip3;
    [SerializeField] UIController _uiController;
    [SerializeField] GameObject _difficultyDropdownPanel;
    
    AudioSource _audioSource;
    GameController _gameControllerScript;
    GameSetup _gameSetup;
    

    private void Start()
    {
        _audioSource = _gameController.GetComponent<AudioSource>();
        _gameControllerScript = _gameController.GetComponent<GameController>();
        _gameSetup = _gameController.GetComponent<GameSetup>();

    }

    public void SetSound()
    {
        if (_uiController._globalDifficulty == 0)
        {
            _audioSource.clip = _clip0;
        }
        else if (_uiController._globalDifficulty == 1)
        {
            _audioSource.clip = _clip1;
        }
        else if (_uiController._globalDifficulty == 2)
        {
            _audioSource.clip = _clip2;
        }
        else if (_uiController._globalDifficulty == 3)
        {
            _audioSource.clip = _clip3;
        }
        else
        {
            print("Error, audio clip or difficulty was not available");
        }
    }
    public IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(1);
        _difficultyDropdownPanel.SetActive(false);
        //_audioSource.Play();
        _gameSetup.PlayAudio();
        _gameSetup.Play();
    }
}
