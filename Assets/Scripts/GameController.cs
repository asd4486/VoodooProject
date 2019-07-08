using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float timer = 0f;
    private float durationBetweenInput = 1.5f;

    private bool head = false;
    public GameObject headImage;
    private bool chest = false;
    public GameObject chestImage;
    private bool rightArm = false;
    public GameObject rightArmImage;
    private bool leftArm = false;
    public GameObject leftArmImage;
    private bool rightLeg = false;
    public GameObject rightLegImage;
    private bool leftLeg = false;
    public GameObject leftLegImage;

    public GameObject validateHead;
    public GameObject validateChest;
    public GameObject validateRightArm;
    public GameObject validateLeftArm;
    public GameObject validateRightLeg;
    public GameObject validateLeftLeg;

    private bool inputAsked = false;

    public static bool[] boss = new bool[6];

    void Start()
    {
        boss[0] = head;
        boss[1] = chest;
        boss[2] = rightArm;
        boss[3] = leftArm;
        boss[4] = rightLeg;
        boss[5] = leftLeg;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= durationBetweenInput && !inputAsked)
        {           
            inputAsked = true;            
            AskInsertNeedle();
        }
        if (timer > durationBetweenInput + 2f)
        {
            timer = 0f;
            inputAsked = false;
            UnshowAllImage();                                                                                                                                                                                                                                                                                                                                           
        }
    }

    private void AskInsertNeedle()
    {
        if (Random.Range(0, 2) == 0) //one part
        {
            int random = Random.Range(0, 6);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            ShowImage(random);
        }
        else // two parts
        {
            int random = Random.Range(0, 6);
            ShowImage(random);
            random = Random.Range(0, 6);
            ShowImage(random);
        }
    }

    private void ShowImage(int r)
    {
        switch (r)
        {
            case 0: headImage.SetActive(true); boss[0] = true; break;
            case 1: chestImage.SetActive(true); boss[1] = true; break;
            case 2: rightArmImage.SetActive(true); boss[2] = true; break;
            case 3: leftArmImage.SetActive(true); boss[3] = true; break;
            case 4: rightLegImage.SetActive(true); boss[4] = true; break;
            case 5: leftLegImage.SetActive(true); boss[5] = true; break;
        }
    }

    public void UnshowImage(int r)
    {
        switch (r)
        {
            case 0:  headImage.SetActive(false); boss[0] = true; break;
            case 1 : chestImage.SetActive(false); boss[1] = true; break;
            case 2 : rightArmImage.SetActive(false); boss[2] = true; break;
            case 3 : leftArmImage.SetActive(false); boss[3] = true; break;
            case 4 : rightLegImage.SetActive(false); boss[4] = true; break;
            case 5 : leftLegImage.SetActive(false); boss[5] = true; break;
        }
    }

    private void UnshowAllImage()
    {
        headImage.SetActive(false);
        chestImage.SetActive(false);
        rightArmImage.SetActive(false);
        leftArmImage.SetActive(false);
        rightLegImage.SetActive(false);
        leftLegImage.SetActive(false);

        validateHead.SetActive(false);
        validateChest.SetActive(false);
        validateRightArm.SetActive(false);
        validateLeftArm.SetActive(false);
        validateRightLeg.SetActive(false);
        validateLeftLeg.SetActive(false);

        boss[0] = false;
        boss[1] = false;
        boss[2] = false;
        boss[3] = false;
        boss[4] = false;
        boss[5] = false;
    }

}
