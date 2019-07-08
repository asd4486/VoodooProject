using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public GameController gameController;

    public float cpt = 0f;

    public Text cptText;

    void Start()
    {
        
    }

    void Update()
    {
        cptText.text = cpt.ToString();

        // --------------------------------------head--------------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GameController.boss[0] == true)
            {
                gameController.UnshowImage(0);
                gameController.validateHead.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cpt += Time.deltaTime;
        }

        // --------------------------------------chest--------------------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GameController.boss[1] == true)
            {
                gameController.UnshowImage(1);
                gameController.validateChest.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cpt += Time.deltaTime;
        }


        // --------------------------------------rightArm--------------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            if (GameController.boss[2] == true)
            {
                gameController.UnshowImage(2);
                gameController.validateRightArm.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {

        }
        if (Input.GetMouseButton(0))
        {
            cpt += Time.deltaTime;
        }

        // --------------------------------------leftArm--------------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameController.boss[3] == true)
            {
                gameController.UnshowImage(3);
                gameController.validateLeftArm.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

        }
        if (Input.GetKey(KeyCode.Space))
        {
            cpt += Time.deltaTime;
        }

        // --------------------------------------rightLeg--------------------------------------
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GameController.boss[4] == true)
             {
                gameController.UnshowImage(4);
                gameController.validateRightLeg.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cpt += Time.deltaTime;
        }

        // --------------------------------------leftLeg--------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (GameController.boss[5] == true)
            {
                gameController.UnshowImage(5);
                gameController.validateLeftLeg.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cpt += Time.deltaTime;
        }
    }
}
