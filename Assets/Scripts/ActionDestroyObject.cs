using System.Collections;
using UnityEngine;
using ActionSystem;

public class ActionDestroyObject : Action
{
    [SerializeField] private GameObject _gameObject;

    public override IEnumerator Perform()
    {
        GameObject.Destroy(_gameObject);
        yield break;
    }
}
