using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerState _playerState; 
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _pitch;

    //--stimpak
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

    public void Rotate(Vector3 input)
    {
        Vector3 direction = input * _mouseSensitivity;

        transform.Rotate(0, direction.y, 0, Space.Self);

        _pitch = Mathf.Clamp(_pitch + direction.x, _minPitch, _maxPitch);

        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _playerState._jumpSpeed, ForceMode.Impulse);
    }

    public void Move(Vector2 input)
    {                
        Vector3 direction = transform.right * input.x + transform.forward * input.y;
        Vector3 newVelocity = new Vector3(direction.x * _playerState._moveSpeed, _rigidbody.velocity.y, direction.z * _playerState._moveSpeed);

        _rigidbody.velocity = newVelocity;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        _useStimpak = true;
        _upSpeed = moveSpeed;
        _playerState._moveSpeed += _upSpeed;
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
        _playerState._moveSpeed -= _upSpeed;
        _useStimpak = false;
        _upTime = 0;
        _duartion = 0;
    }

    private void CacheComponents()
    {
        _playerState = GetComponent<PlayerState>();
        _rigidbody = GetComponent<Rigidbody>();
        _useStimpak = false;
        _duartion = 0;
        _upSpeed = 0;
    }
}