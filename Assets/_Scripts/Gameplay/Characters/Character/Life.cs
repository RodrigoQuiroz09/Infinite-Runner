using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Life : MonoBehaviour
{
    [SerializeField] float amount;

    public float maximumLife = 6f;

    public UnityAction onDeath;

    /// <summary>
    /// Current Amount of health. When reach 0 triggers a Unity Action. Clamp to not get infinite life
    /// </summary>
    public float Amount
    {
        get => amount;
        set
        {
            amount = Mathf.Clamp(value, 0, maximumLife);;
            if (amount <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }

    /// <summary>
    /// Assign health
    /// </summary>
    void Awake()
    {
        amount = maximumLife;
    }
}
