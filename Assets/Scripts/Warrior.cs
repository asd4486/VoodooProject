using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    Animator myAnimator;
    public float damage = 15;
    public float damagePerSecond = 1;

    public float speedMultiplicator = 6f;

    private bool canAttack = false;

    private float timerShoot = 0f;
    public float shootCooldown = 2f;

    public GameObject projectileSpawner;
    public GameObject projectile;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        //myAnimator.SetTrigger("Walk");
        //Invoke("StopAnimation", 6f);
    }

    public void StopAnimation()
    {
        speedMultiplicator = 0f;
        //canAttack = true;
        myAnimator.SetTrigger("StopWalk");
    }

    protected override void Update()
    {
        base.Update();

        //if (canAttack)
        //{
        //    timerShoot += Time.deltaTime;
        //    if (timerShoot > shootCooldown)
        //    {
        //        timerShoot = 0f;
        //        GameObject p = Instantiate(projectile, projectileSpawner.transform);

        //        p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).OnComplete(() => Destroy(p));

        //        target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
        //    }
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