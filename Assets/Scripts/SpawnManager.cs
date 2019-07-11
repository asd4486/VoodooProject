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

        if (GameManager.Instance.isGameOver) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            spawnTimer = Random.Range(GameManager.Instance.spawnTimerMin, GameManager.Instance.spawnTimerMax);

            SpawnVillager();
            if (Random.Range(0, 5) <= 1)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = null;

        //random zone
        float r = Random.Range(0, 2);
        //0 top line
        //1 bottom line
        if (r < 1) spawnPoint = spawnerMiddleGroup[Random.Range(0, spawnerMiddleGroup.Length)];
        else spawnPoint = spawnerDownGroup[Random.Range(0, spawnerDownGroup.Length)];

        //e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], r < 1 ? spawnerDown : spawnerMiddle);
        GameObject e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(transform.position.x, spawnPoint.position.y), Quaternion.identity);

        e.GetComponent<AIEnemy>().Init(r < 1 ? EnemySpawnZones.Middle : EnemySpawnZones.Bottom);
    }

    private void SpawnVillager()
    {
        Transform spawnPoint = spawnerDownGroup[Random.Range(0, spawnerDownGroup.Length)];

        var o = villagerPrefabs[Random.Range(0, villagerPrefabs.Length)];
        Instantiate(o, new Vector3(transform.position.x, spawnPoint.position.y), Quaternion.identity);
    }
}
