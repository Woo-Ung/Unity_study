using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable
{   
    private int _damage;
    private float _speed;
    private float _returnDealy;
    private bool _isReturn = false;

    public ObjectPool Pool { get; set; }
    public Transform tr { get => transform; }
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
            other.GetComponent<IDamageable>().TakeDamage(10);
        }

        Pool.Return(this);
    }

    private void Update()
    {        
        MoveForward();
        SetReturnToPool();
    }
    public void SetReturnToPool()
    {
        if (!_isReturn)
        {
            StartCoroutine(ReturnToPool());
        }
    }

    public IEnumerator ReturnToPool()
    {
        _isReturn = true;
        yield return new WaitForSeconds(_returnDealy);
        Pool.Return(this);
        _isReturn = false;
    }

    //public void ReturnToPool()
    //{
    //    // 제한시간이 경과할 것.
    //    if (_elapsedTime >= _returnDealy)
    //    {
    //        // 자신이 속한 풀에 대한 참조

    //        // 풀 내부적으로 다시 오브젝트를 넣어놓는 기능
    //        _elapsedTime = 0;
    //        Pool.Return(this);
    //    }
    //}

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }        

    public void SetData(int damage, float speed, float returnDelay)
    {
        _damage = damage;
        _speed = speed;
        _returnDealy = returnDelay;
    }
}