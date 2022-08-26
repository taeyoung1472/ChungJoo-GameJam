using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip perspectiveClip;
    public AudioClip negativeClip;
    public void OnClick()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(perspectiveClip, Random.Range(0.9f, 1.1f));
    }
    public void OnClick_Exit()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(negativeClip, Random.Range(0.9f, 1.1f));
    }
}