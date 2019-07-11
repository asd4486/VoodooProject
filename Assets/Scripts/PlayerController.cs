using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameMain main;

    AIMonster aiMoster;
    private float maxKeyUpTimer = 0.5f;

    MonsterPart cryingHead;
    MonsterPart angryHead;
    MonsterPart bodyPart;
    MonsterPart rightHand;
    MonsterPart leftHand;
    MonsterPart rightFoot;
    MonsterPart leftFoot;

    [HideInInspector] public MonsterPart[] allParts;
    [HideInInspector] public List<MonsterPart> topParts = new List<MonsterPart>();
    [HideInInspector] public List<MonsterPart> middleParts = new List<MonsterPart>();
    [HideInInspector] public List<MonsterPart> downParts = new List<MonsterPart>();

    private void Awake()
    {
        main = FindObjectOfType<GameMain>();
        aiMoster = FindObjectOfType<AIMonster>();
    }

    void Start()
    {
        allParts = FindObjectsOfType<MonsterPart>();

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

        //GAME OVER
        if (deadCount >= GameManager.Instance.partsDeadGameOver && GameManager.Instance.isGameOver == false)
        {
            main.GameOver();
        }

        //if (GameManager.Instance.isGameOver == true && Input.GetKeyDown(KeyCode.S))
        //{
            
        //}
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
            angryHead.Attack();
        }

        //cry head
        if (Input.GetButtonDown("cryHead"))
        {
            cryingHead.StartHeal();
        }
        else if (Input.GetButtonUp("cryHead"))
        {
            cryingHead.FinishHeal();
            cryingHead.Attack();
        }

        //body
        if (Input.GetButtonDown("body"))
        {
            bodyPart.StartHeal();
        }
        else if (Input.GetButtonUp("body"))
        {
            bodyPart.FinishHeal();
            bodyPart.Attack();
        }

        //right hand
        if (Input.GetButtonDown("rightHand"))
        {
            rightHand.StartHeal();
        }
        else if (Input.GetButtonUp("rightHand"))
        {
            rightHand.FinishHeal();
            rightHand.Attack();
        }

        //left hand
        if (Input.GetButtonDown("leftHand"))
        {
            leftHand.StartHeal();
        }
        else if (Input.GetButtonUp("leftHand"))
        {
            leftHand.FinishHeal();
            leftHand.Attack();
        }

        //right foot
        if (Input.GetButtonDown("rightFoot"))
        {
            rightFoot.StartHeal();
        }
        else if (Input.GetButtonUp("rightFoot"))
        {
            rightFoot.FinishHeal();
            rightFoot.Attack();
        }

        //left foot
        if (Input.GetButtonDown("leftFoot"))
        {
            leftFoot.StartHeal();
        }
        else if (Input.GetButtonUp("leftFoot"))
        {
            leftFoot.FinishHeal();
            leftFoot.Attack();
        }
    }

}
