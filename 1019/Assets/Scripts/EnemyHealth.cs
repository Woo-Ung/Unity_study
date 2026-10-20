using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private InputActionReference _damageInput;
    [SerializeField] private int _health = 5;
    private BehaviorGraphAgent _graphAgent;

    private void Awake()
    {
        _graphAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        _graphAgent.SetVariableValue("Health", _health);
    }

    private void OnEnable()
    {
        _damageInput.action.Enable();
        _damageInput.action.performed += OnDamage;
    }

    private void OnDamage(InputAction.CallbackContext context)
    {
        _health -= 1;
        _graphAgent.SetVariableValue("Health", _health);
    }
}
