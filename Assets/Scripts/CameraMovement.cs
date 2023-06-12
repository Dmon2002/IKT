using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _downPos;

    private Transform _followTarget;

    void Start()
    {
        transform.position = (Vector3)_startPos + new Vector3(0f, 0f, transform.position.z);
        _followTarget = LevelManager.Instance.Player.transform;
    }

    void Update()
    {
        Vector3 target = new()
        {
            x = _startPos.x,
            y = _followTarget.position.y - _downPos,
            z = transform.position.z
        };

        if (target.y < _startPos.y)
        {
            target = new Vector3(_startPos.x, _startPos.y, -10);

        }

        Vector3 pos = Vector3.Lerp(a: transform.position, b: target, t: _moveSpeed * Time.deltaTime);
        transform.position = pos;
        _startPos = new Vector2(_startPos.x, pos.y);
    }
}
