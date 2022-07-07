using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlesTrigger : MonoBehaviour
{
    [Tooltip("Where the player is going to respawn")]
    [SerializeField] 
    GameObject respawnPoint;
    private Animator _deathAnimator;

    void Awake()
    {
        _deathAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays an explosion animation and respawn the object given.
    /// </summary>
    /// <param name="exploded"> The object that is going to "explode" and respawn</param>
    /// <param name="explosionPos">Position of the explosion</param>
    /// <param name="killedByBullet">Parameter to indicate if there is need to appear from respawn point or can respawn in place</param>
    /// <param name="isDeath">Check if the exploded is alive or not</param>

    public void TriggerExplosion(GameObject exploded, Vector3 explosionPos,bool killedByBullet, bool isDeath=false)
    {
            exploded.SetActive(false);
            _deathAnimator.gameObject.transform.position = explosionPos;

            _deathAnimator.SetTrigger("Death");
            float localPos=_deathAnimator.gameObject.transform.position.x;
            float limitPos=localPos-2;
            StartCoroutine(_deathAnimator.gameObject.GetComponent<MoveBackwards>().GoLeft(localPos,limitPos));
            exploded.transform.position = (killedByBullet?exploded.transform.position:respawnPoint.transform.position) ;
            exploded.SetActive(!isDeath);
            exploded.GetComponent<PlayerController>().PlayReceiveRespawnAnimation();
            Invoke("ResetTriggerAnimation",1f);
    }
    
    /// <summary>
    /// Reset the trigger of the animator. Auxiliar method to Inoke with delay.
    /// </summary>
    void ResetTriggerAnimation()
    {
        _deathAnimator.ResetTrigger("Death");
    }
};
