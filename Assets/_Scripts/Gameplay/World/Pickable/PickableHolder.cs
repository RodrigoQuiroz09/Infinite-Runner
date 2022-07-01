using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PickableHolder : MonoBehaviour
{
    [SerializeField] SpriteRenderer whiteSpite;
    [SerializeField] TextMeshProUGUI _text;

    public Pickable objectPickable;
    public int points;
    private SpriteRenderer _spite;
    Vector3 initialPos;
    Sequence idleSeq;

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

    void Awake()
    {
        _spite = GetComponent<SpriteRenderer>();
        
        initialPos=transform.position;
      
        idleSeq = DOTween.Sequence();
        InitializeIdleAnimation();
    }

    private void Start() 
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {

            idleSeq.Pause();
            PlayPickUpAnimation();
            objectPickable.Effect.OnCollisionPoints?.Invoke(points);
            objectPickable.Effect.OnCollisionHeal?.Invoke(other.gameObject.GetComponent<PlayerController>());

        }
    }

    void InitializeIdleAnimation()
    {
        idleSeq.Pause();
        idleSeq.Append(transform.DOLocalMoveY(initialPos.y+.1f,1.5f));
        idleSeq.Append(transform.DOLocalMoveY(initialPos.y,1.5f));
        idleSeq.SetLoops(-1,LoopType.Restart);
        
    }

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
