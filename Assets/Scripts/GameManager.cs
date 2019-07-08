using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
