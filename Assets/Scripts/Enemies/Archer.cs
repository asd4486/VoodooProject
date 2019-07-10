using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AIEnemy
{
    private float timerShoot = 0f;
    public float shootCooldown = 2f;

    private void Start()
    {
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
        // Vector3 targetDir = target.transform.position - transform.position;
        // GameObject p = Instantiate(projectile, projectileSpawner.transform);
        // float angle = Vector3.Angle(targetDir, p.transform.up);
        // p.transform.Rotate(0, 0, angle);
        //projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y, projectile.transform.position.z + angle);


        // p.transform.DOMove(target.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));

        // target.GetDamage(damage, damagePerSecond, GameManager.Instance.projectileTravelTime);
    }
}
