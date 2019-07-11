using System.Collections.Generic;
using UnityEngine;

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
        allParts = FindObjectsOfType<MonsterPart>();
    }

    void Start()
    {
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
        }
    }

    private void LateUpdate()
    {
        InputController();
    }

    void InputController()
    {
        if (GameManager.Instance.isGameOver)
        {
            if (Input.GetButtonDown("body"))
                main.RestartGame();

            return;
        }
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
