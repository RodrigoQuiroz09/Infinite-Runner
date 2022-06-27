using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLimits : MonoBehaviour
{
    [SerializeField] GameObject respawnPoint;
    [SerializeField] Animator deathAnimator;
    private BoxCollider2D _col;
    private float localPos;
    void Awake() 
    {
        _col=gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            deathAnimator.gameObject.transform.position= new Vector3( 
                other.gameObject.transform.position.x, 
                other.gameObject.transform.position.y+1, 
                other.gameObject.transform.position.z);
            deathAnimator.SetTrigger("Death");
            localPos=deathAnimator.gameObject.transform.position.x;
            float limitPos=localPos-2;
            StartCoroutine(deathAnimator.gameObject.GetComponent<MoveBackwards>().GoLeft(localPos,limitPos));
            other.gameObject.transform.position=respawnPoint.transform.position;
            other.gameObject.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().PlayReceiveRespawnAnimation();
            Invoke("ResetTriggerAnimation",1f);
        }
    }

    void ResetTriggerAnimation()
    {
        deathAnimator.ResetTrigger("Death");
    }

}
