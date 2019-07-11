using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] bgs;
    public GameObject[] walls;

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        foreach (var bg in bgs)
        {
            bg.transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed  * Time.deltaTime);
            if (bg.transform.localPosition.x <= -10) bg.transform.localPosition = new Vector3(20, bg.transform.localPosition.y);
        }

        foreach (var w in walls)
        {
            w.transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * Time.deltaTime);
            if (w.transform.localPosition.x <= -10) w.transform.localPosition = new Vector3(20, w.transform.localPosition.y);
        }
    }
}
