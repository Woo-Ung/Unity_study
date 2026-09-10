using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int _hp { get; protected set; }

    [field: SerializeField] public int MAX_HP { get; protected set; }
    public GameObject GameObject => gameObject;

    public void Awake() => CacheComponents();

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CacheComponents()
    {
        _hp = MAX_HP;
        gameObject.layer = 7;
    }        
}
