using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _pitch;
    private float _upSpeed;
    private float _upTime;
    private int _duartion;
    private Rigidbody _rigidbody;
    private bool _useStimpak;

    private void Awake() => CacheComponents();
    private void Update()
    {
        UpTime();
        EndStimpack();
    }

    public void Rotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;

        transform.Rotate(0, input.y, 0, Space.Self);

        _pitch = Mathf.Clamp(_pitch + input.x, _minPitch, _maxPitch);

        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }    

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    public void Move()
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();
        Vector3 input = ReadMoveInput();
                
        Vector3 direction = transform.right * input.x + transform.forward * input.z;
        Vector3 newVelocity = new Vector3(direction.x * _moveSpeed, _rigidbody.velocity.y, direction.z * _moveSpeed);
        _rigidbody.velocity = newVelocity;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        _useStimpak = true;
        _upSpeed = moveSpeed;
        _moveSpeed += _upSpeed;
    }

    public void SetDuration(int duration)
    {
        _duartion = duration;
    }
    private void UpTime()
    {
        if (!_useStimpak)
        {
            return;
        }
        _upTime += Time.deltaTime;
    }
    private void EndStimpack()
    {
        if(_upTime <= _duartion)
        {
            return;
        }
        GetComponentInChildren<PlayerWeapon>().SetCooldown(0.3f);
        _moveSpeed -= _upSpeed;
        _useStimpak = false;
        _upTime = 0;
        _duartion = 0;
    }

    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        return new Vector3(-y, x, 0);
    }

    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z).normalized;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _useStimpak = false;
        _duartion = 0;
        _upSpeed = 0;
    }
}