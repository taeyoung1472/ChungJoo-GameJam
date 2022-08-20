using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoSingleTon<UpgradeManager>
{
    [SerializeField] private UpgradeUI[] upgradeUIs;
    [SerializeField] private List<UpgradeData> upgradeDataList;
    
    [ContextMenu("¤·¤·¤·")]
    public void Active()
    {
        UpgradeDataSO[] generatedDatas = GenerateUpgradeDatas();

        for (int i = 0; i < 3; i++)
        {
            upgradeUIs[i].upgradeTitle.text = generatedDatas[i].upgradeName;
            upgradeUIs[i].upgradeDesc.text = generatedDatas[i].upgradeDesc;
        }
    }

    UpgradeDataSO[] GenerateUpgradeDatas()
    {
        List<UpgradeDataSO> returnDatas = new List<UpgradeDataSO>();

        while (returnDatas.Count != 3)
        {
            int randIdx = Random.Range(0, upgradeDataList.Count - 1);
            UpgradeDataSO dt = upgradeDataList[randIdx].data;
            if (returnDatas.Contains(dt))
            {
                continue;
            }
            else
            {
                returnDatas.Add(dt);
                upgradeDataList[randIdx].count--;
                if(upgradeDataList[randIdx].count <= 0)
                {
                    upgradeDataList.Remove(upgradeDataList[randIdx]);
                }
            }
        }
        return returnDatas.ToArray();
    }

    [System.Serializable]
    public class UpgradeData
    {
        public UpgradeDataSO data;
        public int count;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public Image iconImage;
        public TextMeshProUGUI upgradeTitle;
        public TextMeshProUGUI upgradeDesc;
    }
}
