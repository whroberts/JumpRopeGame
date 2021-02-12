using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{

    public GameObject character;

    public float c_startSpeed = 2.0f;
    public float r_startSpeed = 0.25f;

    Animator c_animator;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        c_animator = character.GetComponent<Animator>();
        //animator.SetFloat("Speed",r_startSpeed);
        //c_animator.SetFloat("anim_speed",c_startSpeed);
    }


}
