using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SpriteRenderer muzzleSprite;
    [SerializeField] GameObject shootingPoint;
    private Color muzzleColor;
    private float lastShootTime; 
    [Tooltip("Time until next shot")][SerializeField] float shootRate;
    
    string layer; // Layer use to asign a layermask to the bullet

    /// <summary>
    /// Turn off the muzzle flash
    /// </summary>
    void Start() 
    {
        muzzleColor=muzzleSprite.color;
        muzzleSprite.color=new Color(0,0,0,0);
    }

    /// <summary>
    /// "Animation" of firing triggering an on and off of a muzzle flash sprite
    /// </summary>
    public void ToggleMuzzle()
    {
        muzzleSprite.color=muzzleColor;
        Invoke("OffMuzzle",0.1f);
    }
    void OffMuzzle()
    {
         muzzleSprite.color=new Color(0,0,0,0);
    }

    /// <summary>
    /// Spawn a bullet from the pool depending of it´s shootRate
    /// </summary>
    /// <param name="layer">Which entity spawned the bullet</param>
    /// <param name="delay">Delay to match animations</param>
    /// <returns>If the bullet is able to spawn (shootRate)</returns>
    public bool ShootBullet(string layer, float delay)
    {
        if (Time.timeScale > 0)
        {
            this.layer = layer;
            var timeSinceLastShot = Time.time - lastShootTime;
            if (timeSinceLastShot < shootRate) return false;
            lastShootTime = Time.time;
            Invoke("FireBullet", delay);

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Get a bullet from pool an puts it in position
    /// </summary>
    void FireBullet()
    {
        var bullet = ObjectPool.SharedInstance.GetFirstPooledObject();
        bullet.layer = LayerMask.NameToLayer(layer);
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.SetActive(true);
    }

}
