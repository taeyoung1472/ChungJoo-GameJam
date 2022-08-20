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
        rb.velocity = transform.right * (bulletSpeed[GameManager.Instance.Data.bulletSpeedLevel] + (GameManager.Instance.Data.isFrogDogam ? 5 : 0));
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
            PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(impactClip, Random.Range(0.9f, 1.1f), 0.25f);
            PoolManager.Instance.Push(PoolType.Bullet, gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(damage[GameManager.Instance.Data.bulletDamage] * (GameManager.Instance.Data.isDamageUp ? 2 : 1) + (GameManager.Instance.Data.isDogDogam ? 10 : 0));
            if (GameManager.Instance.Data.bulletPoision == 1)
            {
                collision.GetComponent<Enemy>().Poision();
            }
            PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(impactClip, Random.Range(0.9f, 1.1f));
            PoolManager.Instance.Push(PoolType.Bullet, gameObject);
        }
    }
}
