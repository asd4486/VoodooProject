using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Villager,
    Archer,
    Warrior
}

public class AIEnemy : MonoBehaviour
{
    public EnemyTypes enemyType;
    [HideInInspector] public Animator myAnimator;

    Transform targetMonster;

    [HideInInspector] public Part attackTarget;
    public float attackDistance;

    public float damage;
    public float attackDelay = 1;

    [HideInInspector] public bool dead;

    public Vector3 projectileSpawnPos;
    public GameObject projectile;

    public float speedMultiplicator = 8f;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (dead)
        {
            Destroy(gameObject);
            return;
        }
    }

    void GetDistanceToTarget()
    {
        if (attackTarget == null) return;

        var dist = Mathf.Abs(transform.position)
    }

    public virtual void Attack()
    {
    }

    public virtual void Die()
    {
        dead = true;
    }

    public void SetTarget(Part[] parts)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(0, 0.3f), transform.position.z);
        GetComponent<AIEnemy>().attackTarget = parts[Random.Range(0, parts.Length)];
    }
}
