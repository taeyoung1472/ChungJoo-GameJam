using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBullet : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 15);
    }

    void Update()
    {
        rb.velocity = transform.right * 5;
    }
}
