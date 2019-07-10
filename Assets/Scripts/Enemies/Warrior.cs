﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AIEnemy
{
    public void StopAnimation()
    {
        speedMultiplicator = 0f;
        //canAttack = true;
        myAnimator.SetTrigger("StopWalk");
    }

    protected override void Update()
    {
        base.Update();

        //if (canAttack)
        //{
        //    timerShoot += Time.deltaTime;
        //    if (timerShoot > shootCooldown)
        //    {
        //        timerShoot = 0f;
        //        GameObject p = Instantiate(projectile, projectileSpawner.transform);

        //        p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

        //        target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
        //    }
        //}

    }

    public override void Moving()
    {
        rb.velocity = Vector3.right * GameManager.Instance.environmentSpeed * speedMultiplicator;
    }

    public override void StartAttack()
    {
        rb.velocity = Vector3.zero;
        //Vector3 targetDir = attackPart.transform.position - transform.position;
        //GameObject p = Instantiate(projectile);
        //p.transform.position = projectileSpawner.position;
        //float angle = Vector3.Angle(targetDir, p.transform.up);
        //p.transform.Rotate(0, 0, angle);

        //p.transform.DOMove(attackPart.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));

        //attackPart.GetDamage(damage, attackDelay, GameManager.Instance.projectileTravelTime);
    }
}