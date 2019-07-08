﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
     Animator myAnimator;
    public GameObject projectileSpawner;
    public GameObject projectile;

    private float timerShoot = 0f;
    public float shootCooldown = 2f;
    public float damage = 10;
    public float damagePerSecond = 1;

    public float speedMultiplicator = 2f;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetTrigger("Walk");
        Invoke("StopAnimation", 2f);
    }

    private void StopAnimation()
    {
        myAnimator.SetTrigger("StopWalk");
        speedMultiplicator = 1f;
    }

    protected override void Update()
    {
        base.Update();
            
        //timerShoot += Time.deltaTime;
        //if (timerShoot > shootCooldown)
        //{
        //    timerShoot = 0f;
        //    GameObject p = Instantiate(projectile, projectileSpawner.transform);

        //    p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

        //    target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
        //}
        transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * speedMultiplicator * Time.deltaTime);
    }

    public void Shoot()
    {
        GameObject p = Instantiate(projectile, projectileSpawner.transform);

        p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

        target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
    }
}
