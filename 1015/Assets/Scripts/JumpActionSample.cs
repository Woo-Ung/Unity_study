using UnityEngine;
using UnityEngine.InputSystem;

public class JumpActionSample : MonoBehaviour
{
    [SerializeField] private InputAction _jumpAction;

    //private void Update()
    //{
    //    ReadJump();
    //}
    private void OnEnable()
    {
        _jumpAction.Enable();
        BindInputEvents();
    }

    private void OnDisable()
    {
        UnbindInputEvents();
        _jumpAction.Disable();
    }

    private void BindInputEvents()
    {
        _jumpAction.performed += OnJump;
    }

    private void UnbindInputEvents()
    {
        _jumpAction.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("JumpEventSample: Jump 액션이 들어왔습니다.");
    }

    //private void ReadJump()
    //{
    //    if(_jumpAction.WasPressedThisFrame())
    //    {
    //        Debug.Log("JumpActionSample: Jump를 눌렀습니다.");
    //    }
    //}
}
