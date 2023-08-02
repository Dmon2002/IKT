using Sirenix.OdinInspector;
using StatSystem;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [Required]
    [SerializeField] private Transform _heartContainer;
    [AssetsOnly]
    [SerializeField] private GameObject _heartPrefab;
    
    private List<GameObject> _hearts = new List<GameObject>();

    private Player _player;

    private int _currentMaxHP = 0;
    private int _currentHP = 0;

    private void Start()
    {
        _player = LevelManager.Instance.Player;
        SpawnMaxHealth();
    }

    private void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        int hp = (int)Mathf.Round(_player.StatContainer.GetStat<float>(StatNames.HP));
        if (hp == _currentHP)
            return;
        _currentHP = hp;
        for (int i = 0; i < _currentHP; i++)
        {
            _hearts[i].SetActive(true);
        }
        for (int i = _currentHP; i < _hearts.Count; i++)
        {
            _hearts[i].SetActive(false);
        }
    }

    private void SpawnMaxHealth()
    {
        int maxHP = (int)Mathf.Round(_player.StatContainer.GetMaxValue(StatNames.HP));
        if (maxHP <= _currentMaxHP)
            return;
        int diff = maxHP - _currentMaxHP;
        _currentMaxHP = maxHP;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                _hearts.Add(Instantiate(_heartPrefab, _heartContainer));
            }
        }
    }
}
