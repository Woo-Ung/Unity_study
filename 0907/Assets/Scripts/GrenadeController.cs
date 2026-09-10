using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class GrenadeController : MonoBehaviour
{       
    [SerializeField] private float _shootPower;
    [SerializeField] private FlameEffect _flameEffect;
    private Transform _shootDirect;
    private Rigidbody _rigidbody; 
    private float _chargingTime;

    public LayerMask TargetLayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _chargingTime = 0;
    }

    private void Start()
    {
        GrenadeShoot();
    }
       
    public void SetGrenade(float elapsedTime, Transform shootDirect)
    {
        _chargingTime = elapsedTime;
        _shootDirect = shootDirect;
    }

    public void GrenadeShoot()
    {        
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(_shootDirect.forward * (_chargingTime * 1.4f) * _shootPower + _shootDirect.up * (_shootPower/2), ForceMode.Impulse);        
        Destroy(gameObject, 3);
    }

    public void OnDestroy()
    {
        FlameEffect boomEffetct = Instantiate(_flameEffect, transform.position, _flameEffect.transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);        
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.transform.TryGetComponent(out IDamageable iDamageable) && TargetLayer.Contains(collider))
                {
                    collider.GetComponent<IDamageable>().TakeDamage(50);
                }
            }
        }
    }
}