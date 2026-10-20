using UnityEngine;
using UnityEngine.InputSystem;

// sealed를 쓰면 상속하지 못하는 클래스가 된다.
public sealed class IdleState : StateBase
{
    private PlayerControllerA _controller;

    public IdleState(PlayerControllerA controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {       
        Debug.Log("<color=blue>Idle 상태 진입</color>");
        _controller.AttackInputRef.action.performed += TransitionAttack;
    }
    public override void Update()
    {
        Debug.Log("<color=blue>Idle 상태</color>");
    }
    public override void Exit()
    {
        Debug.Log("<color=blue>Idle 상태 빠져나감</color>");
        _controller.AttackInputRef.action.performed -= TransitionAttack;
    }

    public void TransitionAttack(InputAction.CallbackContext ctx)
    {
        _controller.SetState(_controller.Attack);
    }
}
