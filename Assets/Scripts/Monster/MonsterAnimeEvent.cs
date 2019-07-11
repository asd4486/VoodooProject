using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimeEvent : MonoBehaviour
{
    CameraShaker cameraShaker;

    [SerializeField] MonsterAtkCollider leftHandAtkCol;
    [SerializeField] MonsterAtkCollider rightHandAtkCol;

    [SerializeField] MonsterAtkCollider leftFootAtkCol;
    [SerializeField] MonsterAtkCollider rightFootAtkCol;


    private void Awake()
    {
        cameraShaker = FindObjectOfType<CameraShaker>();
    }

    public void EventLeftHandAttack()
    {
        leftHandAtkCol.StartAttack(true);
    }

    public void EventLeftHandIdle()
    {
        leftHandAtkCol.StartAttack(false);
    }

    public void EventRightHandAttack()
    {
        rightHandAtkCol.StartAttack(true);
    }

    public void EventRightHandIdle()
    {
        rightHandAtkCol.StartAttack(false);
    }

    public void EventLeftFootDown()
    {
        leftFootAtkCol.StartAttack(true);
        cameraShaker.CameraLittleDownShake();
    }

    public void EventLeftFootUp()
    {
        leftFootAtkCol.StartAttack(false);
    }

    public void EventRightFootDown()
    {
        rightFootAtkCol.StartAttack(true);
        cameraShaker.CameraLittleDownShake();
    }

    public void EventRightFootUp()
    {
        rightFootAtkCol.StartAttack(false);
    }
}
