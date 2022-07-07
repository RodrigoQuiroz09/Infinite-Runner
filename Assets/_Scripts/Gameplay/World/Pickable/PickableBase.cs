using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Generic objects that the player can pickup
/// </summary>
[CreateAssetMenu(fileName = "Pickable", menuName = "Pickable Item/New Item")]
public class PickableBase : ScriptableObject 
{
    [SerializeField] private string namePickable;
    public string Name => namePickable;
    [SerializeField]  Sprite sprite;
    public Sprite SpriteObj => sprite;
    
    [Tooltip("Effect to trigger when picked")]
    [SerializeField]  
    EffectPickableId effectToApply;
    public EffectPickableId EffectToApply=>effectToApply;
    [TextArea][SerializeField]  string description;
}

