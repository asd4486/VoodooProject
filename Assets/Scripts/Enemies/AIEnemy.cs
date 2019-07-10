﻿using UnityEngine;

public enum EnemyTypes
{
    Villager,
    Archer,
    Warrior
}

public enum EnemyStatus
{
    Run,
    Attack,
    Die,
    Idle
}

public enum EnemySpawnZone
{
    Bottom,
    Middle
}

public class AIEnemy : MonoBehaviour
{
    AIMonster aiMonster;
    public EnemyTypes enemyType;
    [HideInInspector] public EnemyStatus myStatus;
    [HideInInspector] public EnemySpawnZone spawnZone;

    //0 bot
    //1 top
    [HideInInspector] public int targetZoneLine;

    [HideInInspector] public Animator myAnimator;
    [HideInInspector] public Rigidbody2D rb;


    //for check distance to monster
    Transform moveTargetPoint;

    [HideInInspector] public Part attackPart;
    public float atkRange;

    public float damage;
    [HideInInspector] public float attackDelayTimer;
    public float attackDelay = 1;

    public float speedMultiplicator = 8f;

    private void Awake()
    {
        aiMonster = FindObjectOfType<AIMonster>();
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(EnemySpawnZone zone)
    {
        spawnZone = zone;
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-0.1f, 0.1f));

        //two lines for one zone
        targetZoneLine = Random.Range(0, 2);
        moveTargetPoint = targetZoneLine > 0 ? aiMonster.topTargetPoint : aiMonster.botTargetPoint;
        //GetComponent<AIEnemy>().attackPart = parts[Random.Range(0, parts.Length)];

        MoveStart();
    }

    protected virtual void Update()
    {
        if (myStatus == EnemyStatus.Die)
        {
            Destroy(gameObject);
            return;
        }

        GetDistanceToTarget();
    }

    void GetDistanceToTarget()
    {
        //return if enemy is villager
        if (enemyType == EnemyTypes.Villager || moveTargetPoint == null) return;

        var dist = transform.position.x - Mathf.Abs(moveTargetPoint.transform.position.x);

        //can attack monster
        if (dist < atkRange)
        {
            ChangeStatus(EnemyStatus.Attack);
        }
        else
        {
            ChangeStatus(EnemyStatus.Run);
        }
    }

    void ChangeStatus(EnemyStatus status)
    {
        if (myStatus == status) return;

        myStatus = status;

        switch (myStatus)
        {
            case EnemyStatus.Run:
                MoveStart();
                break;
            case EnemyStatus.Attack:
                StartAttack();
                break;
        }
    }

    public virtual void MoveStart()
    {
        AudioManager.Instance.FMODEvent_Ennemi_Walk.start();
    }

    public virtual void Moving()
    {
        if (myStatus != EnemyStatus.Run) return;
    }

    public virtual void StartAttack()
    {
        AudioManager.Instance.FMODEvent_Ennemi_Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AudioManager.Instance.FMODEvent_Ennemi_Attack.start();
    }

    public virtual void Attack()
    {

    }

    public virtual void Die()
    {
        ChangeStatus(EnemyStatus.Die);
        AudioManager.Instance.FMODEvent_Ennemi_BeingHit.start();
    }
}