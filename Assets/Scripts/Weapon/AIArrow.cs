
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArrow : AIWeapon
{
    bool isMoving;
    MonsterPart TargetPart;
    // Start is called before the first frame update

    internal void Init(MonsterPart target)
    {
        TargetPart = target;

        Vector3 diff = TargetPart.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        //p.transform.DOMove(attackTargetPart.transform.position, GameManager.Instance.projectileTravelTime).SetEase(Ease.OutSine).OnComplete(() => Destroy(p));       
    }

    private void Update()
    {
        if (transform.position.x < -5)
        {
            Destroy(gameObject);
            return;
        }

        if (isHited) return;

        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MonsterAtkCollider>() != null)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "monsterPart")
        {
            //Debug.Log("hit");
            //may hit
            var chance = Random.RandomRange(0, hitChance);
            if (chance < 1) StartCoroutine(HitCoroutine(Random.Range(0, 0.3f), collision.transform));
        }

    }

    IEnumerator HitCoroutine(float time, Transform hitPart)
    {
        yield return new WaitForSeconds(time);

        //change sprite
        mySpriteRendrer.sprite = spriteHited;
        isHited = true;
        transform.SetParent(hitPart, true);

        if (hitPart.GetComponent<MonsterPart>() != null)
            hitPart.GetComponent<MonsterPart>().GetDamage(damage, damageDelay, GameManager.Instance.projectileTravelTime, this);
        else if(hitPart.GetComponentInParent<MonsterPart>() != null)
            hitPart.GetComponentInParent<MonsterPart>().GetDamage(damage, damageDelay, GameManager.Instance.projectileTravelTime, this);
        else
            hitPart.GetComponentInChildren<MonsterPart>().GetDamage(damage, damageDelay, GameManager.Instance.projectileTravelTime, this);
    }
}
