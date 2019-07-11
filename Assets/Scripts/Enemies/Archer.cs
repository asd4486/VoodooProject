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

        GameObject p = Instantiate(projectile, projectileSpawner.position, Quaternion.identity);
        p.GetComponent<AIArrow>().Init(attackTargetPart);
    }
}
