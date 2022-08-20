using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public int hp;
    public float speed;
}
