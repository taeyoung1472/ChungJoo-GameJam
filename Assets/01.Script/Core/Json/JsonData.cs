using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class JsonData
{
    [Range(0, 3)] public int fireDelayLevel;
    [Range(0, 2)] public int multiFireLevel;
    [Range(0, 2)] public int multiBullet;
    [Range(0, 3)] public int bulletSpeedLevel;
    [Range(0, 1)] public int bulletPoision;
    [Range(0, 1)] public int petLevel;
    [Range(0, 1)] public int smallPetLevel;
    [Range(0, 2)] public int bulletDamage;
    public bool isDamageUp;
}