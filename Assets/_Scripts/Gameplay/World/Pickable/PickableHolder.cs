using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// GameObject holder of the scriptable object of pickable
/// </summary>
public class PickableHolder : MonoBehaviour
{
    [SerializeField] SpriteRenderer whiteSpite;
    [SerializeField] TextMeshProUGUI _text;

    /// <summary>
    /// Generic pickable assigned
    /// </summary>
    public Pickable objectPickable;

    [HideInInspector] public int points; // Point to be added if needed
    private SpriteRenderer _spite;
    Vector3 initialPos;
    Sequence idleSeq;

    /// <summary>
    /// Reset sprites, choose an effect from pickable, get to position.
    /// </summary>
    public void HandleRestart()
    {
        _spite.sprite=objectPickable.Base.SpriteObj;
        whiteSpite.sprite=objectPickable.Base.SpriteObj;
        _spite.color=Color.white;
        whiteSpite.color=Color.white;
        objectPickable.InitPickable();
        transform.position=initialPos;
        _text.color= new Color(1,1,1,0);
        idleSeq.Play();
    }

    /// <summary>
    /// Initialize DoTween animation an get initial position
    /// </summary>
    void Awake()
    {
        _spite = GetComponent<SpriteRenderer>();
        initialPos=transform.position;
        idleSeq = DOTween.Sequence();
        InitializeIdleAnimation();
    }

    /// <summary>
    /// When hit by the player play animation and trigger it´s respective unity actions (effects)
    /// </summary>
    /// <param name="other">Object collides with (Player)</param>
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            idleSeq.Pause();
            PlayPickUpAnimation();
            objectPickable.Effect.OnCollisionPoints?.Invoke(points);
            objectPickable.Effect.OnCollisionHeal?.Invoke(other.gameObject.GetComponent<PlayerController>());
        }
    }

    /// <summary>
    /// DoTween Animation that goes up and down
    /// </summary>
    void InitializeIdleAnimation()
    {
        idleSeq.Pause();
        idleSeq.Append(transform.DOLocalMoveY(initialPos.y+.1f,1.5f));
        idleSeq.Append(transform.DOLocalMoveY(initialPos.y,1.5f));
        idleSeq.SetLoops(-1,LoopType.Restart);
    }

    /// <summary>
    /// DoTween Animation that represents a pick up animation
    /// </summary>
    void PlayPickUpAnimation()
    {
        _text.text = (points>0 ? $"+{points}": "");
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(initialPos.y+.6f,1.5f));
        seq.Join(_spite.DOFade(0, 1f));
        seq.Join(_text.DOFade(1,1f));
        seq.Append(whiteSpite.DOFade(0, .1f));
        seq.Append(_text.DOFade(0,1f));
    }
}
