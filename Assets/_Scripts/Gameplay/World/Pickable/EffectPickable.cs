using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/// <summary>
/// Programable different effects the pickable object depending of an id
/// </summary>
public class EffectPickable
{
    public EffectPickableId Id { set; get; }
    public string Name { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// <para>
    ///     Unity Action for healing the player 
    /// </para>
    /// <para>
    ///     Effect
    /// </para>
    /// </summary>
    public UnityAction <PlayerController> OnCollisionHeal { get; set; }

    /// <summary>
    /// <para>
    ///     UnityAction to add points to the score
    /// </para>
    /// <para>
    ///     Effect
    /// </para>
    /// </summary>
    public UnityAction <int> OnCollisionPoints { get; set; }
}
