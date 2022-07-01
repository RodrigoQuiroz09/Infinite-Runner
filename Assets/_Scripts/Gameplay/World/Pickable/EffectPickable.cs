using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EffectPickable
{
    public EffectPickableId Id { set; get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UnityAction <PlayerController> OnCollisionHeal { get; set; }
    public UnityAction <int> OnCollisionPoints { get; set; }
}
