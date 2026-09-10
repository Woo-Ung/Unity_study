using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _grenadeSpawn;
    [SerializeField] private float _detectionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private GrenadeController _grenadePrefab;
    [field: SerializeField] public int _grenade_MaxNum { get; protected set; }
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDistance;

    private GameObject _grenadeShape;
    private PlayerWeapon _weapon;    
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    private PlayerUIController _playerUI;
    private float _grenadeTime;    
    public int _grenade_num { get; protected set; }

    private IInteractable _targetInteractable;
    private IDamageable _targetDamageable;
    private bool _hasDetectInteractable => _targetInteractable != null;
    private bool _hasDetectDamageable => _targetDamageable != null;
    private bool _isPressdInteractionKey => Input.GetKeyDown(_interactionKey);
    private bool _canInteraction => _hasDetectInteractable && _isPressdInteractionKey;
    [field: SerializeField] public bool _isJump { get; private set; }


    public GameObject GameObject
    {
        get => gameObject;
    }

    private void Awake() => CacheComponents();
    private void Start() => LockCursor();
    private void FixedUpdate() => _movement.Move();   
    private void Update()
    {
        _movement.Rotate();
        _weapon.Fire();
        _weapon.Reload();
        IsJump();
        Jump();
        DetectInteractable();
        TryInteract();
        SpawnGrenade();
    }
    private void LateUpdate()
    {        
        SetCameraTransform();
        SetWeaponTransform();        
    }

    private void IsJump()
    {
        Ray ray = new Ray(transform.position + (transform.up * 0.2f), Vector3.down);
        Debug.DrawRay(transform.position + (transform.up * 0.2f), Vector3.down * _groundDistance, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _groundDistance, _groundLayer))
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
        if(!Input.GetKeyDown(_jumpKey) || _isJump)
        {
            return;
        }
        _movement.Jump();
    }

    private void SpawnGrenade()
    {
        if (_grenade_num < 1)
        {
            return;
        }

        if (Input.GetKey(_grenadeKey))
        {
            Charging();
            _grenadeShape.SetActive(true);
        }
        if (Input.GetKeyUp(_grenadeKey))
        {
            _grenadeShape.SetActive(false);
            GrenadeController grenade = Instantiate(_grenadePrefab, _grenadeSpawn.position, _grenadeSpawn.rotation);
            grenade.SetGrenade(_grenadeTime, _grenadeSpawn);

            _grenade_num--;
            _grenadeTime = 1f;
        } 
    }

    private void Charging()
    {
        _grenadeTime += Time.deltaTime;
    }
    private void CacheComponents()
    {
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _playerUI = GetComponent<PlayerUIController>();
        _cameraTransform = Camera.main.transform;
        _grenadeTime = 1f;
        _grenadeShape = _grenadeSpawn.Find("GrenadeShape").gameObject;
        _grenadeShape.SetActive(false);
        _grenade_num = _grenade_MaxNum;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(_cameraPivot.position, _cameraPivot.rotation);
    }

    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(_cameraPivot.position, _cameraPivot.rotation);
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit, _detectionRange))
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
        if(!_canInteraction)
        {
            return;
        }

        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }
}