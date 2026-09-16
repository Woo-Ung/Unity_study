using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _grenadeSpawn;
    [SerializeField] private GrenadeController _grenadePrefab;    
    [SerializeField] private LayerMask _groundLayer;  
    [SerializeField] private Canvas _gameoverUI;

    private GameObject _grenadeShape;
    private PlayerWeapon _weapon;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    private PlayerUIController _playerUI;
    private float _grenadeTime;    

    private IInteractable _targetInteractable;
    private IDamageable _targetDamageable;
    private bool _hasDetectInteractable => _targetInteractable != null;
    private bool _hasDetectDamageable => _targetDamageable != null;
    [field: SerializeField] public bool _isJump { get; private set; }

    public GameObject GameObject { get => gameObject; }
    public PlayerInputManager PlayerInput => PlayerInputManager.Instance;

    private void Awake() => CacheComponents();
    private void OnEnable() => BindInputActions();
    private void Update()
    {
        IsJump();
        DetectInteractable();
    }
    private void LateUpdate()
    {        
        SetCameraTransform();
        SetWeaponTransform();        
    }
    private void OnDisable() => UnbindInputActions();

    private void BindInputActions()
    { 
        PlayerInput.Move += _movement.Move;
        PlayerInput.Rotate += _movement.Rotate;
        PlayerInput.Jump += Jump;
        PlayerInput.Interact += TryInteract;
        PlayerInput.Fire += _weapon.Fire;
        PlayerInput.Reload += _weapon.Reload;
        PlayerInput.GrenadeSpawn += GrenadeSpawn;
        PlayerInput.GrenadeThrow += GrenadeThrow;
    }

    private void UnbindInputActions()
    {
        PlayerInput.Move -= _movement.Move;
        PlayerInput.Rotate -= _movement.Rotate;
        PlayerInput.Jump -= Jump;
        PlayerInput.Interact -= TryInteract;
        PlayerInput.Fire -= _weapon.Fire;
        PlayerInput.Reload -= _weapon.Reload;
        PlayerInput.GrenadeSpawn -= GrenadeSpawn;
        PlayerInput.GrenadeThrow -= GrenadeThrow;
    }

    private void IsJump()
    {
        Ray ray = new Ray(transform.position + (transform.up * 0.2f), Vector3.down);
        Debug.DrawRay(transform.position + (transform.up * 0.2f), Vector3.down * _playerState._groundDistance, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _playerState._groundDistance, _groundLayer))
        {            
            _isJump = false;                     
        }
        else
        {
            _isJump = true;
        }
    }

    private void Jump()
    {
        if(_isJump)
        {
            return;
        }
        _movement.Jump();
    }

    private void GrenadeSpawn()
    {
        if (_playerState._grenadeNum < 1)
        {
            return;
        }

        Charging();
    }
    private void GrenadeThrow()
    {
        _grenadeShape.SetActive(false);
        GrenadeController grenade = Instantiate(_grenadePrefab, _grenadeSpawn.position, _grenadeSpawn.rotation);
        grenade.SetGrenade(_grenadeTime, _grenadeSpawn);

        _playerState._grenadeNum--;
        _grenadeTime = 1f;
    }

    private void Charging()
    {
        _grenadeShape.SetActive(true);
        _grenadeTime += Time.deltaTime;
    }
    private void CacheComponents()
    {
        _playerState = GetComponent<PlayerState>();
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _playerUI = GetComponent<PlayerUIController>();      
        _gameoverUI.gameObject.SetActive(false);
        _cameraTransform = Camera.main.transform;
        _grenadeTime = 1f;
        _grenadeShape = _grenadeSpawn.Find("GrenadeShape").gameObject;
        _grenadeShape.SetActive(false);
    }


    private void FreeCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(_cameraPivot.position, _cameraPivot.rotation);
    }

    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(_cameraPivot.position, _cameraPivot.rotation);
    }

    public void DetectInteractable() //코루틴수정ㄱ
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit, _playerState._detectionRange))
        {
            if(_hasDetectInteractable)
            {
                _targetInteractable.Untargeting();
                _targetInteractable = null;
            }
                        
            else if (_hasDetectDamageable)
            {
                _playerUI._scope2.gameObject.SetActive(false);
                _playerUI._scope1.gameObject.SetActive(true);
                _targetDamageable = null;
            }
            return;
        }

        if(_hasDetectInteractable)
        {
            if(hit.collider.gameObject == _targetInteractable.GameObject)
            {
                return; // 같은 Interactable을 계속 주시하고 있는 경우
            }
        }
        
        else if (_hasDetectDamageable)
        {
            if (hit.collider.gameObject == _targetDamageable.GameObject)
            {
                return;
            }
        }

        _targetInteractable?.Untargeting();
        _targetInteractable = hit.collider.GetComponent<IInteractable>();

        _targetInteractable?.Targeting(); // ?를 붙이면 if문 효과 null이면 null반환 아니면 .실행
       
        _targetDamageable = hit.collider.GetComponent<IDamageable>();

        if (_targetDamageable == null)
        {
            _playerUI._scope2.gameObject.SetActive(false);
            _playerUI._scope1.gameObject.SetActive(true);
        }
        else
        {
            _playerUI._scope2.gameObject.SetActive(true);
            _playerUI._scope1.gameObject.SetActive(false);
        }
    }      
    public void TryInteract()
    {
        if(!_hasDetectDamageable)
        {
            return;
        }

        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }
    private void OnDestroy()
    {
        _gameoverUI.gameObject.SetActive(true);
        FreeCursor();
    }
}