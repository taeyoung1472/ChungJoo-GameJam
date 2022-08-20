using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PoolAbleObject
{
    [HideInInspector] public ItemType type;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public Sprite speed;
    public Sprite damage;
    public Sprite hp;
    public Sprite hide;
    public Sprite god;

    public override void Init_Pop()
    {
        type = GameManager.RandomEnum<ItemType>();
        switch (type)
        {
            case ItemType.Speed:
                spriteRenderer.sprite = speed;
                break;
            case ItemType.Damage:
                spriteRenderer.sprite = damage;
                break;
            case ItemType.Hp:
                spriteRenderer.sprite = hp;
                break;
            case ItemType.Hide:
                spriteRenderer.sprite = hide;
                break;
            case ItemType.God:
                spriteRenderer.sprite = god;
                break;
            default:
                break;
        }
    }

    public override void Init_Push()
    {

    }

    public enum ItemType
    {
        Speed,
        Damage,
        Hp,
        Hide,
        God,
        Length
    }
}
