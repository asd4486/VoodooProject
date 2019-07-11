﻿using Anima2D;
using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

public enum MonsterPartType
{
    Body,
    LeftHand,
    RightHand,
    LeftFoot,
    RightFoot,
    CryHead,
    AngryHead
}

public class MonsterPart : MonoBehaviour
{
    AIMonster aiMonster;

    SpriteMeshAnimation mySpriteMeshStade;
    public MonsterPartType partType;

    [SerializeField] Image healthBar;

    public bool stopDamageOverTime = false;

    float myPv;
    [SerializeField] float maxPv = 100f;

    int projectileCount = 0;
    float damagePerSecond;

    float healDelayTimer;

    bool isHealing;
    private VectorLine[] lines = new VectorLine[6];

    [HideInInspector] public bool isDead;

    [SerializeField] float attackCooldown;
    float attackCooldownTimer;
    bool canAttack = true;

    public GameObject fxHeal;

    private void Awake()
    {
        mySpriteMeshStade = GetComponent<SpriteMeshAnimation>();
        myPv = maxPv;
    }

    public void Init(AIMonster _aiMonster)
    {
        StartCoroutine(GetDamageOverTimeCoroutine());
        aiMonster = _aiMonster;
    }

    void Update()
    {
        RefreshPV();

        isDead = myPv <= 0;
        //sprite 4

        Healing();
        CooldownAttack();
    }

    void RefreshPV()
    {
        healthBar.fillAmount = myPv / maxPv;

        if (myPv >= maxPv * 0.66f)
        {
            //sprite 1
            mySpriteMeshStade.frame = 0;
        }
        else if (myPv <= maxPv * 0.66f && myPv >= maxPv * 0.33f)
        {
            //sprite 2
            mySpriteMeshStade.frame = 1;
        }
        else if (myPv <= maxPv * 0.33f && myPv != 0)
        {
            //sprite 3
            mySpriteMeshStade.frame = 2;
        }

        if (myPv <= maxPv * 0.15f)
        {
            AudioManager.Instance.FMODEvent_Creature_LowLife.start();
        }
        else
        {
            var state = PLAYBACK_STATE.PLAYING;
            AudioManager.Instance.FMODEvent_Creature_LowLife.getPlaybackState(out state);
            if (state == PLAYBACK_STATE.PLAYING) AudioManager.Instance.FMODEvent_Creature_LowLife.start();
        }
    }

    public void StartHeal()
    {
        healDelayTimer = 0;
        AudioManager.Instance.FMODEvent_Creature_Healing.start();

        isHealing = true;
    }

    void Healing()
    {
        fxHeal.GetComponent<Animator>().SetBool("Healing", isHealing);
        healthBar.gameObject.transform.parent.GetComponent<Animator>().SetBool("Healing", isHealing);
        if (!isHealing) return;

        if (healDelayTimer < GameManager.Instance.healAfterDelay) healDelayTimer += Time.deltaTime;
        else HealFirst();
    }

    void HealFirst()
    {
        if (myPv <= 100)
        {
            myPv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageFirst / 100);
        }

        //List<GameObject> l = new List<GameObject>();
        //PlayerController.asoc.TryGetValue(gameObject, out l);
        //if (l != null)
        //{
        //    foreach (GameObject g in l)
        //    {
        //        //g.GetComponent<Part>().HealSecond();
        //    }
        //}


        //ShowLines(Color.green);
    }

    void HealSecond()
    {
        if (myPv <= 100)
        {
            myPv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageSecond / 100);
        }
    }

    public void FinishHeal()
    {
        isHealing = false;

        AudioManager.Instance.FMODEvent_Creature_Healing.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        damagePerSecond = projectileCount = 0;
        // UnshowLines();
        ExpulseProjectiles();
    }

    void CooldownAttack()
    {
        if (canAttack) return;
        attackCooldownTimer += Time.deltaTime;
        if (attackCooldownTimer > attackCooldown) canAttack = true;
    }

    public void Attack()
    {
        if (!canAttack) return;

        attackCooldownTimer = 0;
        canAttack = false;
        switch (partType)
        {
            case MonsterPartType.LeftHand:
                aiMonster.PlayAnimation("Left_Arm_Attack");
                AudioManager.Instance.FMODEvent_Creature_Attack.start();
                break;
            case MonsterPartType.RightHand:
                aiMonster.PlayAnimation("Right_Arm_Attack");
                AudioManager.Instance.FMODEvent_Creature_Attack.start();
                break;
                //case MonsterPartType.LeftFoot:
                //    aiMonster.PlayAnimation("Left_Leg_Flex");
                //    AudioManager.Instance.FMODEvent_Creature_Attack.start();
                //    break;
                //case MonsterPartType.RightFoot:
                //    aiMonster.PlayAnimation("Right_Leg_Flex");
                //    AudioManager.Instance.FMODEvent_Creature_Attack.start();
                //    break;
        }
    }



    public void GetDamage(float damageImpact, float damageOverTime, float invokeTimer)
    {
        StartCoroutine(ReducePV(damageImpact, damageOverTime, invokeTimer));
    }

    private IEnumerator ReducePV(float damageImpact, float damageOverTime, float invokeTimer)
    {
        yield return new WaitForSeconds(invokeTimer);        
        myPv = Mathf.Clamp(myPv - damageImpact, 0, maxPv);
        if (Random.Range(0, 100) <= GameManager.Instance.pourcentageChanceDot)
        {
            projectileCount++;
            damagePerSecond += damageOverTime;
        }
    }

    private IEnumerator GetDamageOverTimeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);            
            myPv = Mathf.Clamp(myPv - damagePerSecond, 0, maxPv);
            if (projectileCount == 0)
            {
                healthBar.gameObject.GetComponent<Animator>().SetBool("Damaged", false);
            }
            else
            {
                healthBar.gameObject.GetComponent<Animator>().SetBool("Damaged", true);
            }
        }
    }

    public void ExpulseProjectiles()
    {

    }

    public void ShowLines(Color c)
    {
        int index = 0;
        //List<GameObject> l = new List<GameObject>();
        //PlayerController.asoc.TryGetValue(gameObject, out l);
        //if (l != null)
        //{
        //    foreach (GameObject g in l)
        //    {
        //        lines[index] = VectorLine.SetLine(c, gameObject.transform.position, g.transform.position);
        //        index++;
        //    }
        //}
    }

    //public void UnshowLines()
    //{

    //    if (isHealing == true)
    //    {
    //        isHealing = false;
    //        foreach (VectorLine v in lines)
    //            VectorLine.lineManager.DisableLine(v, 0.01f);
    //    }
    //}



}
