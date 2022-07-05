using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager SharedInstance;
    [SerializeField] MovePlatform firstPlatform;
    [SerializeField] float initialSpeed;
    [SerializeField] PlayerController player;
    [SerializeField] PlatformGenerator platformGenerator;

    public float InitialSpeed=>initialSpeed;

    float speed;
    public float Speed=>speed;

    float timePassed = 0;

    private Vector3 firstPlatformInitialPos;

    void Start() 
    {
        MenuManager.SharedInstance.OnPlay+=HandleStart;
        firstPlatformInitialPos=firstPlatform.gameObject.transform.position;
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
        ScoreManager.SharedInstance.HandleUpdate();
    }
    void HandleStart()
    {
        ScoreManager.SharedInstance.PointsObtained=0;
        ScoreManager.SharedInstance.DistanceTraveled=0;
        platformGenerator.RestartPlatforms();
        firstPlatform.gameObject.SetActive(true);
        firstPlatform.gameObject.transform.position=firstPlatformInitialPos;
        player.HandleStart();
        
        speed=initialSpeed;
        StartCoroutine(firstPlatform.GoLeft());
    }
}
