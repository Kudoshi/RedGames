using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimation : MonoBehaviour
{
    public string OFFRAIL_ANIM = "Train_Offrail";

    private Animator m_Animator;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        m_Animator.Play(animationName);
    }

    
}
