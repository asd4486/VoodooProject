using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AIEnemy
{
    //protected override void Update()
    //{
    //    base.Update();

    //    //if (canAttack)
    //    //{
    //    //    timerShoot += Time.deltaTime;
    //    //    if (timerShoot > shootCooldown)
    //    //    {
    //    //        timerShoot = 0f;
    //    //        GameObject p = Instantiate(projectile, projectileSpawner.transform);

    //    //        p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

    //    //        target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
    //    //    }
    //    //}

    //}

    public override void StartAttack()
    {
        base.StartAttack();

        attackTargetPart = GetClosestPart();
    }

    public override void EventAttack()
    {
        base.EventAttack();

        //attackTargetPart.GetDamage(damage, attackDelay, GameManager.Instance.projectileTravelTime);
    }
}