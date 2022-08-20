using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoSingleTon<HeartSystem>
{
    [SerializeField] private GameObject[] hearts;
    public void SetHeart(int value)
    {
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
