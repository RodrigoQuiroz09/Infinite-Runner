using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPickableFactory
{
    public static void InitFactory()
    {
        foreach (var condition in EffectConditions)
        {
            var id = condition.Key;
            var statusCond = condition.Value;
            statusCond.Id = id;
        }
    }
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

    static void SumHundredPoints(int points)
    {
        ScoreManager.SharedInstance.PointsObtained+=points;
    }

    static void SumOneHealthPoint(PlayerController player)
    {
        player.gameObject.GetComponent<Life>().Amount+=1;
        player.PauseDmgAnimation();
    }
}

public enum EffectPickableId
{
    NONE, SUMPTS,PLUSHEALTH
}