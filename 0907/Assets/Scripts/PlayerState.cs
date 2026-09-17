using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IDamageable
{
    //---- controller
    [field: SerializeField] public float _detectionRange { get; set; }
    [field: SerializeField] public int _grenadeMaxNum { get; set; }
    [field: SerializeField] public int _grenadeNum { get; set; }
    [field: SerializeField] public float _groundDistance { get; set; }

    //---- movement
    [field: SerializeField] public float _moveSpeed { get; set; }
    [field: SerializeField] public float _jumpSpeed { get; set; }

    //---- Weapon
    [field: SerializeField] public float _weaponCooldown { get; set; }
    [field: SerializeField] public float _weaponRange { get; set; }
    [field: SerializeField] public int _weaponDamage { get; set; }

    // ------ 기존
    [field: SerializeField] public int _hp { get; set; }
    [field: SerializeField] public int MaxHp { get; set; }

    public event Action<int> HPChanged;

    private GameObject _gameObject;
    public GameObject GameObject
    {
        get
        {
            _gameObject = gameObject;
            return _gameObject;
        }
    }

    private void Awake() => CacheComponents();
    private void OnDestroy() => CleanUp();

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        HPChanged?.Invoke(_hp);

        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CacheComponents()
    {
        _hp = MaxHp;
        _grenadeNum = _grenadeMaxNum;
    }

    public void CleanUp()
    {
        _gameObject = null;
    }
}