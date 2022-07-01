using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager SharedInstance;
    [SerializeField] MovePlatform firstPlatform;
    [SerializeField] float initialSpeed;
    [SerializeField] PlayerController player;
    public float InitialSpeed=>initialSpeed;
    
    float speed;
    public float Speed=>speed;
    float timePassed = 0;

    void Start() 
    {
        MenuManager.SharedInstance.OnPlay+=HandleStart;
    }
    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
        
    }

    public void HandleUpdate()
    {
        if (timePassed > 10)
        {
               speed-=.5f;
               timePassed=0;       
        }
        timePassed += Time.deltaTime;
        player.HandleUpdate();
    }
    void HandleStart()
    {
          speed=initialSpeed;
          StartCoroutine( firstPlatform.GoLeft());
    }
}
