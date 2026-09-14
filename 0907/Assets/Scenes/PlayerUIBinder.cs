using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private tempPlayer _player;
    [SerializeField] private HealthGauge _healthGauge;
    [SerializeField] private tempPlayerUI _playerUI;
    [SerializeField] private ExpGauge _expGauge;

    private void Awake() => CacheComponents();

    private void OnEnable() => BindPlayerStatChangeEvents();
    private void OnDisable() => UnbindPlayerStatChangeEvents();
    private void OnDestroy() => UnbindPlayerStatChangeEvents();

    private void BindPlayerStatChangeEvents()
    {
        _player.OnHealthChange += _playerUI.RefreshHealthUI;
        _player.OnHealthChange += _healthGauge.RefreshGauge;

        _player.Exp.AddListener(_expGauge.RefreshGauge);
    }

    private void UnbindPlayerStatChangeEvents()
    {
        _player.OnHealthChange -= _playerUI.RefreshHealthUI;
        _player.OnHealthChange -= _healthGauge.RefreshGauge;

        _player.Exp.RemoveListener(_expGauge.RefreshGauge);
    }

    private void CacheComponents()
    {
        _player = GetComponent<tempPlayer>();
    }
}
