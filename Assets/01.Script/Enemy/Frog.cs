using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private GameObject bullet;
    protected override void Move()
    {
        if (isDie)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector3 dir = (player.position - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < data.attackRange)
        {
            rb.velocity = -dir * data.speed;
        }
        else
        {
            rb.velocity = dir * data.speed;
        }

        bool isFlip = player.position.x < transform.position.x;
        if (!isRight)
            isFlip = !isFlip;
        spriteRenderer.flipX = isFlip;
    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            GameObject bul = Instantiate(bullet);
            float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
            bul.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(data.attackDelay);
        }
    }
}
