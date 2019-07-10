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

public class Part : MonoBehaviour
{
    AIMonster aiMonster;

    SpriteMeshAnimation mySpriteMeshStade;
    public MonsterPartType partType;

    [SerializeField] Image healthBar;
    [SerializeField] ParticleSystem healParticle;

    public bool stopDamageOverTime = false;

    float maxPv = 100f;
    float myPv;

    int projectileCount = 0;
    float damagePerSecond;

    float healDelayTimer;
    bool isHealing = false;
    private VectorLine[] lines = new VectorLine[6];

    [HideInInspector] public bool isDead;

    private void Awake()
    {
        mySpriteMeshStade = GetComponent<SpriteMeshAnimation>();
        healParticle.Stop();
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
        healParticle.Play();
        AudioManager.Instance.FMODEvent_Creature_Healing.start();

        isHealing = true;
    }

    void Healing()
    {
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
        healParticle.Stop();
        damagePerSecond = projectileCount = 0;
        UnshowLines();
        ExpulseProjectiles();
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
        }
    }

    public void ExpulseProjectiles()
    {

    }

    public void Attack(string membre)
    {
        switch (membre)
        {
            case "BrasGauche":
                aiMonster.PlayAnimation("BrasGauche");
                AudioManager.Instance.FMODEvent_Creature_Attack.start();
                break;
            case "JambeGauche":
                aiMonster.PlayAnimation("JambeGauche");
                AudioManager.Instance.FMODEvent_Creature_Attack.start();
                break;
            case "BrasDroit":
                aiMonster.PlayAnimation("BrasDroit");
                AudioManager.Instance.FMODEvent_Creature_Attack.start();
                break;
        }
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

    public void UnshowLines()
    {

        if (isHealing == true)
        {
            isHealing = false;
            foreach (VectorLine v in lines)
                VectorLine.lineManager.DisableLine(v, 0.01f);
        }
    }
}
