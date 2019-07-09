using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PlayerController playerController;
    private float spawnTimer;

    public GameObject[] enemies;
    public GameObject villager;
    public GameObject spawnerMiddle;
    public GameObject spawnerDown;


    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        spawnTimer = Random.Range(GameManager.Instance.spawnTimerMin, GameManager.Instance.spawnTimerMax);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            spawnTimer = Random.Range(GameManager.Instance.spawnTimerMin, GameManager.Instance.spawnTimerMax);
            Spawn();
            if (Random.Range(0, 5) <= 1)
            {
                SpawnVillager();
            }
        }
    }

    private void Spawn()
    {
        GameObject e;
        float r = Random.Range(0, 2);
        e = Instantiate(enemies[Random.Range(0, enemies.Length)], r < 1 ? spawnerDown.transform : spawnerMiddle.transform);

        e.GetComponent<Enemy>().SetTarget(playerController.allParts);
    }

    private void SpawnVillager()
    {
        Instantiate(villager, spawnerDown.transform);
    }
}
