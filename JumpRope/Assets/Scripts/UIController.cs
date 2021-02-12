using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    //[SerializeField] TextMesh _scoreText = null;
    //[SerializeField] TextMesh _comboText = null;
    //[SerializeField] TextMesh _totalScore = null;

    [SerializeField] TMP_Text _streakText = null;
    [SerializeField] TMP_Text _comboText = null;
    [SerializeField] TMP_Text _totalScore = null;

    [SerializeField] TMP_Text _fpsText;
    [SerializeField] Button _continueButton;
    private float deltaTime;
    public TMP_Dropdown _difficultyChoice;
    [SerializeField] Text totalHealthUI = null;
    public Slider healthBar;
    public GameObject character;

    public int _globalDifficulty = 0;
    private int score = 0;

    [SerializeField] GameController _gameController;
    PlayerScoreCounter _playerScoreCounter;
    GameSetup _gameSetup;

    void Start()
    {
        //_gameController = FindObjectOfType<GameController>();
        _playerScoreCounter = character.GetComponent<PlayerScoreCounter>();
        _gameSetup = FindObjectOfType<GameSetup>();
        totalHealthUI.text = _playerScoreCounter.maxHealth.ToString();
        healthBar.maxValue = _playerScoreCounter.maxHealth;
        healthBar.value = _playerScoreCounter.maxHealth;

        _difficultyChoice.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fpsText.text = "FPS = " + Mathf.Ceil(fps).ToString();
    }

    void OnApplicationQuit()
    {
        healthBar.maxValue = _playerScoreCounter.maxHealth;
    }

    public void UpdateHitScore(int hit)
    {
        //hitScoreTextUI.text = hit.ToString();
        _streakText.text = hit.ToString();
        _comboText.text = "x" + _playerScoreCounter.combo.ToString();
    }

    public void UpdateMissScore(int miss)
    {
        //missScoreTextUI.text = miss.ToString();
        _comboText.text = "x" + _playerScoreCounter.combo.ToString();
    }

    public void UpdateScore(int point)
    {
        score += point;

        _streakText.text = point.ToString();
        _comboText.text = "x" + _playerScoreCounter.combo.ToString();
        _totalScore.text = _playerScoreCounter._finalScore.ToString();
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        totalHealthUI.text = hp.ToString();
        _comboText.text = "x" + _playerScoreCounter.combo.ToString();
    }

    public void ValueChangeCheck()
    {
        _globalDifficulty = _difficultyChoice.value;
    }

    public void Continue()
    {
        _gameSetup.Play();
    }
}
