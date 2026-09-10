using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretColliderController : MonoBehaviour
{    
    private TurretScript _turretScript;   

    private void Awake() => CacheComponents();

    private void OnTriggerEnter(Collider other)
    {
        if (_turretScript.TargetLayer.Contains(other))
        {
            _turretScript.playerTransform(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_turretScript.TargetLayer.Contains(other))
        {
            _turretScript.playerTransform(other.transform);
        }
    }

    private void CacheComponents()
    {
        _turretScript = GetComponentInParent<TurretScript>();
    }    
}
