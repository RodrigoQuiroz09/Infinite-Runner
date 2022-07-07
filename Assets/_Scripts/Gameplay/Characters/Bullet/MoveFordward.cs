using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFordward : MonoBehaviour
{
    // Flag to enable the forward movement direction of the bullet
    [HideInInspector] public bool CanMoveForward=true;
    private SpriteRenderer _sprite;

    void Awake() 
    {
        _sprite=GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// When ever the flag CanMoveForward the bullet (depend on which entity spawn it) Move to its relative forward direction
    /// </summary>
    void Update()
    {
        if(CanMoveForward)
        {
            if(LayerMask.LayerToName(gameObject.layer) == "PlayerBullet")
            {
                _sprite.flipX=false;
                this.transform.Translate(GameplayManager.SharedInstance.Speed * Time.deltaTime*-1, 0, 0);
            }
            else if(LayerMask.LayerToName(gameObject.layer) == "EnemyBullet")
            {
                _sprite.flipX=true;
                this.transform.Translate((GameplayManager.SharedInstance.Speed - 2.5f)* Time.deltaTime, 0, 0);
            }
        }

    }
}
