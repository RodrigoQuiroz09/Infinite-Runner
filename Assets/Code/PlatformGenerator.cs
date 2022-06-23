using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator SharedInstance;

    [SerializeField]public List<MovePlatform> platforms;
    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    float period = 4.35f;
    int listPos=0;
    void Update()
    {
        if (period > 5f)
        {
            listPos = Random.Range(0, platforms.Count);
            MovePlatform platform=platforms[listPos];
            platforms.RemoveAt(listPos);
            platform.gameObject.SetActive(true);
            StartCoroutine( platform.GoLeft());
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }
}
