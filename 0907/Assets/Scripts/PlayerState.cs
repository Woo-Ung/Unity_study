using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int _hp { get; protected set; }

    private const int MAX_HP = 100;

    public int MaxHp = MAX_HP;

    public GameObject GameObject => gameObject;

    private void Awake() => CacheComponents();

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CacheComponents()
    {
        _hp = MAX_HP;
    }
}