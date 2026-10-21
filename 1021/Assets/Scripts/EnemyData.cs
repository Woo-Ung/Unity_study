using UnityEngine;
using System;

[Serializable]
public class EnemyData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackValue;

    public int ID => _id;
    public string Name => _name;
    public int MaxHP => _maxHP;
    public float DetectRange => _detectRange;
    public float AttackSpeed => _attackSpeed;
    public float Speed => _speed;
    public int AttackValue => _attackValue;

    public EnemyData(int id, string name, int maxHp, float detectRange, float attackSpeed, float speed, int attackValue)
    {
        _id = id;
        _name = name;
        _maxHP = maxHp;
        _detectRange = detectRange;
        _attackSpeed = attackSpeed;
        _speed = speed;
        _attackValue = attackValue;
    }

}
