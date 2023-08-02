using StatSystem;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    [SerializeField] private List<StatChange> _statChanges;

    private Entity _entity;

    private void Start()
    {
        if (_entity == null)
            return;
        foreach (var change in _statChanges)
        {
            _entity.StatContainer.ApplyStatChange(change, true);
        }
    }

    public void SetEntity(Entity entity)
    {
        _entity = entity;
    }
}
