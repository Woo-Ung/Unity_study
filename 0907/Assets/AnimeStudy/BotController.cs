using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnAttack;

    private Vector2 _prevMovement;

    private void Update() => SetMove();

    private void SetMove()
    {
        // 이전 프레임의 Movement와 같으면 return;
        // 다르다면 OnMove
        Vector2 movement = GetMovement();
        if(_prevMovement == movement)
        {
            return;
        }

        OnMove?.Invoke(movement);
        _prevMovement = movement;
    }

    private Vector2 GetMovement()
    {
        // 입력 받아서 Vector2 반환 GetAxisRaw
        // 단위벡터로 만들지 말기

        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
}