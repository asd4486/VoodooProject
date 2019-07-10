using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    AIMonster aiMoster;
    private float maxKeyUpTimer = 0.5f;

    Part cryingHead;
    Part angryHead;
    Part bodyPart;
    Part rightHand;
    Part leftHand;
    Part rightFoot;
    Part leftFoot;

    [HideInInspector] public Part[] allParts;
    [HideInInspector] public List<Part> topParts = new List<Part>();
    [HideInInspector] public List<Part> middleParts = new List<Part>();
    [HideInInspector] public List<Part> downParts = new List<Part>();

    private void Awake()
    {
        aiMoster = FindObjectOfType<AIMonster>();
    }

    void Start()
    {
        allParts = FindObjectsOfType<Part>();

        foreach (var p in allParts)
        {
            switch (p.partType)
            {
                case MonsterPartType.CryHead:
                    topParts.Add(p);
                    cryingHead = p;
                    break;
                case MonsterPartType.AngryHead:
                    topParts.Add(p);
                    angryHead = p;
                    break;
                case MonsterPartType.Body:
                    topParts.Add(p);
                    middleParts.Add(p);
                    bodyPart = p;
                    break;
                case MonsterPartType.LeftHand:
                    middleParts.Add(p);
                    leftHand = p;
                    break;
                case MonsterPartType.RightHand:
                    middleParts.Add(p);
                    rightHand = p;
                    break;
                case MonsterPartType.LeftFoot:
                    downParts.Add(p);
                    leftFoot = p;
                    break;
                case MonsterPartType.RightFoot:
                    downParts.Add(p);
                    rightFoot = p;
                    break;
            }

            p.Init(aiMoster);
        }

        //asoc.Add(part0, listPart0);
        //asoc.Add(part1, listPart1);
        //asoc.Add(part2, listPart2);
        //asoc.Add(part3, listPart3);
        //asoc.Add(part4, listPart4);
        //asoc.Add(part5, listPart5);
        //asoc.Add(part6, listPart6);

        //foreach (KeyValuePair<GameObject, List<GameObject>> entry in asoc)
        //{
        //    foreach (GameObject g in entry.Value)
        //    {
        //        //Debug.DrawLine(entry.Key.transform.position, g.transform.position, Color.white, 800000f);
        //        //Vectrosity.VectorLine.SetLine(Color.white, entry.Key.transform.position, g.transform.position);
        //    }
        //    //entry.Key.Bar = Instantiate(bar, FindObjectOfType<Canvas>().transform);
        //    
        //}
    }

    private void LateUpdate()
    {
        CheckDeadPartCount();
        InputController();
    }

    void CheckDeadPartCount()
    {
        var deadCount = allParts.Where(p => p.isDead).ToArray().Length;
        GameManager.Instance.partCounterText.text = deadCount.ToString();

        //GAME OVER
        if ((deadCount >= GameManager.Instance.partsDeadGameOver || Input.GetKeyDown(KeyCode.I)) && GameManager.Instance.gameOver == false)
        {
            GameManager.Instance.GameOver();
        }

        if (GameManager.Instance.gameOver == true && Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void InputController()
    {
        //angry head
        if (Input.GetButtonDown("angryHead"))
        {
            angryHead.StartHeal();
        }
        else if (Input.GetButtonUp("angryHead"))
        {
            angryHead.FinishHeal();
            aiMoster.PlayAnimation("FlexTeteGauche");
        }

        //cry head
        if (Input.GetButtonDown("cryHead"))
        {
            cryingHead.StartHeal();
        }
        else if (Input.GetButtonUp("cryHead"))
        {
            cryingHead.FinishHeal();
            aiMoster.PlayAnimation("FlexTeteDroite");
        }

        //body
        if (Input.GetButtonDown("body"))
        {
            bodyPart.StartHeal();
        }
        else if (Input.GetButtonUp("body"))
        {
            bodyPart.FinishHeal();
            aiMoster.PlayAnimation("FlexBuste");
        }

        //right hand
        if (Input.GetButtonDown("rightHand"))
        {
            rightHand.StartHeal();
        }
        else if (Input.GetButtonUp("rightHand"))
        {
            rightHand.FinishHeal();
            rightHand.Attack("BrasDroit");
        }

        //left hand
        if (Input.GetButtonDown("leftHand"))
        {
            leftHand.StartHeal();
        }
        else if (Input.GetButtonUp("leftHand"))
        {
            leftHand.UnshowLines();
            leftHand.FinishHeal();
            leftHand.Attack("BrasGauche");
        }

        //right foot
        if (Input.GetButtonDown("rightFoot"))
        {
            rightFoot.StartHeal();
        }
        else if (Input.GetButtonUp("rightFoot"))
        {
            rightFoot.UnshowLines();
            rightFoot.FinishHeal();
            aiMoster.PlayAnimation("FlexJambeDroite");
        }

        //left foot
        if (Input.GetButtonDown("leftFoot"))
        {
            leftFoot.StartHeal();
        }
        else if (Input.GetButtonUp("leftFoot"))
        {
            leftFoot.FinishHeal();
            leftFoot.ExpulseProjectiles();

            aiMoster.PlayAnimation("FlexJambeGauche");
        }
    }

}
