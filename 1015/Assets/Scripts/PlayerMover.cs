using System;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    // InputValue
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _moveInput;

    private void Update()
    {
        Move();
    }

    // InputValue
    //public void OnMove(InputValue value)
    //{
    //    _moveInput = value.Get<Vector2>();
    //}

    //public void OnJump()
    //{
    //    Debug.Log($"PlayerMover: Jump 액션이 들어왔습니다.");
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    Debug.Log($"PlayerMover: Jump 단계는 {context.phase}입니다.");
    //    if(context.performed)
    //    {
    //        Debug.Log($"PlayerMover: Jump 액션이 들어왔습니다.");
    //    }
    //}

    private void Move()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y);
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}
