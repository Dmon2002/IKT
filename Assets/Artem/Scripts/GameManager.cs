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

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private GameObject SpawnPlayer()
    {
        return Instantiate(_player, LevelManager.Instance.ConvertToPosition(_startingPosition), Quaternion.identity);
    }
}
