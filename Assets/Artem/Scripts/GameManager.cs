using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector2Int _startingPosition;
    private GameObject _player;
    
    public GameObject Player
    {
        get
        {
            if (_player == null)
            {
                _player = SpawnPlayer();
            }
            return _player;
        }
    }

    private void Start()
    {
        _player = SpawnPlayer();
    }

    private GameObject SpawnPlayer()
    {
        return Instantiate(_playerPrefab, LevelManager.Instance.ConvertToPosition(_startingPosition), Quaternion.identity);
    }
}
