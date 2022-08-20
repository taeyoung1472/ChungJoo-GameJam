using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;
    protected int curHp;
    protected bool isPoisioning = false;
    protected float curSpeed;

    protected bool isDie = false;

    float dissolve = 1;

    [SerializeField] protected EnemyDataSO data;
    [SerializeField] protected Transform slider;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected bool isRight;
    protected virtual void Start()
    {
        curSpeed = data.speed;
        curHp = data.hp;
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player;
        Begin();
        StartCoroutine(PoisionSystem());
        StartCoroutine(Attack());
    }

    public void GetDamage(int dmg)
    {
        if (isDie) return;
        curHp -= dmg;
        Popup(dmg, false);
        if (curHp <= 0)
        {
            if(Random.Range(0, 100) <= 90)
            {
                GameObject obj = PoolManager.Instance.Pop(PoolType.Item);
                obj.transform.position = transform.position;
            }
            slider.parent.gameObject.SetActive(false);
            isDie = true;
        }
        float value = (float)curHp / (float)data.hp;
        slider.localScale = new Vector3(value < 0 ? 0 : value, slider.localScale.y, slider.localScale.z);
    }

    public void Poision()
    {
        isPoisioning = true;
    }

    protected virtual IEnumerator Attack()
    {
        while (!isDie)
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, player.position) < data.attackRange + 1f);
            yield return new WaitForSeconds(0.2f);
            if(Vector3.Distance(transform.position, player.position) < data.attackRange + 1f)
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
        while (!isDie)
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
        if (isDie) return;
        PopupPoolObject popup = PoolManager.Instance.Pop(PoolType.PopupText).GetComponent<PopupPoolObject>();
        popup.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        popup.Active(value, false);
    }

    void Update()
    {
        Move();
        if (isDie)
        {
            dissolve -= Time.deltaTime;
            spriteRenderer.material.SetFloat("_Dissolve", dissolve);
            if(dissolve <= 0)
            {
                ExpManager.Instance.AddExp(10);
                Destroy(gameObject);
            }
        }
    }
    protected virtual void Move()
    {
        if (isDie)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = (player.position - transform.position).normalized * curSpeed;
        bool isFlip = player.position.x < transform.position.x;
        if (!isRight)
            isFlip = !isFlip;
        spriteRenderer.flipX = isFlip;
    }

    protected virtual void Begin()
    {

    }
}
