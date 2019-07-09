using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnTimer;

    public GameObject[] enemies;
    public GameObject villager;
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
            if (Random.Range(0,5) <= 1)
            {
                SpawnVillager();
            }
        }
    }

    private void Spawn()
    {
        GameObject e;
        float r = Random.Range(0, 2);

        if (r == 0)
        {
            e = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerDown.transform);
            e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y + Random.Range(0,0.3f), e.transform.position.z);
            e.GetComponent<Enemy>().target = PartManager.allParts[Random.Range(0, PartManager.nbParts )].GetComponent<Part>();
        }
        else if (r == 1)
        {
            e = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerMiddle.transform);
            e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y + Random.Range(0, 0.3f), e.transform.position.z);
            e.GetComponent<Enemy>().target = PartManager.allParts[Random.Range(0, PartManager.nbParts )].GetComponent<Part>();
        }       
    }

    private void SpawnVillager()
    {
        Instantiate(villager, spawnerDown.transform);
    }
}
