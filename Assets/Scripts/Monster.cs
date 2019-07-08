using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private float startSpeed;

    

    private void Start()
    {
        startSpeed = GameManager.Instance.environmentSpeed;
    }

    public void StartAnimation()
    {
        //GameManager.Instance.environmentSpeed = 0f;
    }

    public void FinishAnimation()
    {
        GameManager.Instance.environmentSpeed = startSpeed;
    }
}
