using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazine;
    [SerializeField] private TextMeshProUGUI _grenade;
    [SerializeField] private Image _hpBar;
    
    [field: SerializeField] public Canvas _scope { get; protected set; }
    
    private PlayerWeapon _weapon;
    private PlayerState _state;
    private PlayerController _player;
    public Image _scope1 { get; protected set; }
    public Image _scope2 { get; protected set; }

    private void Awake() => CacheComponents();
    private void Update()
    {
        RefreshMagazineUI();
        HPBar();
    }

    private void HPBar()
    {
        _hpBar.fillAmount = ((float)_state._hp / (float)_state.MaxHp);
    }

    private void CacheComponents()
    {
        Image[] scope = _scope.GetComponentsInChildren<Image>();
        _scope1 = scope[0];
        _scope2 = scope[1];
        _scope1.gameObject.SetActive(true);
        _scope2.gameObject.SetActive(false);
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _state = GetComponent<PlayerState>();
        _player = GetComponent<PlayerController>();
    }

    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagzine}";
        _grenade.text = $"{_player._grenade_num} / {_player._grenade_MaxNum}";
    }
}