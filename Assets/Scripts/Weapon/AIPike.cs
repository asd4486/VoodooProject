using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPike : AIWeapon
{
    public void Attack(MonsterPart targetPart)
    {
        if (targetPart.isDead) return;

        targetPart.GetDamage(damage);
    }
}
