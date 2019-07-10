using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  class GameManager : MonoBehaviour
{
    AIMonster aiMonster;
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

    public GameObject spawner;
    public GameObject bars;
    public GameObject panelGameOver;

    [HideInInspector] public bool gameOver = false;

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
        aiMonster = FindObjectOfType<AIMonster>();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        AudioManager.Instance.FMODEvent_Environnement.start();
        aiMonster.Init();
    }

    public void GameOver()
    {
        //Debug.Log("perdulol");
        panelGameOver.SetActive(true);
        gameOver = true;
        //spawner.SetActive(false);
        //bars.SetActive(false);
        //panelGameOver.SetActive(true);
        //environmentSpeed = 0f;
    }
}
