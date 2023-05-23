using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Character", menuName ="Selectable Character")]
public class SelectableCharacter: ScriptableObject
{
    public new string name;
    public GameObject characterPrefab;
    public Sprite sprite;

}
