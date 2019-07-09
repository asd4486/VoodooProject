using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public float speedMultiplicator = 8f;

    void Update()
    {
        transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * (speedMultiplicator - Random.Range(0,3)) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CacAttack")
        {
            //die
            Instantiate(GameManager.Instance.particuleDeath, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Particle").transform);
            Destroy(gameObject);
        }
    }
}
