using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{  
    [SerializeField] private PlayerState _playerState;  
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletImpactEffectPrefab;
    [SerializeField] private int _currentMagazine;
    [SerializeField] private float _reloadDelay;
    private Transform _cameraTransform;
    public int CurrentMagazine => _currentMagazine;
    public int MaxMagzine => MAX_MAGAZINE;

    private const int MAX_MAGAZINE = 30;

    private bool _isReadyFire = true;
    private bool _isNeedReload => _currentMagazine <= 0;
    private bool _isReloading;


    private void Awake() => CacheComponent();
    private void Start() => Init(); 

    public void SetCooldown(float cooldown)
    {
        _playerState._weaponCooldown = cooldown;
    }

    public void Fire()
    {       
        if (!_isReadyFire || _isReloading)
        {
            return;
        }

        if(_isNeedReload)
        {
            Debug.Log("총알을 장전해주세요");
            return;
        }
        
        _currentMagazine--;
        
        StartCoroutine(UpdateCurrentCooldown());
        PlayFlameEffect();

        if (!TryGetDamageale(out IDamageable damageable))
        {
            return;
        }

        damageable.TakeDamage(_playerState._weaponDamage);               
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

        if (Physics.Raycast(ray, out hit, _playerState._weaponRange))
        {
            PlayBulletImpactEffect(hit);
            result = hit.transform.TryGetComponent(out damageable);
        }
        return result;
    }

    public void Reload()
    {
        if (_isReloading)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());      
    }

    public IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadDelay);
        _currentMagazine = MAX_MAGAZINE;      
        _isReloading = false;
    }

    public IEnumerator UpdateCurrentCooldown()
    {
        _isReadyFire = false;
        yield return new WaitForSeconds(_playerState._weaponCooldown);
        _isReadyFire = true;
    }             

    private void CacheComponent()
    {
        _playerState = GetComponentInParent<PlayerState>();
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {        
        _currentMagazine = MAX_MAGAZINE;
    }
}