using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartManager : MonoBehaviour
{
    public static int nbParts = 7;
    private float maxKeyUpTimer = 0.5f;
    public GameObject part0; 
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;

    public float timerPart0;
    public float timerPart1;
    public float timerPart2;
    public float timerPart3;
    public float timerPart4;
    public float timerPart5;
    public float timerPart6;

    public List<GameObject> listPart0 = new List<GameObject>(); 
    public List<GameObject> listPart1 = new List<GameObject>();
    public List<GameObject> listPart2 = new List<GameObject>();
    public List<GameObject> listPart3 = new List<GameObject>();
    public List<GameObject> listPart4 = new List<GameObject>();
    public List<GameObject> listPart5 = new List<GameObject>();
    public List<GameObject> listPart6 = new List<GameObject>();


    public static Dictionary<GameObject, List<GameObject>> asoc = new Dictionary<GameObject, List<GameObject>>();

    public GameObject bar;

    public static List<GameObject> allParts = new List<GameObject>();
    public static List<GameObject> topParts = new List<GameObject>();
    public static List<GameObject> middleParts = new List<GameObject>();
    public static List<GameObject> downParts = new List<GameObject>();

    void Start()
    {
        allParts.Add(part0);
        allParts.Add(part1);
        allParts.Add(part2);
        allParts.Add(part3);
        allParts.Add(part4);
        allParts.Add(part5);
        allParts.Add(part6);

        topParts.Add(part0);
        topParts.Add(part1);
        topParts.Add(part2);

        middleParts.Add(part2);
        middleParts.Add(part3);
        middleParts.Add(part4);

        downParts.Add(part2);
        downParts.Add(part5);
        downParts.Add(part6);

        asoc.Add(part0, listPart0);
        asoc.Add(part1, listPart1);
        asoc.Add(part2, listPart2);
        asoc.Add(part3, listPart3);
        asoc.Add(part4, listPart4);
        asoc.Add(part5, listPart5);
        asoc.Add(part6, listPart6);

        foreach (KeyValuePair<GameObject, List<GameObject>> entry in asoc)
        {
            foreach(GameObject g in entry.Value)
            {
                //Debug.DrawLine(entry.Key.transform.position, g.transform.position, Color.white, 800000f);
                //Vectrosity.VectorLine.SetLine(Color.white, entry.Key.transform.position, g.transform.position);
            }
            //entry.Key.GetComponent<Part>().Bar = Instantiate(bar, FindObjectOfType<Canvas>().transform);
            entry.Key.GetComponent<Part>().LateStart();
        }        
    }

    void Update()
    {
        //tete gauche
        if (Input.GetKey(KeyCode.G))
        {
            timerPart0 += Time.deltaTime;
            if (timerPart0 > GameManager.Instance.healAfterDelay)
                part0.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("BONJOUR");
            timerPart0 = 0f;
            part0.GetComponent<Part>().UnshowLines();
            part0.GetComponent<Part>().damagePerSecond = 0;
            part0.GetComponent<Part>().projectileCount = 0;
            part0.GetComponent<Part>().ExpulseProjectiles();
        }

        //tete droite
        if (Input.GetKey(KeyCode.Q))
        {
            timerPart1 += Time.deltaTime;
            if (timerPart1 > GameManager.Instance.healAfterDelay)
                part1.GetComponent<Part>().HealFirst();
        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            timerPart1 = 0f;
            part1.GetComponent<Part>().UnshowLines();
            part1.GetComponent<Part>().damagePerSecond = 0;
            part1.GetComponent<Part>().projectileCount = 0;
            part1.GetComponent<Part>().ExpulseProjectiles();           
        }

        //tronc
        if (Input.GetKey(KeyCode.S))
        {
            timerPart2 += Time.deltaTime;
            if (timerPart2 > GameManager.Instance.healAfterDelay)
                part2.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            timerPart2 = 0f;
            part2.GetComponent<Part>().UnshowLines();
            part2.GetComponent<Part>().damagePerSecond = 0;
            part2.GetComponent<Part>().projectileCount = 0;
            part2.GetComponent<Part>().ExpulseProjectiles();
        }

        //bras droit
        if (Input.GetKey(KeyCode.Z))
        {
            timerPart3 += Time.deltaTime;
            if (timerPart3 > GameManager.Instance.healAfterDelay)
                part3.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            timerPart3 = 0f;
            part3.GetComponent<Part>().UnshowLines();
            part3.GetComponent<Part>().damagePerSecond = 0;
            part3.GetComponent<Part>().projectileCount = 0;
            part3.GetComponent<Part>().ExpulseProjectiles();
            part3.GetComponent<Part>().Attack("BrasDroit");
        }

        //bras gauche
        if (Input.GetKey(KeyCode.Mouse1))
        {
            timerPart4 += Time.deltaTime;
            if (timerPart4 > GameManager.Instance.healAfterDelay)
                part4.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            timerPart4 = 0f;
            part4.GetComponent<Part>().UnshowLines();
            part4.GetComponent<Part>().damagePerSecond = 0;
            part4.GetComponent<Part>().projectileCount = 0;
            part4.GetComponent<Part>().ExpulseProjectiles();
            part4.GetComponent<Part>().Attack("BrasGauche");
        }

        //jambe droite
        if (Input.GetKey(KeyCode.D))
        {
            timerPart5 += Time.deltaTime;
            if (timerPart5 > GameManager.Instance.healAfterDelay)
                part5.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            timerPart5 = 0f;
            part5.GetComponent<Part>().UnshowLines();
            part5.GetComponent<Part>().damagePerSecond = 0;
            part5.GetComponent<Part>().projectileCount = 0;
            part5.GetComponent<Part>().ExpulseProjectiles();
        }

        //jambe gauche
        if (Input.GetKey(KeyCode.F))
        {
            timerPart6 += Time.deltaTime;
            if (timerPart6 > GameManager.Instance.healAfterDelay)
                part6.GetComponent<Part>().HealFirst();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            timerPart6 = 0f;
            part6.GetComponent<Part>().UnshowLines();
            part6.GetComponent<Part>().damagePerSecond = 0;
            part6.GetComponent<Part>().projectileCount = 0;
            part6.GetComponent<Part>().ExpulseProjectiles();
            part6.GetComponent<Part>().Attack("JambeGauche");
        }
    }
}
