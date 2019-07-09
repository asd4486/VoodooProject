using Anima2D;
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
    SpriteMeshAnimation mySpriteMeshStade;
    public MonsterPartType partType;

    [SerializeField] Image healthBar;
    public ParticleSystem healParticle;

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
        healParticle.Pause();
        myPv = maxPv;
    }

    public void LateStart()
    {
        StartCoroutine(GetDamageOverTimeCoroutine());
    }

    void Update()
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

        isDead = myPv <= 0;
        //sprite 4
    }

    public void StartHeal()
    {
        healDelayTimer = 0;
        healParticle.gameObject.SetActive(true);
        healParticle.Play();
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

        healParticle.Pause();
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
        if (membre == "BrasGauche")
        {
            GameObject.FindGameObjectWithTag("Monster").gameObject.transform.GetComponent<Animator>().SetTrigger("BrasGauche");
        }
        else if (membre == "JambeGauche")
        {
            GameObject.FindGameObjectWithTag("Monster").gameObject.transform.GetComponent<Animator>().SetTrigger("JambeGauche");
        }
        else if (membre == "BrasDroit")
        {
            GameObject.FindGameObjectWithTag("Monster").gameObject.transform.GetComponent<Animator>().SetTrigger("BrasDroit");
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
