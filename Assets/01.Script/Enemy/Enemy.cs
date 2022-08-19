using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    int curHp;
    bool isPoisioning = false;

    [SerializeField] private EnemyDataSO data;
    [SerializeField] private Transform slider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        curHp = data.hp;
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player;
        StartCoroutine(PoisionSystem());
    }

    public void GetDamage(int dmg)
    {
        curHp -= dmg;
        Popup(dmg, false);
        if (curHp <= 0)
        {
            print("Á×À½");
            Destroy(gameObject);
        }
        slider.localScale = new Vector3((float)curHp / (float)data.hp, slider.localScale.y, slider.localScale.z);
    }

    public void Poision()
    {
        isPoisioning = true;
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
        rb.velocity = (player.position - transform.position).normalized * data.speed;
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}
