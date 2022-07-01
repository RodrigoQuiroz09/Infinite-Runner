using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] Sight sight;
    [SerializeField] int points;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject Canvas;

    private Life _life;
    private Animator _anim;
    private BoxCollider2D _collider;
    private Rigidbody2D _rig;
    private Weapon _weapon;
    
    void Awake() 
    {
        _life=GetComponent<Life>();
        _anim=GetComponent<Animator>();
        _collider=GetComponent<BoxCollider2D>();
        _rig=GetComponent<Rigidbody2D>();
        _weapon=gameObject.GetComponent<Weapon>();
        text.color= new Color(1,1,1,0);
        _life.onDeath+=Die;
        sight.OnSight+=ShootTarget;
    }

    void ShootTarget()
    {
        if(_life.Amount>0)
        {
            if(_weapon.ShootBullet("EnemyBullet", 0.1f)) _weapon.ToggleMuzzle();
        }
        
    }

    IEnumerator GoDownInYForDeathAnim()
    {
        float localPos=transform.position.y;
        float limitPos= transform.position.y-0.44f;
        while (localPos >= limitPos)
        {
            localPos=transform.position.y;
            localPos -= .8f* Time.deltaTime;
            transform.position = new Vector3 (transform.position.x, localPos, transform.position.z);
            yield return null;
        }
    }

    void Die()
    {
        StartCoroutine( GoDownInYForDeathAnim());
        PlayPointsAnimation();
        _rig.isKinematic = true;
        _collider.enabled=false;
        _anim.SetTrigger("IsDeath");
        sight.gameObject.SetActive(false);
        ScoreManager.SharedInstance.PointsObtained+=points;
    }

    void PlayPointsAnimation()
    {
        text.text = (points>0 ? $"+{points}": "");
        var seq = DOTween.Sequence();
        seq.Append(Canvas.transform.DOMoveY(Canvas.transform.position.y+.6f,1.5f));
        seq.Join(text.DOFade(1,1f));
        seq.Append(text.DOFade(0,1f));
    }

}
