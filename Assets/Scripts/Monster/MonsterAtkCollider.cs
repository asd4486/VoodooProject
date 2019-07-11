using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MonsterAtkCollider : MonoBehaviour
{
    Collider2D myCol;

    private void Awake()
    {
        myCol = GetComponent<Collider2D>();
        myCol.enabled = false;
    }

    public void StartAttack(bool b)
    {
        myCol.enabled = b;
    }

}
