using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    [SerializeField] Animator myAnimator;

    //target point for enemy
    public Transform topTargetPoint;
    public Transform botTargetPoint;

    //private void Awake()
    //{
    //    myAnimator = GetComponentInChildren<Animator>();
    //}

    public void Init()
    {
        PlayAnimation("walk");
        AudioManager.Instance.FMODEvent_Creature_Walk.start();
    }

    public void PlayAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }
}
