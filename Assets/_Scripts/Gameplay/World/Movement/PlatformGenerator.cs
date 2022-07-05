using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator SharedInstance;
    public UnityAction OnPlatformLimit;
    [SerializeField] List<MovePlatform> InitialPlatforms; 
    public List<MovePlatform> platforms;
    int listPos=0;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    void Start()
    {
        OnPlatformLimit+=SpawnPlatform;
    }

    public void RestartPlatforms()
    {
        platforms.Clear();
        foreach (var item in InitialPlatforms)
        {
            item.ResetPlatform();
        }
        platforms=new List<MovePlatform>(InitialPlatforms);;
    }

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
