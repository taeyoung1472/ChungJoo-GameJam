using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float[] fireDelay;
    [SerializeField] private FirePos[] firePos;
    Camera cam;
    bool isControllFirePos = true;
    public void Start()
    {
        cam = Camera.main;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (!isControllFirePos) { return; }
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));

            isControllFirePos = false;
            for (int i = 0; i < JsonManager.Instance.Data.multiBullet + 1; i++)
            {
                foreach (var pos in firePos[JsonManager.Instance.Data.multiFireLevel].firePos)
                {
                    Bullet bullet = PoolManager.Instance.Pop(PoolType.Bullet).GetComponent<Bullet>();
                    bullet.transform.SetPositionAndRotation(pos.position, pos.rotation);
                    bullet.Active();
                }
                yield return new WaitForSeconds(0.1f);
            }
            isControllFirePos = true;

            yield return new WaitForSeconds(fireDelay[JsonManager.Instance.Data.fireDelayLevel]);
        }
    }

    [System.Serializable]
    public class FirePos
    {
        public Transform[] firePos;
    }
}
