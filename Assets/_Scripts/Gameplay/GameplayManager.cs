using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager SharedInstance;
    //Time reference variable to control whén to add more time
    float timePassed = 0;
    //Position of the first platform
    Vector3 firstPlatformInitialPos;
    //Initial platform to trigger the first movement
    [Tooltip("Initial platform")][SerializeField] MovePlatform firstPlatform;
    [SerializeField] PlayerController player;
    [SerializeField] PlatformGenerator platformGenerator;
    [Tooltip("Interval for increasing speed")][SerializeField] float timeInterval = 10f;
    [Tooltip("Amount of speed incremented")][SerializeField] float incrementSpeed = .5f;

    [Tooltip("Initial speed the platforms will move")][SerializeField] float initialSpeed;
    public float InitialSpeed=>initialSpeed;

    // Actual speed the level ig going to be playing
    float speed;
    public float Speed=>speed;

    /// <summary>
    /// Add listener to the OnPlay Unity Action.
    /// Save the position of the fisrt platform.
    /// </summary>
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

    /// <summary>
    /// Increments speed depending of timeInterval at a incrementSpeed. 
    /// Runs the HandleUpdate from different components realted to Gameplay.
    /// </summary>
    public void HandleUpdate()
    {
        if (timePassed > timeInterval)
        {
               speed-=incrementSpeed;
               timePassed=0;       
        }
        timePassed += Time.deltaTime;
        player.HandleUpdate();
        ScoreManager.SharedInstance.HandleUpdate();
    }

    /// <summary>
    /// <para>
    ///     Resets:
    /// </para>
    /// <para>
    ///     Score Manager / Platforms / Player Controller stats / Speed
    /// </para>
    /// </summary>
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
