using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolAbleObject
{
    [SerializeField] private float[] bulletSpeed;
    [SerializeField] private int[] damage;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip impactClip;
    private Rigidbody2D rb;

    public void Active()
    {
        rb.velocity = transform.right * bulletSpeed[JsonManager.Instance.Data.bulletSpeedLevel];
    }

    public override void Init_Pop()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(fireClip, Random.Range(0.9f, 1.1f));
    }

    public override void Init_Push()
    {
        rb.velocity = Vector2.zero;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            PoolManager.Instance.Push(PoolType.Bullet, gameObject);
                    PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(impactClip, Random.Range(0.9f, 1.1f));
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(damage[JsonManager.Instance.Data.bulletDamage]);
            if (JsonManager.Instance.Data.bulletPoision == 1)
            {
                collision.GetComponent<Enemy>().Poision();
            }
            PoolManager.Instance.Push(PoolType.Bullet, gameObject);
        }
    }
}
