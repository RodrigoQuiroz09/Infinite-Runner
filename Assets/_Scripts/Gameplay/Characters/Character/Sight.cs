using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Sight : MonoBehaviour
{
    public UnityAction OnSight;
     void OnTriggerStay2D(Collider2D other) 
     {
        if(other.gameObject.CompareTag("Player"))
        {
            OnSight?.Invoke();
        }
        
    }
}