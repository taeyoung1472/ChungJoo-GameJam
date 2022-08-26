using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(30);
            PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(hitClip, Random.Range(0.9f, 1.1f));
        }
    }
}
