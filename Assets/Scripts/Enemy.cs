using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Part target;

    public bool dead = false;

    protected virtual void Update()
    {
        if (dead)
        {
            Destroy(gameObject);
            return;
        }
    }

    public virtual void Die()
    {
        dead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Instantiate(GameManager.Instance.particuleDeath, transform.position , Quaternion.identity, GameObject.FindGameObjectWithTag("Particle").transform);
            GameManager.Instance.enemyDeadCounter++;
            GameManager.Instance.enemyDeadCounterText.text = GameManager.Instance.enemyDeadCounter.ToString();
            Die();           
        }
    }
}
