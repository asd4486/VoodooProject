using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AIEnemy
{
    private bool canAttack = false;

    private float timerShoot = 0f;
    public float shootCooldown = 2f;

    public GameObject projectileSpawner;
    public GameObject projectile;

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

        transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * speedMultiplicator * Time.deltaTime);
    }

    public void Shoot()
    {
        Vector3 targetDir = attackTarget.transform.position - transform.position;
        GameObject p = Instantiate(projectile, projectileSpawner.transform);
        float angle = Vector3.Angle(targetDir, p.transform.up);
        p.transform.Rotate(0, 0, angle);

        p.transform.DOMove(attackTarget.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));

        attackTarget.GetDamage(damage, attackDelay, GameManager.Instance.projectileTravelTime);
    }
}