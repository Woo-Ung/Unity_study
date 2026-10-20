using UnityEngine;
using UnityEngine.InputSystem;

public class JumpReferenceSample : MonoBehaviour
{
    [SerializeField] private InputActionReference _jumpActionReference;

    private void OnEnable()
    {
        _jumpActionReference.action.Enable();
    }

    private void Update()
    {
        ReadJump();
    }

    private void OnDisable()
    {
        _jumpActionReference.action.Disable();
    }

    private void ReadJump()
    {
        if(_jumpActionReference.action.WasPressedThisFrame())
        {
            Debug.Log("JumpReferenveSample: Jump를 눌렀습니다.");
        }
    }
}
