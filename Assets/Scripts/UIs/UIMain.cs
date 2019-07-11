using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField] Text textScore;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] Text textGameOverScore;

    public void Init()
    {
        gameOverUI.SetActive(false);
        textScore.text = "0";
    }

    public void SetScoreTxt(int score)
    {
        textScore.text = score.ToString();
    }

    public void ShowGameOverUI(int score)
    {
        textGameOverScore.text = score.ToString();
        gameOverUI.SetActive(true);
    }
}
