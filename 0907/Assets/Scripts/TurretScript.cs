using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : Monster
{
    [SerializeField] private ObjectPool _bulletpool;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private FlameEffect _boomEffect;

    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _returnDelay;

    public LayerMask TargetLayer;
    private Transform _playerTransform;
    private SphereCollider _sphereCollider;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInSight = false;
    private bool _isShoot = false;
    

    //--엔진 매서드
    private void Awake() => CacheComponents();
    private void Update()
    {        
        RayShotToPlayer(); // 코루틴수정ㄱ
        Rotate();
        FireMode();
    }

    public void playerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void CacheComponents()
    {
        base.CacheComponents();
        _sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    private void SpawnBullet()
    {
        // 1. 얻어오기
        IPoolable bullet = _bulletpool.Take();

        // 2. Transform.position, rotation
        bullet.tr.position = _muzzlePoint.position;
        bullet.tr.rotation = _muzzlePoint.rotation;

        // 3. 활성화
        bullet.tr.gameObject.SetActive(true);
       
        (bullet as BulletController).SetData(_bulletDamage, _bulletSpeed, _returnDelay);
    }

    private void Rotate()
    {
        if(_isPlayerInSight)
        {
            return;
        }        
        _headTransform.Rotate(Vector3.up,_rotateSpeed * Time.deltaTime);
    }
    private IEnumerator Fire()
    {
        _isShoot = true;
        yield return new WaitForSeconds(_cooldown);
        SpawnBullet();
        _isShoot = false;
    }
    private void FireMode()
    {
        if(!_isPlayerInSight || !_isPlayerInTrigger)
        {
            return;
        }

        Vector3 look = new Vector3(_playerTransform.position.x, _headTransform.position.y, _playerTransform.position.z);
        _headTransform.LookAt(look);

        if (!_isShoot)
        {
            StartCoroutine(Fire());
        }
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
    public override void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
            FlameEffect boom = Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);
        }
    }
}
