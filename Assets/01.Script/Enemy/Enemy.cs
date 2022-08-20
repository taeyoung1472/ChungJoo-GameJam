using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    int curHp;
    bool isPoisioning = false;
    float curSpeed;

    [SerializeField] private EnemyDataSO data;
    [SerializeField] private Transform slider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        curSpeed = data.speed;
        curHp = data.hp;
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player;
        StartCoroutine(PoisionSystem());
        StartCoroutine(Attack());
    }

    public void GetDamage(int dmg)
    {
        curHp -= dmg;
        Popup(dmg, false);
        if (curHp <= 0)
        {
            ExpManager.Instance.AddExp(10);
            Destroy(gameObject);
        }
        slider.localScale = new Vector3((float)curHp / (float)data.hp, slider.localScale.y, slider.localScale.z);
    }

    public void Poision()
    {
        isPoisioning = true;
    }

    protected virtual IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, player.position) < data.attackRange + 0.5f);
            yield return new WaitForSeconds(0.2f);
            if(Vector3.Distance(transform.position, player.position) < data.attackRange + 0.5f)
            {
                player.GetComponent<PlayerController>().GetDamage(data.damage);
                curSpeed = data.speed * 0.25f;
                yield return new WaitForSeconds(data.attackDelay);
                curSpeed = data.speed;
            }
        }
    }

    IEnumerator PoisionSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => isPoisioning);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1f);
                GetDamage(1);
            }
            isPoisioning = false;
        }
    }

    void Popup(int value, bool isCritical)
    {
        PopupPoolObject popup = PoolManager.Instance.Pop(PoolType.PopupText).GetComponent<PopupPoolObject>();
        popup.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        popup.Active(value, false);
    }

    void Update()
    {
        rb.velocity = (player.position - transform.position).normalized * curSpeed;
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}
