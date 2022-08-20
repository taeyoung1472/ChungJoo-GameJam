using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Enemy
{
    [SerializeField] private float StopTime = 1f;

    private bool isDash = false;

    protected override void Begin()
    {
        StartCoroutine("DashOn");
    }

    protected override void Move()
    {
        if (!isDash)
        {
            base.Move();
        }
    }


    IEnumerator DashOn()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine("CowDash");
        }
    }

    IEnumerator CowDash()
    {
        isDash = true;

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(StopTime);

        rb.velocity = (player.position - transform.position).normalized * curSpeed * 5f;

        yield return new WaitForSeconds(1f);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(StopTime);

        isDash = false;
    }
}
