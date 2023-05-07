using UnityEngine;

public class GameManager : Manager<GameManager>
{
    [SerializeField] private Vector2Int _startingPosition;
    private GameObject _player;
    
    public GameObject Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>().gameObject;
            }
            return _player;
        }
    }

    private void Start()
    {
        Player.transform.position = LevelManager.Instance.ConvertToPosition(_startingPosition);
    }
}
