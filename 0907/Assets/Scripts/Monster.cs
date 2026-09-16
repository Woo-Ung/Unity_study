using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int _hp { get; protected set; }

    [field: SerializeField] public int MAX_HP { get; protected set; }
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

    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void CacheComponents()
    {
        _hp = MAX_HP;
        gameObject.layer = 7;
    }        

    public void CleanUp()
    {
        _gameObject = null;
    }
}