using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator camAnimator;
    [SerializeField] private TextMeshPro tmp;

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
                    tmp.text = item.speedText;
                    SpeedUp();
                    break;
                case Item.ItemType.Damage:
                    tmp.text = item.damageText;
                    DamageUp();
                    break;
                case Item.ItemType.Hp:
                    tmp.text = item.hpText;
                    HpUp();
                    break;
                case Item.ItemType.Hide:
                    tmp.text = item.hideText;
                    break;
                case Item.ItemType.God:
                    tmp.text = item.godText;
                    GodMode();
                    break;
            }
            Sequence seq = DOTween.Sequence();
            seq.Append(tmp.DOFade(1, 0));
            seq.Append(tmp.DOFade(0, 2.5f));
            PoolManager.Instance.Push(PoolType.Item, item.gameObject);
        }
    }

    private void Buffing()
    {
        if(speedTimer > 0)
        {
            speedFixValue = 1.25f;
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
            camAnimator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
            camAnimator.SetBool("Move", false);
        }

        rb.velocity = new Vector3(h, v, 0).normalized * curSpeed * speedFixValue;
    }

    public void GetDamage(int value)
    {
        if(godTimer > 0) { return; }
        EffectManager.Instance.Shake(0.25f);
        EffectManager.Instance.ZoomOut(6);
        EffectManager.Instance.ActiveColorPanel(Color.red);
        hp -= value;
        HeartSystem.Instance.SetHeart(hp);
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
        hp += 2;
        hp = Mathf.Clamp(hp, 0, 12);
        HeartSystem.Instance.SetHeart(hp);
    }

    public void GodMode()
    {
        godTimer = 3f;
    }
}
