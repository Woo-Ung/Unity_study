using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazine;
    [SerializeField] private Image _hpBar;
    private PlayerWeapon _weapon;
    private PlayerState _state;

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
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _state = GetComponent<PlayerState>();
    }

    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagzine}";
    }
}
