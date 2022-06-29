using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SpriteRenderer muzzleSprite;
    [SerializeField] GameObject shootingPoint;
    private Color muzzleColor;
    private float lastShootTime;
    public float shootRate;
    string layer;

    void Start() {
        muzzleColor=muzzleSprite.color;
        muzzleSprite.color=new Color(0,0,0,0);
    }
    public void ToggleMuzzle()
    {
        muzzleSprite.color=muzzleColor;
        Invoke("OffMuzzle",0.1f);
    }
        void OffMuzzle()
    {
         muzzleSprite.color=new Color(0,0,0,0);
    }

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

    void FireBullet()
    {
        var bullet = ObjectPool.SharedInstance.GetFirstPooledObject();
        bullet.layer = LayerMask.NameToLayer(layer);
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.SetActive(true);
    }

}
