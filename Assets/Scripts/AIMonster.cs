using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    private float startSpeed;
    Animator myAnimator;

    //target point for enemy
    public Transform topTargetPoint;
    public Transform botTargetPoint;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        startSpeed = GameManager.Instance.environmentSpeed;
    }

    public void Init()
    {
        PlayAnimation("Walk");
        AudioManager.Instance.FMODEvent_Creature_Walk.start();
    }

    public void StartAnimation()
    {
        //GameManager.Instance.environmentSpeed = 0f;
    }

    public void FinishAnimation()
    {
        GameManager.Instance.environmentSpeed = startSpeed;
    }

    public void PlayAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }
}
