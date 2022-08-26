using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoisionFrog : MonoBehaviour
{
    bool isActive;
    public void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(0.25f);
        seq.AppendCallback(() => isActive = true);
        seq.AppendInterval(1.5f);
        seq.AppendCallback(() => isActive = false);
        seq.AppendInterval(0.25f);
        seq.AppendInterval(5);
        seq.AppendCallback(() => Destroy(gameObject));
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActive) { return; }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Poision();
        }
    }
}
