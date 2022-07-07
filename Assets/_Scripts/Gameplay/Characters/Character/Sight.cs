using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Sight : MonoBehaviour
{
    //TODO: Change the BoxCollider with a raycast for optimization
    
    /// <summary>
    /// Whenever the enemy "see" a target it can shoot
    /// </summary>
    public UnityAction OnSight;
     void OnTriggerStay2D(Collider2D other) 
     {
        if(other.gameObject.CompareTag("Player"))
        {
            OnSight?.Invoke();
        }
        
    }
}