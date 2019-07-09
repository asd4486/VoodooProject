using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Part target;

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

    public void SetTarget(Part[] parts)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(0, 0.3f), transform.position.z);
        GetComponent<Enemy>().target = parts[Random.Range(0, parts.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Instantiate(GameManager.Instance.particuleDeath, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Particle").transform);
            GameManager.Instance.enemyDeadCounter++;
            GameManager.Instance.enemyDeadCounterText.text = GameManager.Instance.enemyDeadCounter.ToString();
            Die();
        }
    }
}
