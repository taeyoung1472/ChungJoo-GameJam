using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoSingleTon<UpgradeManager>
{
    [SerializeField] private UpgradeUI[] upgradeUIs;
    [SerializeField] private List<UpgradeData> upgradeDataList;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private UpgradeDataSO max;

    public void Active()
    {
        Time.timeScale = 0;
        upgradePanel.SetActive(true);

        Action[] ac = new Action[3];

        UpgradeDataSO[] generatedDatas = GenerateUpgradeDatas(ref ac);

        for (int i = 0; i < 3; i++)
        {
            UpgradeDataSO.UpgradeType upgradeType = generatedDatas[i].upgradeType;

            #region 버튼 처리
            upgradeUIs[i].upgradeButton.onClick.RemoveAllListeners();
            if(generatedDatas[i].upgradeType != UpgradeDataSO.UpgradeType.MAX)
            {
                int t = i;
                upgradeUIs[i].upgradeButton.onClick.AddListener(() => Upgrade(upgradeType));
                upgradeUIs[i].upgradeButton.onClick.AddListener(() => ac[t]?.Invoke());
                upgradeUIs[i].upgradeButton.onClick.AddListener(() => upgradePanel.SetActive(false));
                upgradeUIs[i].upgradeButton.onClick.AddListener(() => Time.timeScale = 1);
            }
            #endregion

            upgradeUIs[i].upgradeTitle.text = generatedDatas[i].upgradeName;
            upgradeUIs[i].upgradeDesc.text = generatedDatas[i].upgradeDesc;
        }
    }

    private void Upgrade(UpgradeDataSO.UpgradeType type)
    {
        switch (type)
        {
            case UpgradeDataSO.UpgradeType.FireDelay:
                JsonManager.Instance.Data.fireDelayLevel++;
                break;
            case UpgradeDataSO.UpgradeType.MultiShoot:
                JsonManager.Instance.Data.multiFireLevel++;
                break;
            case UpgradeDataSO.UpgradeType.MultiBullet:
                JsonManager.Instance.Data.multiBullet++;
                break;
            case UpgradeDataSO.UpgradeType.BulletDamage:
                JsonManager.Instance.Data.bulletDamage++;
                break;
            case UpgradeDataSO.UpgradeType.BulletSpeed:
                JsonManager.Instance.Data.bulletSpeedLevel++;
                break;
            case UpgradeDataSO.UpgradeType.BulletPoision:
                JsonManager.Instance.Data.bulletPoision++;
                break;
            case UpgradeDataSO.UpgradeType.Ora:
                JsonManager.Instance.Data.petLevel++;
                break;
            case UpgradeDataSO.UpgradeType.Pet:
                JsonManager.Instance.Data.smallPetLevel++;
                break;
        }
    }

    UpgradeDataSO[] GenerateUpgradeDatas(ref Action[] ac)
    {
        List<UpgradeDataSO> returnDatas = new List<UpgradeDataSO>();

        if(upgradeDataList.Count < 3)
        {
            int idx = 0;
            foreach (var dataList in upgradeDataList)
            {
                idx++;
                returnDatas.Add(dataList.data);
                ac[idx] = () => { upgradeDataList[idx].count--; };
            }
            for (int i = idx; i <= 3; i++)
            {
                returnDatas.Add(max);
            }
            return returnDatas.ToArray();
        }
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
                ac[returnDatas.Count] = () => { upgradeDataList[randIdx].count--; };
                returnDatas.Add(dt);
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
        public Button upgradeButton;
        public TextMeshProUGUI upgradeTitle;
        public TextMeshProUGUI upgradeDesc;
    }
}
