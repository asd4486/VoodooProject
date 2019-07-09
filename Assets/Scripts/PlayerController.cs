using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    AIMonster myMonster;
    private float maxKeyUpTimer = 0.5f;

    Part cryingHead;
    Part angryHead;
    Part bodyPart;
    Part rightHand;
    Part leftHand;
    Part rightFoot;
    Part leftFoot;

    public Part[] allParts;
    public List<Part> topParts = new List<Part>();
    public List<Part> middleParts = new List<Part>();
    public List<Part> downParts = new List<Part>();

    private void Awake()
    {
        myMonster = FindObjectOfType<AIMonster>();
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

            p.LateStart();
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
        if (deadCount >= GameManager.Instance.partsDeadGameOver)
        {
            GameManager.Instance.GameOver();
        }
    }

    void InputController()
    {
        //tete gauche
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            cryingHead.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.Keypad9))
        {
            cryingHead.FinishHeal();
            myMonster.PlayAnimation("FlexTeteGauche");
        }

        //tete droite
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            angryHead.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Keypad7))
        {
            angryHead.FinishHeal();
            myMonster.PlayAnimation("FlexTeteDroite");
        }

        //tronc
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            bodyPart.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Keypad5))
        {
            bodyPart.FinishHeal();
            myMonster.PlayAnimation("FlexBuste");
        }

        //bras droit
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            rightHand.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Keypad4))
        {
            rightHand.FinishHeal();
            rightHand.Attack("BrasDroit");
        }

        //bras gauche
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            leftHand.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Keypad6))
        {
            leftHand.UnshowLines();
            leftHand.FinishHeal();
            leftHand.Attack("BrasGauche");
        }

        //jambe droite
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            rightFoot.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Keypad1))
        {
            rightFoot.UnshowLines();
            rightFoot.FinishHeal();
            myMonster.PlayAnimation("FlexJambeDroite");
        }

        //jambe gauche
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            leftFoot.StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Keypad3))
        {
            leftFoot.FinishHeal();
            leftFoot.ExpulseProjectiles();

            myMonster.PlayAnimation("FlexJambeGauche");
        }
    }

}
