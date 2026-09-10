using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIController : MonoBehaviour
{    
    [SerializeField] private Canvas _hpBar;
    private Canvas hpBar;
    private Monster _state;

    private void Awake() => CacheComponents();
    private void Update()
    {        
        HPBar();
    }

    private void HPBar()
    {        
        hpBar.transform.position = new Vector3(transform.position.x, transform.position.y +0.3f, transform.position.z);
        Image[] redbar = hpBar.GetComponentsInChildren<Image>();
        redbar[1].fillAmount = ((float)_state._hp / (float)_state.MAX_HP);
    }
    
    private void CacheComponents()
    {
        hpBar =  Instantiate(_hpBar, gameObject.transform);
        _state = GetComponent<Monster>();
    }    
}
