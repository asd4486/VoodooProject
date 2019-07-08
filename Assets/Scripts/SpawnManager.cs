using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnTimer;

    public GameObject[] enemies;
    public GameObject spawnerTop;
    public GameObject spawnerMiddle;
    public GameObject spawnerDown;


    void Start()
    {
        spawnTimer = Random.Range(GameManager.Instance.spawnTimerMin, GameManager.Instance.spawnTimerMax);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            spawnTimer = Random.Range(GameManager.Instance.spawnTimerMin, GameManager.Instance.spawnTimerMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject e;
        float r = Random.Range(0, 2);
        if (r == 0)
        {
            e = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerDown.transform);
            e.GetComponent<Enemy>().target = PartManager.allParts[Random.Range(0, PartManager.nbParts )].GetComponent<Part>();
        }
        else if (r == 1)
        {
            e = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerMiddle.transform);
            e.GetComponent<Enemy>().target = PartManager.allParts[Random.Range(0, PartManager.nbParts )].GetComponent<Part>();
        }
        //else
        //{
        //    e = Instantiate(enemy, spawnerDown.transform);
        //    e.GetComponent<Archer>().target = PartManager.downParts[Random.Range(0, PartManager.downParts.ToArray().Length)].GetComponent<Part>();
        //}
        
    }
}
