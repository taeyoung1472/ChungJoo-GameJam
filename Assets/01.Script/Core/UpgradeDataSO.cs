using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Upgrade")]
public class UpgradeDataSO : ScriptableObject
{
    public Sprite profile;
    public string upgradeName;
    public string upgradeDesc;
    public UpgradeType upgradeType;
    public enum UpgradeType
    {
        FireDelay,
        MultiShoot,
        MultiBullet,
        BulletDamage,
        BulletSpeed,
        BulletPoision,
        Ora,
        Pet,
        Hp,
        Speed,
        MAX
    }
}