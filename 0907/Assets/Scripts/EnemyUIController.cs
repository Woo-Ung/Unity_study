using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIController : MonoBehaviour
{    
    [SerializeField] private Canvas _hpBar;
    [SerializeField] private float _hpYPosition;
    private Transform _playerTransform;
    private Canvas hpBar;
    private Monster _state;

    private void Awake() => CacheComponents();
    private void Update()
    {        
        HPBar();
    }

    private void HPBar()
    {        
        hpBar.transform.position = new Vector3(transform.position.x, transform.position.y + _hpYPosition, transform.position.z);
        Vector3 look = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z);
        hpBar.transform.LookAt(look);
        Image[] redbar = hpBar.GetComponentsInChildren<Image>();
        redbar[1].fillAmount = ((float)_state._hp / (float)_state.MAX_HP);
    }
    
    private void CacheComponents()
    {
        _playerTransform = GameObject.Find("Player").transform;
        hpBar =  Instantiate(_hpBar, gameObject.transform);
        _state = GetComponent<Monster>();
    }    
}
