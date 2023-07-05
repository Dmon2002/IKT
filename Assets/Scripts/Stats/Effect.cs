using System;
using UnityEngine;

[Serializable]
public class Effect
{
    [SerializeField] private EffectImposer _prefab;
    [SerializeField] private StatChange _statChange;
    [SerializeField] private bool _isTemporary;
    [SerializeField] private float _duration;

    public StatChange StatChange => _statChange;

    public bool IsTemporary => _isTemporary;

    public float Duration => _duration;

    public EffectImposer SpawnEffect(Vector3 position)
    {
        return GameObject.Instantiate(_prefab, position, Quaternion.identity);
    }
}
