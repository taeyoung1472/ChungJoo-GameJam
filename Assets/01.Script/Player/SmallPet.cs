using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPet : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform firePos;
    public void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    public void Update()
    {
        transform.position = followTarget.position;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));
            Bullet bullet = PoolManager.Instance.Pop(PoolType.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
            bullet.Active();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
