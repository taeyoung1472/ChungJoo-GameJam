using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluHumman : Enemy
{
    [SerializeField] private GameObject fog;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Fogging());
    }

    IEnumerator Fogging()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(fog, transform.position, Quaternion.identity);
        }
    }
}
