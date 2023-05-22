using System;
using System.Collections;
using UnityEngine;

public class CooldownReload 
{
    private float _cooldownDuration;
    private bool _onCooldown = false;
    private float _remainingTime = 0f;
    private IEnumerator _cooldownIEnumerator;

    public event Action Reloaded;
    private MonoBehaviour _mono;

    public float CooldownDuration
    {
        get { return _cooldownDuration; }
        set { _cooldownDuration = value; }
    }

    public float RemainingTime => _remainingTime;

    public bool OnCooldown => _onCooldown;

    public CooldownReload(float cooldownDuration, MonoBehaviour mono)
    {
        _mono = mono;
        _cooldownDuration = cooldownDuration;
    }

    public void StartReloading()
    {
        if (_onCooldown) return;
        _cooldownIEnumerator = ReloadCoroutine();
        _mono.StartCoroutine(_cooldownIEnumerator);
    }

    private IEnumerator ReloadCoroutine()
    {
        _remainingTime = _cooldownDuration;
        _onCooldown = true;
        while (_remainingTime > 0f)
        {
            _remainingTime -= Time.deltaTime;
            yield return null;

        }
        _onCooldown = false;
        _remainingTime = 0f;
        Reloaded?.Invoke();
    }

    public void StopReloding()
    {
        if (!_onCooldown) return;
        _onCooldown = false;
        _remainingTime = 0f;
        _mono.StopCoroutine(_cooldownIEnumerator);
    }
    
    public void EndReloading()
    {
        if (!_onCooldown) return;
        StopReloding();
        Reloaded?.Invoke();
    }

    public void PauseReloading()
    {
        if (!_onCooldown) return;
        _mono.StopCoroutine(_cooldownIEnumerator);
    }
}
