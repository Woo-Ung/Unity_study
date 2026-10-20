using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratedClassMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerControls _controls;

    private void Awake()
    {
        CreateControls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        BindInputEvents();
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        UnbindInputEvents();
        _controls.Player.Disable();
    }

    private void CreateControls()
    {
        _controls = new PlayerControls();
    }

    private void BindInputEvents()
    {
        _controls.Player.Jump.performed += OnJump;
    }

    private void UnbindInputEvents()
    {
        _controls.Player.Jump.performed -= OnJump;
    }

    private void Move()
    {
        Vector2 input = _controls.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("GeneratedClassMover: Jump 액션이 들어왔습니다.");
    }
}
