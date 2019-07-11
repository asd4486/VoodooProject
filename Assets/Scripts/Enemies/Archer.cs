using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AIEnemy
{
    [SerializeField] Transform projectileSpawner;
    [SerializeField] GameObject projectile;

    public override void EventAttack()
    {
        base.EventAttack();
        AudioManager.Instance.FMODEvent_Ennemi_Attack_Arrow.start();
        List<MonsterPart> parts = playerController.middleParts;

        switch (spawnZone)
        {
            case EnemySpawnZones.Middle:
                foreach (var part in playerController.topParts) parts.Add(part);
                break;
            case EnemySpawnZones.Bottom:
                foreach (var part in playerController.downParts) parts.Add(part);
                break;
        }

        attackTargetPart = parts[Random.Range(0, parts.Count)];

        //Vector3 targetDir = attackTargetPart.transform.position - transform.position;
        GameObject p = Instantiate(projectile, projectileSpawner.position, Quaternion.identity);
        p.GetComponent<AIArrow>().Init(attackTargetPart);

        //p.transform.position = projectileSpawner.position;
        //float angle = Vector3.Angle(targetDir, p.transform.up);
        //p.transform.Rotate(0, 0, angle);

        //p.transform.DOMove(attackTargetPart.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));

        //attackTargetPart.GetDamage(damage, attackDelay, GameManager.Instance.projectileTravelTime);
    }
}
