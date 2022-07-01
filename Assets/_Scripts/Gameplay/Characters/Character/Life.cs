using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Life : MonoBehaviour
{
    [SerializeField]
    private float amount;

    public float maximumLife = 100f;

    public UnityAction onDeath;

    public float Amount
    {
        get => amount;
        set
        {
            amount = Mathf.Clamp(value, 0, 6);;
            if (amount <= 0)
            {
                onDeath?.Invoke();

            }
        }
    }

    void Awake()
    {
        amount = maximumLife;
    }
}
