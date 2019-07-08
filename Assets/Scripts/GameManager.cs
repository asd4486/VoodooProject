using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  class GameManager : MonoBehaviour
{
    public float spawnTimerMin;
    public float spawnTimerMax;
    
    public float projectileTravelTime;

    public float environmentSpeed;

    public float healMultiplicator;
    public float healMultiplicatorPourcentageFirst;
    public float healMultiplicatorPourcentageSecond;

    public float healAfterDelay;

    public GameObject particuleDeath;

    public float pourcentageChanceDot;

    public int partsDeadGameOver;

    public int enemyDeadCounter = 0;
    public Text enemyDeadCounterText;

    public Text partCounterText;

    public GameObject monster;
    public GameObject spawner;
    public GameObject bars;
    public GameObject panelGameOver;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        monster.SetActive(false);
        spawner.SetActive(false);
        bars.SetActive(false);
        panelGameOver.SetActive(true);
        environmentSpeed = 0f;

    }
}
