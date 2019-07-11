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

public enum EnemySpawnZones
{
    Bottom,
    Middle
}

public class AIEnemy : MonoBehaviour
{
    GameMain main;
    protected PlayerController playerController;

    AIMonster aiMonster;
    [SerializeField] protected EnemyTypes enemyType;

    protected EnemyStatus myStatus;
    protected EnemySpawnZones spawnZone;

    //0 bot
    //1 top
    protected int targetZoneLine;

    protected Animator myAnimator;
    protected Rigidbody2D rb;


    //for check distance to monster
    protected Transform moveTargetPoint;

    protected MonsterPart attackTargetPart;
    [SerializeField] protected float atkRange;

    [SerializeField] protected float attackDelay = 1;
    protected float attackTimer;
    protected bool isAttaking;

    [SerializeField] protected float speedMultiplicator = 8f;

    private void Awake()
    {
        aiMonster = FindObjectOfType<AIMonster>();
        main = FindObjectOfType<GameMain>();

        playerController = FindObjectOfType<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    public void Init(EnemySpawnZones zone)
    {
        spawnZone = zone;

        if (spawnZone == EnemySpawnZones.Bottom)
        {
            var spriteRenderes = GetComponentsInChildren<SpriteRenderer>();
            foreach (var sp in spriteRenderes)
            {
                sp.sortingLayerName = "enemy1";
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-0.1f, 0.1f));

        //two lines for one zone
        targetZoneLine = Random.Range(0, 2);
        moveTargetPoint = targetZoneLine > 0 ? aiMonster.topTargetPoint : aiMonster.botTargetPoint;

        MoveStart();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (transform.position.x < -5)
        {
            Destroy(gameObject);
            return;
        }

        GetDistanceToTarget();

        switch (myStatus)
        {
            case EnemyStatus.Run:
                Moving();
                break;
            case EnemyStatus.Attack:
                Attacking();
                break;
        }
    }

    public void PlayAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
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
        PlayAnimation("walk");
    }

    public virtual void Moving()
    {
        if (myStatus != EnemyStatus.Run) return;

        //move directment
        rb.velocity = Vector3.right * GameManager.Instance.environmentSpeed * (speedMultiplicator - Random.Range(0, 2f));
    }

    public virtual void StartAttack()
    {
        AudioManager.Instance.FMODEvent_Ennemi_Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        rb.velocity = new Vector3(GameManager.Instance.environmentSpeed, 0, 0);
        attackTimer = 0;
        PlayAnimation("startAttack");
    }

    public virtual void Attacking()
    {
        if (!isAttaking) attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            PlayAnimation("attack");
            isAttaking = true;
        }
    }

    public virtual void EventAttack()
    {
        isAttaking = false;
    }

    public virtual void Die()
    {
        Instantiate(GameManager.Instance.particuleDeath, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Particle").transform);
        ChangeStatus(EnemyStatus.Die);

       

        main.AddScore();

        Destroy(gameObject, 0.05f);
    }

    protected MonsterPart GetClosestPart()
    {
        var allParts = playerController.allParts;
        MonsterPart closest = null;
        var closestDist = float.MaxValue;

        foreach (var p in allParts)
        {
            var dist = Vector2.Distance(transform.position, p.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = p;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MonsterAtkCollider>() != null)
        {
            Die();
        }
    }
}
