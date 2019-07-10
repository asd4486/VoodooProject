using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AIEnemy
{
    [SerializeField] Transform projectileSpawner;
    [SerializeField] GameObject projectile;

    protected override void Update()
    {
        base.Update();

        //timerShoot += Time.deltaTime;
        //if (timerShoot > shootCooldown)
        //{
        //    timerShoot = 0f;
        //    GameObject p = Instantiate(projectile, projectileSpawner.transform);

        //    p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

        //    target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
        //}
    }

    public override void EventAttack()
    {
        base.EventAttack();

        //get random attack part
        attackPart = playerController.allParts[Random.Range(0, playerController.allParts.Length)];

        Vector3 targetDir = attackPart.transform.position - transform.position;
        GameObject p = Instantiate(projectile);
        p.transform.position = projectileSpawner.position;
        float angle = Vector3.Angle(targetDir, p.transform.up);
        p.transform.Rotate(0, 0, angle);

        p.transform.DOMove(attackPart.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));

        attackPart.GetDamage(damage, attackDelay, GameManager.Instance.projectileTravelTime);
    }
}
