using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScroller : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.right * GameManager.Instance.environmentSpeed * Time.deltaTime);
    }
}
