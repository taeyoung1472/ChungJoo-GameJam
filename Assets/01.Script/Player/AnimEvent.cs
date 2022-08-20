using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;

    public void PlayFootStep()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(footsteps[Random.Range(0, footsteps.Length)]);
    }
}
