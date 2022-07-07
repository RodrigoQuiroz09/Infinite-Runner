using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLimits : MonoBehaviour
{
    [SerializeField] ExplosionParticlesTrigger explosion;
    private BoxCollider2D _col;
    private Vector3 deathPos;
    void Awake() 
    {
        _col=gameObject.GetComponent<BoxCollider2D>();
    }
    
    /// <summary>
    /// Whenever the player enter a Map limit triggers the explosion and substract life.
    /// </summary>
    /// <param name="other"> Object which collides </param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            deathPos= new Vector3(
                other.gameObject.transform.position.x,
                other.gameObject.transform.position.y+1,
                other.gameObject.transform.position.z
            );
            Life playerLife = other.gameObject.GetComponent<Life>();
            if(playerLife !=null )
            {
                playerLife.Amount-=2;
            }
            explosion.TriggerExplosion(other.gameObject,deathPos,false,playerLife.Amount==0);
        }
    }



}
