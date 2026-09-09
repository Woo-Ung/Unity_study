using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{   
    private int _damage;
    private float _speed;

    public LayerMask TargetLayer;

    private void Awake()
    {
        SetTargetLayer();
    }

    private void SetTargetLayer()
    {
        TargetLayer = 1 << 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TargetLayer.Contains(other))
        {
            Debug.Log("플레이어 맞음");
        }

        Destroy(gameObject);
    }

    private void Update() => MoveForward();

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void SetData(int damage, float speed, float destroyDelay)
    {
        _damage = damage;
        _speed = speed;

        Destroy(gameObject, destroyDelay);
    }

}
