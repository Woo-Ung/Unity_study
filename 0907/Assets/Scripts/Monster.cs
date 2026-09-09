using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    public GameObject GameObject => gameObject;

    public void Awake() => SetLayer();

    public void TakeDamage(int damage)
    {
        Debug.Log($"{GameObject.name}은 {damage}데미지를 입었다.");
    }

    public void SetLayer()
    {
        gameObject.layer = 7;
    }
}
