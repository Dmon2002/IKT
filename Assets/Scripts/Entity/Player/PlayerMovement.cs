using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _smoothInputSpeed;
    [SerializeField] private StatContainer _playerStatContainer;

    private PlayerInput _input;

    private Vector2 _currentInput;
    private Vector2 _smoothVelocity;

    public bool CanMove { get; set; } = true;

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
        if (_input == null)
            _input = new PlayerInput();
        _input.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 input = _input.Player.Walk.ReadValue<Vector2>();
        _currentInput = Vector2.SmoothDamp(_currentInput, input, ref _smoothVelocity, _smoothInputSpeed);
        Move(_currentInput, Time.fixedDeltaTime);
    }

    public void Move(Vector2 moveVector2, float time)
    {
        if (!CanMove)
            return;
        transform.Translate(moveVector2 * _playerStatContainer.GetStatFloatValue(Stat.MovementSpeedStatName) * time);
    }
}
