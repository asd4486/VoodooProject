using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    public float damage = 10;
    public float damagePerSecond = 1;

    public float speedMultiplicator = 6f;

    private void Start()
    {
        GetComponent<Animator>().SetTrigger("Walk");
        Invoke("StopAnimation", 6f);
    }

    public void StopAnimation()
    {
        //GetComponent<Animator>().SetTrigger("StopWalk
        speedMultiplicator = 0f;
    }

    protected override void Update()
    {
        base.Update();

        transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * speedMultiplicator * Time.deltaTime);
    }

    public void Attack()
    {

    }
}