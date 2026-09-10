using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private FlameEffect _boomEffect;

    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyDelay;

    public LayerMask TargetLayer;

    private float _currentCooldown;
    private Transform _playerTransform;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInSight = false;   
    private bool _isReadyToFire { get { return _currentCooldown >= _cooldown; } }
    private SphereCollider _sphereCollider;

    //--엔진 매서드
    private void Awake() => CacheComponents();
        private void Update()
    {
        UpdateCurrentCooldown();
        RayShotToPlayer();
        Rotate();
        Fire();
    }

    public void playerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void CacheComponents()
    {
        _sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    private void UpdateCurrentCooldown()
    {
        if(_isReadyToFire)
        {
            return;
        }

        _currentCooldown += Time.deltaTime;
    }

    private void SpawnBullet()
    {
        BulletController bullet = Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        bullet.SetData(_bulletDamage, _bulletSpeed, _bulletDestroyDelay);
    }

    private void Rotate()
    {
        if(_isPlayerInSight)
        {
            return;
        }        
        _headTransform.Rotate(Vector3.up,_rotateSpeed * Time.deltaTime);
    }
    private void Fire()
    {
        if(!_isPlayerInSight || !_isPlayerInTrigger)
        {
            return;
        }

        Vector3 look = new Vector3(_playerTransform.position.x, _headTransform.position.y, _playerTransform.position.z);
        _headTransform.LookAt(look);

        if(!_isReadyToFire)
        {
            return;
        }

        SpawnBullet();

        _currentCooldown = 0f;
    }

    private void RayShotToPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger)
        {
            return;
        }

        Vector3 from = new Vector3(transform.position.x,transform.position.y + _muzzlePoint.position.y / 2, transform.position.z);
        Vector3 to = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _muzzlePoint.position.y / 2, _playerTransform.position.z);

        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _sphereCollider.radius))
        {            
            if (TargetLayer.Contains(hit.collider))
            {
                _isPlayerInSight = true;
                Debug.Log("플레이어 감지");
            }
        }
        else
        {
            _isPlayerInSight = false;
        }
    }
    private void OnDestroy()
    {
        FlameEffect boom = Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);
    }
}
