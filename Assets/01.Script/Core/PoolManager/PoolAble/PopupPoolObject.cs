using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class PopupPoolObject : PoolAbleObject
{
    TextMeshPro tmp;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color criticalColor;

    public override void Init_Pop()
    {
        if(tmp == null)
        {
            tmp = GetComponent<TextMeshPro>();
        }
    }

    public override void Init_Push()
    {
        tmp.text = "";
    }

    public void Active(int value, bool isCritical)
    {
        tmp.text = $"{value}";
        tmp.color = isCritical ? criticalColor : defaultColor;
        tmp.transform.DOJump(Random.insideUnitCircle + Vector2.up * 2 + new Vector2(transform.position.x, transform.position.y), 1, 1, 0.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(tmp.DOFade(0, 1));
        seq.AppendCallback(() => PoolManager.Instance.Push(PoolType.PopupText, gameObject));
    }
}
