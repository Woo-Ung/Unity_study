using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public event Action HPBar;
    public event Action RefreshMagazineUI;

    private static PlayerUIManager _instance;
    public static PlayerUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerUIManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    private void Awake() => SetSingleton();
    private void Update() => GetInput();
    private void OnDestroy() => Clean();

    private void GetInput()
    {
        HPBar?.Invoke();
        RefreshMagazineUI?.Invoke();
    }

    private void Clean()
    {
        _instance = null;
    }

    private void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}