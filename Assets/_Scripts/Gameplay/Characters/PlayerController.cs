using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpVelocity;
    [SerializeField] LayerMask platformsLayerMask;
    [SerializeField] float hitTimeAnim;
    [SerializeField] Animator whiteSprite;
    
    private float screenWidth;
    private bool IsOnGround=true;
    private Color intialColorPlayer;
    private Weapon _weapon;
    private Rigidbody2D _rg;
    private CapsuleCollider2D _collider;
    private Animator _anim;
    private SpriteRenderer _characterSprite;

    

    void Awake()
    {
        screenWidth = (float)Screen.width / 2.0f;
        _characterSprite=GetComponent<SpriteRenderer>();
        intialColorPlayer=_characterSprite.color;
    }

    void Start()
    {
        _rg= gameObject.GetComponent<Rigidbody2D>();
        _anim= gameObject.GetComponent<Animator>();
        _collider= gameObject.GetComponent<CapsuleCollider2D>();
        _weapon=gameObject.GetComponent<Weapon>();
    }
    public void HandleUpdate()
    {
        IsOnGround=IsGrounded();
       
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
                    if(_weapon.ShootBullet("", 0.1f)) _weapon.ToggleMuzzle();
                }
            }
            if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
            {
                 if(_weapon.ShootBullet("", 0.1f)) _weapon.ToggleMuzzle();
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
        seq.Append(_characterSprite.DOColor(new Color(255,255,255,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        seq.Append(_characterSprite.DOColor(new Color(255,255,255,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        seq.Append(_characterSprite.DOColor(new Color(255,255,255,0), hitTimeAnim));
        seq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        _anim.SetBool("IsRunning", true);  
        whiteSprite.SetBool("IsRunning", true);  
    }
}