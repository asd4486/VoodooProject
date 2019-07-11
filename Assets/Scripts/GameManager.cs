using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    UIMain uiMain;
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
    int deadEnemyScore;

    public int partsDeadGameOver;

    public Text partCounterText;

    public GameObject spawner;
    public GameObject bars;

    [HideInInspector] public bool isGameOver = false;

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
        uiMain = FindObjectOfType<UIMain>();
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
        uiMain.Init();
    }

    public void AddScore()
    {
        if (isGameOver) return;

        deadEnemyScore += 1;
        uiMain.SetScoreTxt(deadEnemyScore);
    }

    public void GameOver()
    {
        uiMain.ShowGameOverUI(deadEnemyScore);

        isGameOver = true;
        //spawner.SetActive(false);
        //bars.SetActive(false);
        environmentSpeed = 0f;
    }
}
