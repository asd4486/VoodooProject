using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AIEnemy
{
    AIPike myPike;

    private void Start()
    {
        myPike = GetComponentInChildren<AIPike>();
    }

    public override void StartAttack()
    {
        base.StartAttack();

        attackTargetPart = GetClosestPart();
    }

    public override void EventAttack()
    {
        base.EventAttack();

        myPike.Attack(attackTargetPart);
    }
    public override void Die()
    {
        base.Die();
        AudioManager.Instance.FMODEvent_Ennemi_BeingHit.start();
    }
}