using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public bool IsInvincible=false;

    [SerializeField] float jumpVelocity;
    [SerializeField] LayerMask platformsLayerMask;
    [SerializeField] float hitTimeAnim;
    [SerializeField] Animator whiteSprite;
    [SerializeField] ExplosionParticlesTrigger explosion;

    private float screenWidth;
    private bool IsOnGround=true;
    private Color intialColorPlayer;
    private Sequence dmgSeq;

    private Life _life;
    private Weapon _weapon;
    private Rigidbody2D _rg;
    private BoxCollider2D _collider;
    private Animator _anim;
    private SpriteRenderer _characterSprite;

    void Awake()
    {
        screenWidth = (float)Screen.width / 2.0f;
        _life = gameObject.GetComponent<Life>();
        _rg = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _weapon = gameObject.GetComponent<Weapon>();
        

    }

    void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        intialColorPlayer = _characterSprite.color;
        dmgSeq=DOTween.Sequence();
        InitializeDmgAnimation();
    }
    public void HandleUpdate()
    {

        IsOnGround = IsGrounded();
       
            if(Input.GetKeyDown("space") && IsOnGround)
            {
                _anim.SetBool("IsGrounded", false); 
                whiteSprite.SetBool("IsGrounded", false);  
                Jump();
            }
            if ( Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if(touch.position.x>screenWidth && IsOnGround)
                {
                    _anim.SetBool("IsGrounded", false);  
                    whiteSprite.SetBool("IsGrounded", false);  
                    Jump();
                }
                if(touch.position.x<=screenWidth)
                {
                    if(_weapon.ShootBullet("PlayerBullet", 0.1f)) _weapon.ToggleMuzzle();
                }
            }
            if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
            {
                 if(_weapon.ShootBullet("PlayerBullet", 0.1f)) _weapon.ToggleMuzzle();
            }
        
    }

    void Jump()
    {
        _rg.velocity=Vector2.up*jumpVelocity;
    }

    bool IsGrounded()
    {
        RaycastHit2D raycast2D=Physics2D.Raycast(transform.position,-Vector2.up,1.0f,platformsLayerMask);
        _anim.SetBool("IsGrounded", raycast2D.collider!=null);  
        whiteSprite.SetBool("IsGrounded", raycast2D.collider!=null);  
        return raycast2D.collider!=null;
    }

        
    public void PlayReceiveRespawnAnimation()
    {
        var seq = DOTween.Sequence();
        seq.Append(_characterSprite.DOColor(new Color(1f,1f,1f,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        seq.Append(_characterSprite.DOColor(new Color(1f,1f,1f,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        seq.Append(_characterSprite.DOColor(new Color(1f,1f,1f,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        _anim.SetBool("IsRunning", true);  
        whiteSprite.SetBool("IsRunning", true);  
        IsInvincible=true;
        Invoke("IsInvincibleToFalse",1f);
    }

    void IsInvincibleToFalse()
    {
        IsInvincible=false;
    }

    public void PlayReceiveDamageAnimation()
    {
            if(_life.Amount%2>0)
            {
                dmgSeq.Play();
            }
            else
            {
                explosion.TriggerExplosion(gameObject,gameObject.transform.position);
                dmgSeq.Pause();
                PlayReceiveRespawnAnimation();
            }
    }

    void InitializeDmgAnimation()
    {   
        dmgSeq.Pause();
        dmgSeq.Append(_characterSprite.DOColor(new Color(1,.5f,.5f,1f), hitTimeAnim));
        dmgSeq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        dmgSeq.SetLoops(-1,LoopType.Restart);
    }
}