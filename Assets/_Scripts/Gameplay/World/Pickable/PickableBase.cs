using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Pickable", menuName = "Pickable Item/New Item")]
public class PickableBase : ScriptableObject 
{
    [SerializeField] private string namePickable;
    public string Name => namePickable;
    [SerializeField]  Sprite sprite;
    public Sprite SpriteObj => sprite;
    [SerializeField]  EffectPickableId effectToApply;
    public EffectPickableId EffectToApply=>effectToApply;
    [TextArea][SerializeField]  string description;
}

