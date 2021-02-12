using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreCounter : MonoBehaviour
{
    int h_score = 0;
    int m_score = 0;
    public int _finalScore = 0;
    bool _damage = false;

    public int curHealth = 0;
    public int maxHealth;
    public int _streak = 0;
    public int combo = 0;

    private int _tempStreak = 0;
    private int _tempFinalScore = 0;
    private bool _missed = false;

    [SerializeField] GameObject arrowContainer;
    [SerializeField] ParticleSystem _comboSystem;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip missSound;
    [SerializeField] AudioClip _comboSound;

    Animator anim;
    AudioSource source;
    ArrowScript a_controller;
    UIController uIController;

    private void Awake()
    {
        uIController = FindObjectOfType<UIController>();
        source = GetComponent<AudioSource>();
        a_controller = arrowContainer.GetComponent<ArrowScript>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        a_controller = arrowContainer.GetComponent<ArrowScript>();
        curHealth = maxHealth;
    }

    public void DamagePlayer(int damage)
    {
        curHealth -= damage;
        uIController.SetHealth(curHealth);

        _damage = true;
        AddNegativeScore();
        _damage = false;

    }
    public void AddPositiveScore()
    {
        _streak++;
        CheckForCombo();
        uIController.UpdateScore(_streak);

        StartCoroutine(a_controller.GreenArrow());
        
    }

    public void AddNegativeScore()
    {
        _missed = true;
        CheckForCombo();
        uIController.UpdateScore(0);

        if (!_damage)
        {
            source.clip = missSound;
            source.Play();
        } else if (_damage)
        {
            source.clip = hurtSound;
            source.Play();
        }

        StartCoroutine(a_controller.RedArrow());

    }

    void CheckForCombo()
    {
        if (_missed)
        {
            if (combo == 0)
            {
                _streak = 0;
                _missed = false;
            }
            else
            {
                _tempFinalScore = combo * _streak;
                _streak = 0;
                combo = 0;
                _missed = false;
            }
        } 
        else if (!_missed)
        {
            if (_streak % 5 == 0)
            {
                if (combo < 5)
                {
                    print("ADDED COMBO");
                    combo++;
                    source.clip = _comboSound;
                    source.Play();
                    _comboSystem.Play();
                }
            }
            _finalScore++;
        }
        _finalScore += _tempFinalScore;
        _tempFinalScore = 0;
    }
}
