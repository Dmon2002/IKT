using Sirenix.OdinInspector;
using UnityEngine;

public class AbilityDecisionDirectionVectorInput : AbilityDecisionDirection
{
    [SerializeField] private bool makeSmooth;
    [ShowIf("makeSmooth")]
    [SerializeField] private float _smoothInputSpeed;
    private PlayerInput _input;

    private Vector2 _currentInput;
    private Vector2 _smoothVelocity;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public override Vector2 DecideDirection()
    {
        Vector2 direction = _input.Player.Walk.ReadValue<Vector2>();
        if (makeSmooth)
        {
            _currentInput = Vector2.SmoothDamp(_currentInput, direction, ref _smoothVelocity, _smoothInputSpeed);
        }
        else
        {
            _currentInput = direction;
        }
        return _currentInput;
    }
}
