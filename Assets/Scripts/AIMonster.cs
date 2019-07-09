using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    private float startSpeed;
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        startSpeed = GameManager.Instance.environmentSpeed;
    }

    private void Start()
    {
        PlayAnimation("Walk");
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
