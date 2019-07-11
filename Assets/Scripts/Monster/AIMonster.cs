using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    [SerializeField] Animator myAnimator;

    //target point for enemy
    public Transform topTargetPoint;
    public Transform botTargetPoint;

    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform monsterUI;

    public void Init()
    {
        //set health bar for all parts
        foreach(var p in FindObjectOfType<PlayerController>().allParts)
        {
            var bar = Instantiate(healthBarPrefab);
            bar.transform.SetParent(monsterUI, false);

            p.Init(this, bar.transform);
        }

        PlayAnimation("walk");
        AudioManager.Instance.FMODEvent_Creature_Walk.start();
    }

    public void PlayAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }
}
