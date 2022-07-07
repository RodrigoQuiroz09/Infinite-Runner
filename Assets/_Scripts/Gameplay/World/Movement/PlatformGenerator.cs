using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator SharedInstance;
    
    //Trigger for the next spawned platform
    public UnityAction OnPlatformLimit;
    [Tooltip("Platforms to generate")][SerializeField] List<MovePlatform> InitialPlatforms; 
    //List of platforms derived from InitialPlatforms that enables the spawning of them
    [Tooltip("List to manage platforms")]public List<MovePlatform> platforms;
    
    //Random generated position for the list of platfoms
    int listPos=0;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    /// <summary>
    /// Listener for the spawing of the next platform
    /// </summary>
    void Start()
    {
        OnPlatformLimit+=SpawnPlatform;
    }

    /// <summary>
    /// Reset the platform generation list
    /// </summary>
    public void RestartPlatforms()
    {
        platforms.Clear();
        foreach (var item in InitialPlatforms)
        {
            item.ResetPlatform();
        }
        platforms=new List<MovePlatform>(InitialPlatforms);;
    }

    /// <summary>
    /// When the Unity Action OnPlatformLimit is Invoked, 
    /// spawn a random platform from the list and activate anything needed.
    /// </summary>
    void SpawnPlatform()
    {
        if(GameManager.SharedInstance.CanMove)
        {
            listPos = Random.Range(0, platforms.Count-1);
            MovePlatform platform=platforms[listPos];
            platforms.RemoveAt(listPos);
            platform.gameObject.SetActive(true);
            platform.enemySpawner.InstantiateObj();
            platform.pickableManager.GeneratePickableObj();
            StartCoroutine( platform.GoLeft());
        }
    }
}
