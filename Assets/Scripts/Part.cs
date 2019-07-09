using Anima2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

public class Part : MonoBehaviour
{
    SpriteMeshAnimation mySpriteMeshStade;

    [SerializeField] Image healthBar;
    public ParticleSystem healParticle;

    public bool stopDamageOverTime = false;

    float maxPv = 100f;
    float myPv;

    public int projectileCount = 0;
    public float damagePerSecond;

    public bool heal = false;
    private VectorLine[] lines = new VectorLine[6];

    [HideInInspector]
    public bool dead = false;

    private void Awake()
    {
        mySpriteMeshStade = GetComponent<SpriteMeshAnimation>();
    }

    public void LateStart()
    {
        StartCoroutine(GetDamageOverTimeCoroutine());
        myPv = maxPv;
        healParticle.Pause();
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

        if (myPv <= 0)
        {
            dead = true;
            //sprite 4
        }
        else
        {
            dead = false;
        }


        if (heal)
        {
            healParticle.gameObject.SetActive(true);
            healParticle.Play();
        }
        else
        {
            healParticle.Pause();
            healParticle.gameObject.SetActive(false);
        }
    }

    public void HealFirst()
    {
        if (myPv <= 100)
        {
            myPv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageFirst / 100);
        }

        List<GameObject> l = new List<GameObject>();
        PartManager.asoc.TryGetValue(gameObject, out l);
        if (l != null)
        {
            foreach (GameObject g in l)
            {
                //g.GetComponent<Part>().HealSecond();
            }
        }
        if (heal == false)
        {
            //ShowLines(Color.green);
            heal = true;
        }
    }

    public void HealSecond()
    {
        if (myPv <= 100)
        {
            myPv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageSecond / 100);
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
        List<GameObject> l = new List<GameObject>();
        PartManager.asoc.TryGetValue(gameObject, out l);
        if (l != null)
        {
            foreach (GameObject g in l)
            {
                lines[index] = VectorLine.SetLine(c, gameObject.transform.position, g.transform.position);
                index++;
            }
        }
    }

    public void UnshowLines()
    {

        if (heal == true)
        {
            heal = false;
            foreach (VectorLine v in lines)
                VectorLine.lineManager.DisableLine(v, 0.01f);
        }
    }
}
