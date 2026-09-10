using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Raycast -> IDamageable
    private Transform _cameraTransform; 

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletImpactEffectPrefab;

    [SerializeField] private int _currentMagazine;

    public int CurrentMagazine => _currentMagazine;
    public int MaxMagzine => MAX_MAGAZINE;

    private const int MAX_MAGAZINE = 30;
    private float _currentCooldown;

    private bool _isPressedFire => Input.GetKeyDown(_fireKey);
    
    private bool _isReadyFire => _currentCooldown >= _cooldown;
    private bool _isNeedReload => _currentMagazine <= 0;

    private void Awake() => CacheComponent();
    private void Start() => Init();
    private void Update() => UpdateCurrentCooldown();

    public void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
    }

    public void Fire()
    {       
        if (!_isPressedFire || !_isReadyFire)
        {
            return;
        }

        if(_isNeedReload)
        {
            Debug.Log("총알을 장전해주세요");
            return;
        }
        
        _currentMagazine--;
        _currentCooldown = 0f;
        PlayFlameEffect();

        if (!TryGetDamageale(out IDamageable damageable))
        {
            return;
        }

        damageable.TakeDamage(_damage);               
    }

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void PlayBulletImpactEffect(RaycastHit hit)
    {
        Transform effectTransform = Instantiate(_bulletImpactEffectPrefab).transform;
        effectTransform.position = hit.point;
        effectTransform.forward = hit.normal;        
    }

    private bool TryGetDamageale(out IDamageable damageable)
    {
        bool result = false;
        damageable = null;

        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _range))
        {
            PlayBulletImpactEffect(hit);
            result = hit.transform.TryGetComponent(out damageable);
        }
        return result;
    }

    public void Reload()
    {
        if (Input.GetKeyDown(_reloadKey))
        {
            _currentMagazine = MAX_MAGAZINE;
        }
    }

    public void UpdateCurrentCooldown()
    {        
        if(_isReadyFire)
        {
            return;
        }
        _currentCooldown += Time.deltaTime;        
    }          

    private void CacheComponent()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {
        _currentCooldown = 0f;
        _currentMagazine = MAX_MAGAZINE;
    }
}