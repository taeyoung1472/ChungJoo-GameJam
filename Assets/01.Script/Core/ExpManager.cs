using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private List<int> expTable;
    int curExp;

    [Header("Calcul")]
    [SerializeField] private int upperValue = 10;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddExp(10);
        }
    }

    public void AddExp(int value)
    {
        curExp += value;
        if (curExp >= expTable[0])
        {
            int nextTossExp = curExp - expTable[0];
            curExp = nextTossExp;
            expTable.RemoveAt(0);
            UpgradeManager.Instance.Active();
        }
    }

    [ContextMenu("경험치 계산")]
    public void Calcul()
    {
        for (int i = 2; i < expTable.Count; i++)
        {
            expTable[i] = expTable[i - 1] + expTable[i - 1] - expTable[i - 2] + upperValue;
        }
    }
}
