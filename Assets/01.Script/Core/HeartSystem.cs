using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoSingleTon<HeartSystem>
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject[] heartBackground;
    public void SetHeart(int value, int maxValue)
    {
        for (int i = 0; i < Mathf.RoundToInt(maxValue * 0.5f); i++)
        {
            heartBackground[i].SetActive(true);
        }
        for (int i = Mathf.RoundToInt(maxValue * 0.5f); i < 6; i++)
        {
            heartBackground[i].SetActive(false);
        }
        if(value < 0)
        {
            return;
        }
        for (int i = 0; i < value; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int i = value; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
        }
    }
}
