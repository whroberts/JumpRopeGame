using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject a_container;
    public GameObject uIContainer;

    private float animation_length;
    private string[] stored_clip;
    private float total_anim_length;
    private bool waiting = false;

    private bool frontFlip = false;
    private bool leftFrontFlip = false;
    private bool leftFlip = false;
    private bool backFlip = false;
    private bool rightFlip = false;
    private bool rightFrontFlip = false;

    GameObject character;
    Animator animator;
    ArrowScript s_arrow;
    PlayerScoreCounter score_counter;
    UIController ui_controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        s_arrow = a_container.GetComponent<ArrowScript>();
        score_counter = GetComponent<PlayerScoreCounter>();
        ui_controller = uIContainer.GetComponent<UIController>();

        stored_clip = new string[7];
    }

    void Update() {

        animator.SetBool("flipForward", false);
        animator.SetBool("flipBack", false);
        animator.SetBool("flipLeftForward", false);
        animator.SetBool("flipRightForward", false);
        animator.SetBool("flipRight", false);
        animator.SetBool("flipLeft", false);
        if (Input.GetKeyDown("w"))
        {
            animator.SetBool("flipForward", true);
            frontFlip = true;
            StartCoroutine("AddingScores");
        }

        else if (Input.GetKeyDown("q"))
        {
            animator.SetBool("flipLeftForward", true);
            leftFrontFlip = true;
            StartCoroutine("AddingScores");
        }

        else if (Input.GetKeyDown("a"))
        {
            animator.SetBool("flipLeft", true);
            leftFlip = true;
            StartCoroutine("AddingScores");
        }

        else if (Input.GetKeyDown("s"))
        {
            animator.SetBool("flipBack", true);
            backFlip = true;
            StartCoroutine("AddingScores");
        }

        else if (Input.GetKeyDown("d"))
        {
            animator.SetBool("flipRight", true);
            rightFlip = true;
            StartCoroutine("AddingScores");
        }

        else if (Input.GetKeyDown("e"))
        {
            animator.SetBool("flipRightForward", true);
            rightFrontFlip = true;
            StartCoroutine("AddingScores");

        }

        
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "FrontFlip":
                    animation_length = clip.length;
                    stored_clip[0] = clip.name;
                    break;
                case "LeftFrontFlip":
                    animation_length = clip.length;
                    stored_clip[1] = clip.name;
                    break;
                case "LeftFlip":
                    animation_length = clip.length;
                    stored_clip[2] = clip.name;
                    break;
                case "BackFlip":
                    animation_length = clip.length;
                    stored_clip[3] = clip.name;
                    break;
                case "RightFlip":
                    animation_length = clip.length;
                    stored_clip[4] = clip.name;
                    break;
                case "RightFrontFlip":
                    animation_length = clip.length;
                    stored_clip[5] = clip.name;
                    break;
            }
        }
    }

    IEnumerator AddingScores()
    {
        UpdateAnimClipTimes();
        while (!waiting)
        {
            total_anim_length = animation_length / animator.GetFloat("anim_speed_player");
            if ((s_arrow.current_arrow == 0) && frontFlip == true)
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[0]);
                frontFlip = false;
                
            } 
            
            else if ((s_arrow.current_arrow == 1) && leftFrontFlip == true)
            
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[1]);
                leftFrontFlip = false;
            } 

            else if ((s_arrow.current_arrow == 2) && leftFlip == true)
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[2]);
                leftFlip = false;
            }

            else if ((s_arrow.current_arrow == 3) && backFlip == true)
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[3]);
                backFlip = false;
            }

            else if ((s_arrow.current_arrow == 4) && rightFlip == true)
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[4]);
                rightFlip = false;
            }

            else if ((s_arrow.current_arrow == 5) && rightFrontFlip == true)
            {
                score_counter.AddPositiveScore();
                //print(stored_clip[5]);
                rightFrontFlip = false;
            }

            else
            {
                score_counter.AddNegativeScore();
            }


            waiting = true;
            frontFlip = false;
            leftFrontFlip = false;
            leftFlip = false;
            backFlip = false;
            rightFlip = false;
            rightFrontFlip = false;
        }
        
        yield return new WaitForSeconds(total_anim_length);
        waiting = false;

    }
}
