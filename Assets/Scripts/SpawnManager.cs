using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PlayerController playerController;
    private float spawnTimer;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] villagerPrefabs;

    [SerializeField] Transform[] spawnerMiddleGroup;
    [SerializeField] Transform[] spawnerDownGroup;

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
            SpawnEnemy();
            if (Random.Range(0, 5) <= 1)
            {
                SpawnVillager();
            }
        }
    }

    private void SpawnEnemy()
    {
        float r = Random.Range(0, 2);

        //0 top line
        //1 bottom line
        var spawnPoint = spawnerDownGroup[Random.Range(0, spawnerDownGroup.Length)];

        //e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], r < 1 ? spawnerDown : spawnerMiddle);
        GameObject e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(transform.position.x, spawnPoint.position.y), Quaternion.identity);

        e.GetComponent<AIEnemy>().Init(EnemySpawnZone.Bottom);
    }

    private void SpawnVillager()
    {
        var o = villagerPrefabs[Random.Range(0, villagerPrefabs.Length)];
        var posY = spawnerDownGroup[Random.Range(0, spawnerDownGroup.Length)].position.y;

        Instantiate(o, new Vector3(transform.position.x, posY), Quaternion.identity);
    }
}
