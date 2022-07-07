using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Flag to give the player space and prepare to get the next hit
    public bool IsInvincible=false;
    
    /// <summary>
    /// Triggers when the player life reaches 0
    /// </summary>
    public UnityAction PlayerDeath;

    [Tooltip("Jump Force of the player")]
    [SerializeField] 
    float jumpVelocity;
    [Tooltip("Surface the player can jump off")]
    [SerializeField] 
    LayerMask platformsLayerMask;
    [Tooltip("Time frame for animations")]
    [SerializeField] 
    float hitTimeAnim;
    [SerializeField] Animator whiteSprite; //White sprite player for esthetic reasons and animations
    [SerializeField] ExplosionParticlesTrigger explosion; 

    private float screenWidth; // Configuration for mobile mapping
    private bool IsOnGround=true; // Flag to enable player jump
    private Color intialColorPlayer; //Save initial color(sprite) whenever is needed to reset it
    private Sequence dmgSeq; // DoTween Sequence to indicate the player is half a life

    private Life _life;
    private Weapon _weapon;
    private Rigidbody2D _rg;
    private BoxCollider2D _collider;
    private Animator _anim;
    private SpriteRenderer _characterSprite;

    /// <summary>
    /// Listener when the player dies and get the screen width (If played in a mobile device)
    /// </summary>
    void Awake()
    {
        screenWidth = (float)Screen.width / 2.0f;
        _life = gameObject.GetComponent<Life>();
        _rg = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _weapon = gameObject.GetComponent<Weapon>();
        _life.onDeath+=Die;
    }

    /// <summary>
    /// Initialization of dmgSequence and save color
    /// </summary>
    void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        intialColorPlayer = _characterSprite.color;
        dmgSeq=DOTween.Sequence();
        InitializeDmgAnimation();
    }

    /// <summary>
    /// Reset player life and activates the running animation
    /// </summary>
    public void HandleStart()
    {
        _life.Amount=_life.maximumLife;
        gameObject.SetActive(true);
        _anim.SetBool("IsRunning", true);
        whiteSprite.SetBool("IsRunning", true);
    }

    /// <summary>
    /// Controlling mapping for jumping and shooting (Mobile and pc controls)
    /// </summary>
    public void HandleUpdate()
    {
        IsOnGround = IsGrounded();
       
        if(Input.GetKeyDown("space") && IsOnGround)
        {
            _anim.SetBool("IsGrounded", false); 
            whiteSprite.SetBool("IsGrounded", false);  
            Jump();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.position.x<screenWidth && IsOnGround)
            {
                _anim.SetBool("IsGrounded", false);  
                whiteSprite.SetBool("IsGrounded", false);  
                Jump();
            }
            else if(touch.position.x > screenWidth)
            {
                if(_weapon.ShootBullet("PlayerBullet", 0.1f)) _weapon.ToggleMuzzle();
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
        {
            if(_weapon.ShootBullet("PlayerBullet", 0.1f)) _weapon.ToggleMuzzle();
        }
        
    }

    /// <summary>
    /// Move in the Y axis with a force
    /// </summary>
    void Jump()
    {
        _rg.velocity=Vector2.up*jumpVelocity;
    }

    /// <summary>
    /// Cast a ray under the player and check if is on the ground. Also triggers a jump animation relying on the reaycast
    /// </summary>
    /// <returns>If the player hits a layer underneath it can jump</returns>
    bool IsGrounded()
    {
        RaycastHit2D raycast2D=Physics2D.Raycast(transform.position,-Vector2.up,1.0f,platformsLayerMask);
        _anim.SetBool("IsGrounded", raycast2D.collider!=null);  
        whiteSprite.SetBool("IsGrounded", raycast2D.collider!=null);  
        return raycast2D.collider!=null;
    }

    /// <summary>
    /// Invoke the PlayerDeath Action to trigger the GameManager to change the GameState and set this gameObjecto to false.
    /// </summary>
    void Die()
    {
        gameObject.SetActive(false);
        PlayerDeath?.Invoke();
        dmgSeq.Pause();
    }

    /// <summary>
    /// Animation with DoTween for respawning player. Give invincibility to the player for a second.
    /// </summary>    
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

    /// <summary>
    /// Auxiliar method to be Invoke with a delay. Turns off invincibility.
    /// </summary>
    void IsInvincibleToFalse()
    {
        IsInvincible=false;
    }
    
    /// <summary>
    /// Depending of the amounts of hearts this method triggers whether play dmgSeq or makes the whole process of respawning the player.
    /// </summary>
    public void PlayReceiveDamageAnimation()
    {
        if(_life.Amount%2>0)
        {
            dmgSeq.Play();
        }
        else
        {
            explosion.TriggerExplosion(gameObject,gameObject.transform.position,true);
            dmgSeq.Pause();
            PlayReceiveRespawnAnimation();
        }
    }
    
    /// <summary>
    /// Pause the pulsing red animation and reset the sprite color
    /// </summary>
    public void PauseDmgAnimation()
    {
        dmgSeq.Pause();
        _characterSprite.color=intialColorPlayer;
    }

    /// <summary>
    /// Hit with a bullet animation
    /// </summary>
    void InitializeDmgAnimation()
    {   
        dmgSeq.Pause();
        dmgSeq.Append(_characterSprite.DOColor(new Color(1,.5f,.5f,1f), hitTimeAnim));
        dmgSeq.Append(_characterSprite.DOColor(intialColorPlayer, hitTimeAnim));
        dmgSeq.SetLoops(-1,LoopType.Restart);
    }
}