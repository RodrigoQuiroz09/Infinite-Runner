using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFordward : MonoBehaviour
{
    [Range(0, 20)]
    public bool CanMoveForward=true;
    private SpriteRenderer _sprite;

    private void Awake() {
        _sprite=GetComponent<SpriteRenderer>();
    }
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
