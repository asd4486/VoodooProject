using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    AIMonster aiMonster;
    UIMain uiMain;

    int deadEnemyScore;
    int monsterDeadPartCount;

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
        GameManager.Instance.Init();

        AudioManager.Instance.FMODEvent_Environnement.start();
        aiMonster.Init();
        uiMain.Init();
    }

    public void AddScore()
    {
        if (GameManager.Instance.isGameOver) return;

        deadEnemyScore += 1;
        uiMain.SetScoreTxt(deadEnemyScore);
    }

    public void GameOver()
    {
        uiMain.ShowGameOverUI(deadEnemyScore);

        GameManager.Instance.isGameOver = true;
        //spawner.SetActive(false);
        //bars.SetActive(false);
        GameManager.Instance.environmentSpeed = 0f;
    }

    public void ChangeMonsterDeadPartCount()
    {
        //GameManager.Instance.partCounterText.text = deadCount.ToString();
    }
}
