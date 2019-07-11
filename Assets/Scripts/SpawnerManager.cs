using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject background;
    public GameObject walls;
    public GameObject wallsSpawner;
    public GameObject wall;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.isGameOver) return;

        walls.transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.tag == "Wall")
        {
            Instantiate(wall, wallsSpawner.transform.position, Quaternion.identity, walls.transform);
        }
    }
}
