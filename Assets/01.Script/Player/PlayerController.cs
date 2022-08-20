using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private Animator animator;

    float speedFixValue;

    float curSpeed;
    float poisionTimer;
    float speedTimer;
    float damageTimer;
    float godTimer;
    Rigidbody2D rb;
    void Start()
    {
        curSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        Poisioning();
        Buffing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            switch (item.type)
            {
                case Item.ItemType.Speed:
                    SpeedUp();
                    break;
                case Item.ItemType.Damage:
                    DamageUp();
                    break;
                case Item.ItemType.Hp:
                    HpUp();
                    break;
                case Item.ItemType.Hide:
                    break;
                case Item.ItemType.God:
                    GodMode();
                    break;
            }
            PoolManager.Instance.Push(PoolType.Item, item.gameObject);
        }
    }

    private void Buffing()
    {
        if(speedTimer > 0)
        {
            speedFixValue = 2;
            speedTimer -= Time.deltaTime;
        }
        else
        {
            speedFixValue = 1;
        }

        if(damageTimer > 0)
        {
            JsonManager.Instance.Data.isDamageUp = true;
            damageTimer -= Time.deltaTime;
        }
        else
        {
            JsonManager.Instance.Data.isDamageUp = false;
        }

        godTimer -= Time.deltaTime;
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        rb.velocity = new Vector3(h, v, 0).normalized * curSpeed;
    }

    public void GetDamage(int value)
    {
        if(godTimer > 0) { return; }
        hp -= value;
    }

    public void Poisioning()
    {
        if(poisionTimer > 0)
        {
            curSpeed = speed * 0.25f;
        }
        else
        {
            curSpeed = speed;
        }
        poisionTimer -= Time.deltaTime;
    }

    public void Poision()
    {
        poisionTimer = 1f;
    }

    public void SpeedUp()
    {
        speedTimer = 10;
    }

    public void DamageUp()
    {
        damageTimer = 10;
    }

    public void HpUp()
    {
        hp += 1;
    }

    public void GodMode()
    {
        godTimer = 3f;
    }
}
