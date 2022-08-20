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
    [SerializeField] private int[] maxHp;
    [SerializeField] private SpriteRenderer spriteRenderer;

    float speedFixValue;

    [SerializeField] private float[] speedUpgradeFixValue;
    float curSpeed;
    float poisionTimer;
    float speedTimer;
    float damageTimer;
    float godTimer;
    float hideTimer;
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
        HeartSystem.Instance.SetHeart(hp, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
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
                    Hide();
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
        if (collision.CompareTag("EnemyBullet"))
        {
            GetDamage(1);
            Destroy(collision.gameObject);
        }
    }

    private void Hide()
    {
        hideTimer = 3f;
    }

    private void Buffing()
    {
        if(speedTimer > 0)
        {
            speedFixValue = 1.25f;
        }
        else
        {
            speedFixValue = 1;
        }

        if(damageTimer > 0)
        {
            GameManager.Instance.Data.isDamageUp = true;
        }
        else
        {
            GameManager.Instance.Data.isDamageUp = false;
        }

        if(hideTimer > 0)
        {
            GameManager.Instance.Data.isFreeze = true;
        }
        else
        {
            GameManager.Instance.Data.isFreeze = false;
        }

        hideTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;
        godTimer -= Time.deltaTime;
        damageTimer -= Time.deltaTime;
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

        if (h > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(h == 0)
        {
            // Nothing
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        rb.velocity = new Vector3(h, v, 0).normalized * ((curSpeed + ((GameManager.Instance.Data.isBoreDogam ? 0.5f : 0f) + (GameManager.Instance.Data.isHummanDogam ? 0.5f : 0f))) * speedFixValue) * speedUpgradeFixValue[GameManager.Instance.Data.speedLevel];
    }

    public void GetDamage(int value)
    {
        if(godTimer > 0) { return; }
        EffectManager.Instance.Shake(0.25f);
        EffectManager.Instance.ZoomOut(6);
        EffectManager.Instance.ActiveColorPanel(Color.red);
        hp -= value;
        if(hp <= 0)
        {
            Time.timeScale = 0;
            UIManager.Instance.ActiveDeadPanel();
        }
        HeartSystem.Instance.SetHeart(hp, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
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
        hp = Mathf.Clamp(hp, 0, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
        HeartSystem.Instance.SetHeart(hp, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
    }

    public void HpToMax()
    {
        hp += 20;
        hp = Mathf.Clamp(hp, 0, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
        HeartSystem.Instance.SetHeart(hp, maxHp[GameManager.Instance.Data.hpLevel] + (GameManager.Instance.Data.isCowDogam ? 2 : 0));
    }

    public void GodMode()
    {
        godTimer = 3f;
    }
}
