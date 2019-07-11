using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    PlayerController playerController;
    AIMonster aiMonster;
    UIMain uiMain;

    int deadEnemyScore;
    int monsterDeadPartCount;

    void Awake()
    {
        aiMonster = FindObjectOfType<AIMonster>();
        uiMain = FindObjectOfType<UIMain>();
        playerController = FindObjectOfType<PlayerController>();
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

    private void Update()
    {
        CheckDeadPartCount();
    }

    void CheckDeadPartCount()
    {
        if (GameManager.Instance.isGameOver) return;

        monsterDeadPartCount = playerController.allParts.Where(p => p.isDead).ToArray().Length;
        uiMain.SetDeadPartCount(monsterDeadPartCount);

        //GAME OVER
        if (monsterDeadPartCount >= GameManager.Instance.partsDeadGameOver)
            GameOver();
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
        GameManager.Instance.environmentSpeed = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
