using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputManager : MonoBehaviour
{
    // controller
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;    
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;

    // movement
    [SerializeField] private string _moveX = "Horizontal";
    [SerializeField] private string _moveY = "Vertical";
    [SerializeField] private string _rotateX = "Mouse X";
    [SerializeField] private string _rotateY = "Mouse Y";

    // weapon
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;

    public event Action Fire;
    public event Action Reload;
    public event Action Interact;
    public event Action GrenadeSpawn;
    public event Action GrenadeThrow;
    public event Action<Vector2> Move;
    public event Action<Vector3> Rotate;
    public event Action Jump;

    private static PlayerInputManager _instance;
    public static PlayerInputManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerInputManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    private void Awake() => SetSingleton();
    private void Update() => GetInput();
    private void OnDestroy() => CleanUp();

    private void CleanUp()
    {
        _instance = null;
    }

    private void GetInput()
    {
        Rotate?.Invoke(GetRotate());
        Move?.Invoke(GetMove());
        if (Input.GetKeyDown(_jumpKey))
        {
            Jump?.Invoke();
        }
        if (Input.GetKey(_fireKey))
        {
            Fire?.Invoke();
        }
        if (Input.GetKeyDown(_reloadKey))
        {
            Reload?.Invoke();
        }
        if(Input.GetKeyDown(_interactionKey))
        {
            Interact?.Invoke();
        }
        if(Input.GetKey(_grenadeKey))
        {
            GrenadeSpawn?.Invoke();
        }
        if (Input.GetKeyUp(_grenadeKey))
        {
            GrenadeThrow?.Invoke();
        }
    }

    private Vector3 GetRotate()
    {
        float x = Input.GetAxis(_rotateX);
        float y = Input.GetAxis(_rotateY);

        return new Vector3(-y, x, 0);
    }

    private Vector3 GetMove()
    {
        float x = Input.GetAxisRaw(_moveX);
        float z = Input.GetAxisRaw(_moveY);

        return new Vector3(x, z).normalized;
    }

    private void SetSingleton()
    {
        if(_instance != null && _instance != this)
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
