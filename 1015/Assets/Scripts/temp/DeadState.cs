using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class DeadState : StateBase
{
    private PlayerControllerA _controller;

    public DeadState(PlayerControllerA controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("쥬금");
    }
    public override void Update()
    {
        Debug.Log("...");
    }
    public override void Exit()
    {
    }
}
