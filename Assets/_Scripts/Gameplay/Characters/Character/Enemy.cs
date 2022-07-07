using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Tooltip("Line of vision of the enemy")]
    [SerializeField] 
    Sight sight;

    [Tooltip("Points given for killing this enemy")]
    [SerializeField] 
    int points;

    [Tooltip("Text holder for points")]
    [SerializeField] 
    TextMeshProUGUI text;

    [Tooltip("Canvas attached to the text")][SerializeField] GameObject Canvas;

    private Life _life;
    private Animator _anim;
    private BoxCollider2D _collider;
    private Rigidbody2D _rig;
    private Weapon _weapon;
    
    /// <summary>
    /// Add listener whenever the enemy dies and finds the player onSight
    /// </summary>
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

    /// <summary>
    /// Spawn a bullet and triggers a mini animation of a Muzzeflash
    /// </summary>
    void ShootTarget()
    {
        if(_life.Amount>0)
        {
            if(_weapon.ShootBullet("EnemyBullet", 0.1f)) _weapon.ToggleMuzzle();
        }
        
    }

    /// <summary>
    /// Aligns the Y position of the enemy in order to match the death animation with the floor.
    /// </summary>
    /// <returns>Wait for next frame</returns>
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

    /// <summary>
    /// Deactivate colliders and add points to the ScoreManager
    /// </summary>
    void Die()
    {
        StartCoroutine( GoDownInYForDeathAnim());
        PlayPointsAnimation();
        _rig.isKinematic = true;
        _collider.enabled = false;
        _anim.SetTrigger("IsDeath");
        sight.gameObject.SetActive(false);
        ScoreManager.SharedInstance.PointsObtained+=points;
    }

    /// <summary>
    /// Esthetic animation for feedback and show the points gained from the enemy 
    /// </summary>
    void PlayPointsAnimation()
    {
        text.text = (points>0 ? $"+{points}": "");
        var seq = DOTween.Sequence();
        seq.Append(Canvas.transform.DOMoveY(Canvas.transform.position.y+.6f,1.5f));
        seq.Join(text.DOFade(1,1f));
        seq.Append(text.DOFade(0,1f));
    }

}
