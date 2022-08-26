using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleTon<UIManager>
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject dogamPanel;
    [SerializeField] private EnemyDate[] enemyDates;
    [SerializeField] private ExplanePanel explanePan;

    bool isActivePause;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivePause();
        }
    }

    public void ActivePause()
    {
        if (isActivePause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        isActivePause = !isActivePause;
        pausePanel.SetActive(isActivePause);
    }
    
    public void ActiveDeadPanel()
    {
        deadPanel.SetActive(true);
    }

    public void ActiveDogam(bool enabled)
    {
        dogamPanel.SetActive(enabled);
    }

    public void DeactivateExplane()
    {
        if (!dogamPanel.activeSelf)
            Time.timeScale = 1;

        explanePan.explanePanel.SetActive(false);
    }

    public void DeathCount(int enemyNum)
    {
        enemyDates[enemyNum].count++;
        if (enemyDates[enemyNum].count == 10)
        {
            switch (enemyNum)
            {
                case 0:
                    GameManager.Instance.Data.isDogDogam = true;
                    break;
                case 1:
                    GameManager.Instance.Data.isBoreDogam = true;
                    break;
                case 2:
                    GameManager.Instance.Data.isCowDogam = true;
                    break;
                case 3:
                    GameManager.Instance.Data.isFrogDogam = true;
                    break;
                case 4:
                    GameManager.Instance.Data.isHummanDogam = true;
                    break;
            }
            ActiveExplane(enemyNum);
            DogamManager.Instance.ActiveDogam(enemyNum);
        }
    }

    public void ActiveExplane(int enemyNum)
    {
        explanePan.iconImage.sprite = enemyDates[enemyNum].enemyData.enemyImage;
        explanePan.explaneTitle.text = enemyDates[enemyNum].enemyData.titleName;
        explanePan.explaneDesc.text = enemyDates[enemyNum].enemyData.description;

        Time.timeScale = 0;

        explanePan.explanePanel.SetActive(true);
    }

    [System.Serializable]
    public class EnemyDate
    {
        public EnemyDataSO enemyData;
        public int count = 0;
    }

    [System.Serializable]
    public class ExplanePanel
    {
        public GameObject explanePanel;
        public Image iconImage;
        public TextMeshProUGUI explaneTitle;
        public TextMeshProUGUI explaneDesc;
    }
}
