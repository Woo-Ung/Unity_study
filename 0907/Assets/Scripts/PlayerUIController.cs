using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TextMeshProUGUI _magazine;
    [SerializeField] private TextMeshProUGUI _grenade;
    [SerializeField] private Image _hpBar;    
    [field: SerializeField] public Canvas _scope { get; protected set; }

    private PlayerWeapon _weapon;

    public Image _scope1 { get; protected set; }
    public Image _scope2 { get; protected set; }

    private void Awake() => CacheComponents();

    private void OnEnable() => BindInputActions();
    private void Start() => RefreshMagazineUI();
    private void OnDisable() => UnbindIputActions();

    private void BindInputActions()
    {
        _playerState.HPChanged += HPBar;
        _playerController.RefreshMagazineUI += RefreshMagazineUI;
        _weapon.RefreshMagazineUI += RefreshMagazineUI;
    }

    private void UnbindIputActions()
    {
        _playerState.HPChanged -= HPBar;
        _playerController.RefreshMagazineUI -= RefreshMagazineUI;
        _weapon.RefreshMagazineUI -= RefreshMagazineUI;
    }

    private void HPBar(int _hp)
    {
        _hpBar.fillAmount = ((float)_hp / (float)_playerState.MaxHp);
    }

    private void CacheComponents()
    {
        _playerState = GetComponent<PlayerState>();
        _playerController = GetComponent<PlayerController>();
        Image[] scope = _scope.GetComponentsInChildren<Image>();
        _scope1 = scope[0];
        _scope2 = scope[1];
        _scope1.gameObject.SetActive(true);
        _scope2.gameObject.SetActive(false);
        _weapon = GetComponentInChildren<PlayerWeapon>();        
    }

    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagzine}";
        _grenade.text = $"{_playerState._grenadeNum} / {_playerState._grenadeMaxNum}";
    }
}