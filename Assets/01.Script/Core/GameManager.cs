using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleTon<GameManager>
{
    public Transform player;
    public JsonData Data;
    public void PlayGame()
    {
        Time.timeScale = 1;
        if (!JsonManager.Instance.Data.hasSawTrail)
        {
            SceneManager.LoadScene(2);
            JsonManager.Instance.Data.hasSawTrail = true;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    public void ReturnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetJson()
    {
        JsonManager.Instance.Data.hasSawTrail = false;
        JsonManager.Instance.Save();
    }

    internal void LoadEnding()
    {
        SceneManager.LoadScene(3);
    }

    private static System.Random random = new System.Random();
    public static T RandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }
}
