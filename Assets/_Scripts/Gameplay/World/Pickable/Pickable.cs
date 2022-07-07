using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Pickable
{
    [SerializeField] private PickableBase _base;
    public PickableBase Base => _base;    
    public EffectPickable Effect {get;set;}

    /// <summary>
    /// Assign a effect from the facroty dictionary
    /// </summary>
    public void InitPickable()
    {
        Effect = EffectPickableFactory.EffectConditions[_base.EffectToApply];
    }
}