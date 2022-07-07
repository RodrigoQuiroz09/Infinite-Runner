using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPickableFactory
{
    /// <summary>
    /// Create a dictionary that order the effect id with a method effect
    /// </summary>
    public static void InitFactory()
    {
        foreach (var condition in EffectConditions)
        {
            var id = condition.Key;
            var statusCond = condition.Value;
            statusCond.Id = id;
        }
    }

    /// <summary>
    /// Dictionary that aligns EffectId with method and Unity Action
    /// </summary>
    public static Dictionary<EffectPickableId, EffectPickable> EffectConditions { get; set; } =

        new Dictionary<EffectPickableId, EffectPickable>()
        {
            {
                EffectPickableId.SUMPTS,
                new EffectPickable()
                {
                    Name ="Sum points Gem",
                    Description = "Sum +100 in the score",
                    OnCollisionPoints = SumHundredPoints
                }
            },            {
                EffectPickableId.PLUSHEALTH,
                new EffectPickable()
                {
                    Name ="Sum health points",
                    Description = "Sum +1 in health to player",
                    OnCollisionHeal = SumOneHealthPoint
                }
            },
        };

    /// <summary>
    /// Add a ceratin amount of points to the Score Manager
    /// TODO: Change Name
    /// </summary>
    /// <param name="points"> Points to be added</param>
    static void SumHundredPoints(int points)
    {
        ScoreManager.SharedInstance.PointsObtained+=points;
    }

    /// <summary>
    /// Add +1 health to the player
    /// </summary>
    /// <param name="player"> Player Controller to get life component</param>
    static void SumOneHealthPoint(PlayerController player)
    {
        player.gameObject.GetComponent<Life>().Amount+=1;
        player.PauseDmgAnimation();
    }
}

/// <summary>
/// Id´s of effects
/// </summary>
public enum EffectPickableId
{
    NONE, SUMPTS,PLUSHEALTH
}