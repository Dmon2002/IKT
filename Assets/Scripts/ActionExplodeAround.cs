using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionSystem;

public class ActionExplodeAround : ActionSystem.Action
{
    [AssetsOnly]
    [SerializeField] private GameObject _explodePrefab;
    [SerializeField] private Entity _entity;

    public override IEnumerator Perform()
    {
        var levelManager = LevelManager.Instance;
        Room entityRoom = levelManager.GetNearestRoom(_entity.transform.position);
        List<Room> neighbours = levelManager.GetAllNeighbours(entityRoom.Coords);
        foreach (var neighbour in neighbours)
        {
            if (neighbour.FogRevealed)
            {
                GameObject.Instantiate(_explodePrefab, neighbour.transform.position, Quaternion.identity);
            } 
            else
            {
                neighbour.RevealFog((neighbour.transform.position - entityRoom.transform.position).normalized);
            }
        }
        yield break;
    }
}
