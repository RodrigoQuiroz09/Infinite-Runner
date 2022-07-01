using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] Sprite imgBullet;
    public float damage;
    private Animator _anim;
    private MoveFordward _moveF;
    private MoveBackwards _moveB;
    private SpriteRenderer _sprite;

    void Awake() 
    {
        _anim=GetComponent<Animator>();
        _moveF=GetComponent<MoveFordward>();
        _sprite=GetComponent<SpriteRenderer>();
        _moveB=GetComponent<MoveBackwards>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Floor"))
        {
            _moveF.CanMoveForward=false;
            StartCoroutine(_moveB.GoLeft(gameObject.transform.position.x,gameObject.transform.position.x-2));
            _anim.SetTrigger("Impact");
        
            Invoke("OffObject",0.2f);

            Life life = other.GetComponent<Life>();
            if (life != null) life.Amount -= damage;
            
            PlayerController player=other.GetComponent<PlayerController>();
            if(player!=null) 
            {
                if(!player.IsInvincible) player.PlayReceiveDamageAnimation();
                else life.Amount += damage;
            }
            
        }

    }

    void OffObject()
    {
        gameObject.SetActive(false);
        _anim.ResetTrigger("Impact");
        _sprite.sprite=imgBullet;
        _moveF.CanMoveForward=true;
    }
}
