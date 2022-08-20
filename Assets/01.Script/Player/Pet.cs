using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(30);
        }
    }
}
