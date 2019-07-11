using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Pike,
    Arrow,
}

public class AIWeapon : MonoBehaviour
{
    public float damage;
    public WeaponTypes weaponType;


}
