using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "Data/MonsterStats")]
public class MonsterStats : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackValue;

    public int ID => _id;
    public string Name => _name;
    public int MaxHP => _maxHP;
    public float DetectRange => _detectRange;
    public float AttackRange => _attackRange;
    public float Speed => _speed;
    public int AttackValue => _attackValue;

    public void SetData(int id, string name, int maxHp, float detectRange, float attackRange, float speed, int attackValue)
    {
        _id = id;
        _name = name;
        _maxHP = maxHp;
        _detectRange = detectRange;
        _attackRange = attackRange;
        _speed = speed;
        _attackValue = attackValue;
    }
}
