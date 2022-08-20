using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoSingleTon<ExpManager>
{
    [SerializeField] private List<int> expTable;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelTmp;
    int curExp;
    int maxLevel;

    [Header("Calcul")]
    [SerializeField] private int upperValue = 10;

    public void Start()
    {
        maxLevel = expTable.Count;
        AddExp(0);
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
        fillImage.fillAmount = (float)curExp / (float)expTable[0];
        levelTmp.text = $"집필 장수 : {maxLevel - expTable.Count}";
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
