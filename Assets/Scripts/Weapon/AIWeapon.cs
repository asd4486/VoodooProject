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
    
    [SerializeField] protected int hitChance = 2;
    [SerializeField] protected float damage;
    [SerializeField] protected float damageOverTime = 1;
    [SerializeField] protected WeaponTypes weaponType;

    //for arrow
    [SerializeField] protected float moveSpeed;

    protected Sprite spriteDefault;
    [SerializeField] protected Sprite spriteHited;

    protected bool isHited;

    protected SpriteRenderer mySpriteRendrer;
    protected Collider2D myCol;

    string projectileSortingLayerName = "projectiles";

    private void Awake()
    {
        mySpriteRendrer = GetComponent<SpriteRenderer>();
        spriteDefault = mySpriteRendrer.sprite;

        myCol = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        //auto remove weapon if out of screen
        if (transform.position.x < -5 || transform.position.x > 10 || transform.position.y < -2 || transform.position.y > 7)
        {
            Destroy(gameObject);
            return;
        }
    }

    //for remove projectile
    public virtual void Out()
    {
        //reset weapon infos
        transform.SetParent(null);
        mySpriteRendrer.sprite = spriteDefault;
        mySpriteRendrer.sortingLayerName = projectileSortingLayerName;

        //invert direction of arrow
        transform.rotation = Quaternion.Euler(0f, 0f, -transform.eulerAngles.z);
        myCol.enabled = false;

        moveSpeed = moveSpeed * 5;
        isHited = false;
    }
}
