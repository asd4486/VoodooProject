using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

public class Part : MonoBehaviour
{
    public GameObject Bar;
    public ParticleSystem healParticle;
    public Text textProjectile;
    private float barFilledWidth;
    private float barFilledHeight;

    public bool stopDamageOverTime = false;
    public float maxPv = 100f;
    public float pv;
    public Material healMaterial;
    public Material basicMaterial;

    public int projectileCount = 0;
    public float damagePerSecond;

    public bool heal = false;
    private VectorLine[] lines = new VectorLine[6];

    public void LateStart()
    {
        barFilledWidth = Bar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        barFilledHeight = Bar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        textProjectile = Bar.transform.GetChild(1).GetComponent<Text>();
        StartCoroutine(GetDamageOverTimeCoroutine());
        pv = maxPv;
        healParticle.Pause();
    }

    void Update()
    {
        //Bar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.2f);
        Bar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(barFilledWidth * (pv/100), barFilledHeight);

        textProjectile.text = projectileCount.ToString();

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
        if(pv <= 100)
        {
            pv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageFirst/100);
        }

        List<GameObject> l = new List<GameObject>();
        PartManager.asoc.TryGetValue(gameObject, out l);
        if (l != null)
        {
            foreach (GameObject g in l)
            {
                g.GetComponent<Part>().HealSecond();
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
        if (pv <= 100)
        {
            pv += Time.deltaTime * GameManager.Instance.healMultiplicator * (GameManager.Instance.healMultiplicatorPourcentageSecond/100);
        }
    }

    public void GetDamage(float damageImpact, float damageOverTime, float invokeTimer)
    {
        StartCoroutine(ReducePV(damageImpact, damageOverTime, invokeTimer));        
    }

    private IEnumerator ReducePV(float damageImpact, float damageOverTime, float invokeTimer)
    {
        yield return new WaitForSeconds(invokeTimer);
        pv = Mathf.Clamp(pv-damageImpact, 0, maxPv);
        if (Random.Range(0,100) <= GameManager.Instance.pourcentageChanceDot)
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
            pv = Mathf.Clamp(pv - damagePerSecond, 0, maxPv);
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
                lines[index]     = VectorLine.SetLine(c, gameObject.transform.position, g.transform.position);
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
