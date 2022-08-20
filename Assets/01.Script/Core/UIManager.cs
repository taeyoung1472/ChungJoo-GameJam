using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoSingleTon<UIManager>
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;

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
}
