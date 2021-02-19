using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _rope;
    /*
    [SerializeField] AudioClip _clip0;
    [SerializeField] AudioClip _clip1;
    [SerializeField] AudioClip _clip2;
    [SerializeField] AudioClip _clip3;
    */
    [SerializeField] Material _colorProtector;
    [SerializeField] int _timeToSpeedUp;

    
    private float _animParamPlayer;
    private float _animParamRope;
    private float _p_animLength = 2.5f;
    private float _r_animLength = 0.5f;
    private float[] _ropeMultiplier;
    private int _spedUpCount = 0;
    private float _currentDifference;

    UIController _uiController;
    Animator _p_animator;
    Animator _r_animator;
    AudioSource _source;
    GameSetup _gameSetup;

    private void Awake()
    {
        //getting components
        _source = GetComponent<AudioSource>();
        _p_animator = _player.GetComponent<Animator>();
        _r_animator = _rope.GetComponent<Animator>();
        _gameSetup = GetComponent<GameSetup>();
        _uiController = FindObjectOfType<UIController>();

        // playing level music

    }
    void Start()
    {
        _ropeMultiplier = new float[] { 1f / 4f, 2f / 7f, 1f / 3f };

        //animation parameters
        _animParamPlayer = _p_animator.GetFloat("anim_speed_player");
        _animParamRope = _r_animator.GetFloat("anim_speed_rope");

        _currentDifference = ((_r_animLength / _animParamRope) - (_p_animLength / _animParamPlayer));

    }

    //speed up the rope maximum of three times
    //legacy
    IEnumerator SpeedUp()
    {
        Debug.Log("Started");
        yield return new WaitForSeconds(_timeToSpeedUp);
        _r_animator.SetFloat("anim_speed_rope", _ropeMultiplier[_spedUpCount]);
        _spedUpCount++;
        Debug.Log("Finished");
    }

    private IEnumerator DifficultySpeed()
    {
        yield return new WaitForSeconds(2);
        if (_uiController._globalDifficulty == 0)
        {
            _r_animator.SetFloat("anim_speed_rope", _animParamRope);
        } else if (_uiController._globalDifficulty == 1)
        {
            _r_animator.SetFloat("anim_speed_rope", _ropeMultiplier[2]);
        } else if (_uiController._globalDifficulty == 2)
        {
            _r_animator.SetFloat("anim_speed_rope", _animParamRope * 3);
        } else if (_uiController._globalDifficulty == 3)
        {
            _r_animator.SetFloat("anim_speed_rope", _animParamRope * 4);
        }
    }
}
