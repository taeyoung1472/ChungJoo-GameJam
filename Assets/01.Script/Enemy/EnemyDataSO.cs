using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public int number;
    public string enemyName;
    public int hp;
    public float speed;
    public int exp = 5;
    public float attackRange = 2;
    public int damage = 1;
    public float attackDelay;
    public string titleName;
    public string description;
    public Image enemyImage;
}
