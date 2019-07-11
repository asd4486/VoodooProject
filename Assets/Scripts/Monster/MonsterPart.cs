using Anima2D;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] SpriteMeshAnimation[] mySpriteMeshStades;
    SpriteMeshInstance mySpriteMeshInstance;

    public MonsterPartType partType;

    Transform healthBar;
    Image healthBarValue;

    [SerializeField] Vector3 healthBarTransformOffset;

    float myPv;
    [SerializeField] float maxPv = 100f;

    List<AIWeapon> projectileList = new List<AIWeapon>();
    int projectileCount;

    float damagePerSecond;

    float healDelayTimer;

    bool isHealing;

    [HideInInspector] public bool isDead;

    [SerializeField] float attackCooldown;
    float attackCooldownTimer;
    bool canAttack = true;

    [SerializeField] GameObject fxHeal;

    private void Awake()
    {
        mySpriteMeshInstance = mySpriteMeshStades[0].GetComponent<SpriteMeshInstance>();
        myPv = maxPv;
    }

    public void Init(AIMonster _aiMonster, Transform _healthBar)
    {
        aiMonster = _aiMonster;
        healthBar = _healthBar;
        healthBarValue = healthBar.GetChild(0).GetComponent<Image>();
        
        StartCoroutine(GetDamageOverTimeCoroutine());
    }

    void Update()
    {
        RefreshPV();

        Healing();
        CooldownAttack();
    }

    void RefreshPV()
    {
        if (healthBar == null) return;

        //health bar follow body
        healthBar.transform.position = transform.position + healthBarTransformOffset;

        isDead = myPv <= 0;
        healthBarValue.fillAmount = myPv / maxPv;

        foreach (var stade in mySpriteMeshStades)
        {
            if (myPv >= maxPv * 0.66f)
            {
                //sprite 1
                stade.frame = 0;
            }
            else if (myPv <= maxPv * 0.66f && myPv >= maxPv * 0.33f)
            {
                //sprite 2
                stade.frame = 1;
            }
            else if (myPv <= maxPv * 0.33f && myPv != 0)
            {
                //sprite 3
                stade.frame = 2;
            }
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
        healthBar.GetComponent<Animator>().SetBool("Healing", isHealing);
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
        ExpulseProjectiles();
    }

    public void ExpulseProjectiles()
    {
        foreach (var p in projectileList)
        {
            if (p != null) p.Out();
        }

        projectileList.Clear();
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
                AudioManager.Instance.FMODEvent_Creature_Attack_base.start();
                break;
            case MonsterPartType.RightHand:
                aiMonster.PlayAnimation("Right_Arm_Attack");
                AudioManager.Instance.FMODEvent_Creature_Attack_longArm.start();
                break;
            case MonsterPartType.AngryHead:
                aiMonster.PlayAnimation("Left_Head_Flex");
                AudioManager.Instance.FMODEvent_Creature_Attack_head_aggro.start();
                break;
            case MonsterPartType.CryHead:
                aiMonster.PlayAnimation("Right_Head_Flex");
                AudioManager.Instance.FMODEvent_Creature_Attack_head_nice.start();
                break;
            case MonsterPartType.LeftFoot:
                aiMonster.PlayAnimation("Left_Leg_Flex");
                AudioManager.Instance.FMODEvent_Creature_Attack_feet.start();
                //audiomanager.instance.fmodevent_creature_attack.start();
                break;
            case MonsterPartType.RightFoot:
                aiMonster.PlayAnimation("Right_Leg_Flex");
                AudioManager.Instance.FMODEvent_Creature_Attack_feet.start();
                //audiomanager.instance.fmodevent_creature_attack.start();
                break;
        }
    }

    public void GetDamage(float damageImpact, float damageOverTime = 0, float invokeTimer = 0, AIWeapon targetProjectile = null)
    {
        myPv = Mathf.Clamp(myPv - damageImpact, 0, maxPv);

        if (targetProjectile == null) return;

        //change sorting layer
        var targetSpriteRendrer = targetProjectile.GetComponent<SpriteRenderer>();
        targetSpriteRendrer.sortingLayerName = mySpriteMeshInstance.sortingLayerName;
        targetSpriteRendrer.sortingOrder = mySpriteMeshInstance.sortingOrder + 3;

        projectileList.Add(targetProjectile);

        StartCoroutine(ReducePvCoroutine(damageOverTime, invokeTimer));

    }

    private IEnumerator ReducePvCoroutine(float damageOverTime, float invokeTimer)
    {
        yield return new WaitForSeconds(invokeTimer);
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
                healthBarValue.gameObject.GetComponent<Animator>().SetBool("Damaged", false);
            }
            else
            {
                healthBarValue.gameObject.GetComponent<Animator>().SetBool("Damaged", true);
            }
        }
    }

}
