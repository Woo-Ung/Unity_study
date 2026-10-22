using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class EnemyList
{
    [SerializeField] private List<EnemyData> _enemies;

    public List<EnemyData> Enemies => _enemies;

    public EnemyList(List<EnemyData> enemies)
    {
        _enemies = enemies;
    }
}
