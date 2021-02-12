using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    public GameObject arrow;
    public GameObject character;
    public GameObject rope;

    private AnimationClip clip;
    private GameObject clone;

    private float _animParamPlayer;
    private float _animParamRope;
    private float[] direction;
    public float rope_speed;
    public float rope_rotation;

    public int current_arrow;

    UIController _uiController;
    Animator animator;
    Animator _playerAnimator;
    Animator r_animator;
    PlayerScoreCounter counter;
    SpriteRenderer a_renderer;

    void Awake()
    {
        //UpdateAnimClipTimes();
    }
    void Start()
    {
        _playerAnimator = character.GetComponent<Animator>();
        _uiController = FindObjectOfType<UIController>();
        animator = character.GetComponent<Animator>();
        counter = character.GetComponent<PlayerScoreCounter>();
        r_animator = rope.GetComponent<Animator>();
        a_renderer = arrow.GetComponent<SpriteRenderer>();


        _animParamPlayer = _playerAnimator.GetFloat("anim_speed_player");
        _animParamRope = r_animator.GetFloat("anim_speed_rope");

        direction = new float[6] { 0f, 45f, 90f, 180f, 270f, 315f };
        UpdateAnimClipTimes();
        StartCoroutine("SwitchArrow");
    }

    void OnApplicationQuit()
    {
        a_renderer.color = Color.white;
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = r_animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Rope":
                    rope_rotation = clip.length;
                    break;
            }
        }
    }

    IEnumerator SwitchArrow()
    {
        DifficultySpeed();
        while (true)
        {
            rope_speed = rope_rotation / r_animator.GetFloat("anim_speed_rope");
            current_arrow = Random.Range(0, direction.Length);
            Destroy(clone);
            clone = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, direction[current_arrow]), transform);
            yield return new WaitForSeconds(rope_speed);
        }
    }

    private void DifficultySpeed()
    {
        if (_uiController._globalDifficulty == 0)
        {
            r_animator.SetFloat("anim_speed_rope", _animParamRope);
        }
        else if (_uiController._globalDifficulty == 1)
        {
            r_animator.SetFloat("anim_speed_rope", _animParamRope*(8f/7f));
        }
        else if (_uiController._globalDifficulty == 2)
        {
            r_animator.SetFloat("anim_speed_rope", _animParamRope * 2f);
            _playerAnimator.SetFloat("anim_speed_player", _animParamPlayer * (3f / 2f));
        }
        else if (_uiController._globalDifficulty == 3)
        {
            r_animator.SetFloat("anim_speed_rope", _animParamRope * 2.5f);
            _playerAnimator.SetFloat("anim_speed_player", _animParamPlayer * 2.25f);
        }
    }

    public IEnumerator GreenArrow()
    {
        Destroy(clone);
        clone = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, direction[current_arrow]), transform);
        clone.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
        yield return new WaitForEndOfFrame();

    }

    public IEnumerator RedArrow()
    {
        Destroy(clone);
        clone = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, direction[current_arrow]), transform);
        clone.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForEndOfFrame();
    }
}
