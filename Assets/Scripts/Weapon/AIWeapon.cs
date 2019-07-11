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
    public int hitChance = 2;
    public float damage;
    public float damageOverTime = 1;
    public WeaponTypes weaponType;

    //for arrow
    public float moveSpeed;
    [HideInInspector] public SpriteRenderer mySpriteRendrer;
    public Sprite spriteHited;
    [HideInInspector] public bool isHited;

    private void Awake()
    {
        mySpriteRendrer = GetComponent<SpriteRenderer>();
    }
}
