using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public float spawnTimerMin;
    public float spawnTimerMax;

    public float projectileTravelTime;

    [SerializeField] float environmentDefaultSpeed;
    [HideInInspector] public float environmentSpeed;

    public float healMultiplicator;
    public float healMultiplicatorPourcentageFirst;
    public float healMultiplicatorPourcentageSecond;

    public float healAfterDelay;

    public GameObject particuleDeath;

    public float pourcentageChanceDot;

    public int partsDeadGameOver;

    [HideInInspector] public bool isGameOver;

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

    public void Init()
    {
        environmentSpeed = environmentDefaultSpeed;

        isGameOver = false;
    }
}
