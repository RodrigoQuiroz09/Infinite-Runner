using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlesTrigger : MonoBehaviour
{
    [SerializeField] GameObject respawnPoint;
    private Animator _deathAnimator;

    void Awake()
    {
        _deathAnimator = GetComponent<Animator>();
    }

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
        void ResetTriggerAnimation()
    {
        _deathAnimator.ResetTrigger("Death");
    }
}
