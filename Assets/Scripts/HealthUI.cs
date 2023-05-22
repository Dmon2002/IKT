using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<Image> _heartImages;

    private Entity _player;

    private void Awake()
    {
        _player = LevelManager.Instance.Player;
    }

    private void OnEnable()
    {
        _player.HealthChanged += UpdateHP;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= UpdateHP;
    }

    private void UpdateHP()
    {
        for (int i = 0; i < _heartImages.Count; i++)
        {
            if ((i + 1) / _heartImages.Count >= _player.Hp / _player.MaxHp)
            {
                _heartImages[i].gameObject.SetActive(false);
            } 
            else
            {
                _heartImages[i].gameObject.SetActive(true);
            }
        }
    }
}
