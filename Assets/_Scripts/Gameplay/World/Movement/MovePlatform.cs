using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class MovePlatform : MonoBehaviour
{
    float limitPos= -25f;
    [SerializeField] Vector3 spawnPoint;
    public Spawner enemySpawner;
    public PickableManager pickableManager;

    private float localPos;

    void Start() 
    {
        localPos=transform.position.x;
    }

    public IEnumerator GoLeft()
    {
        bool flag=true;
        while (localPos >= limitPos && gameObject.activeSelf==true)
        {
            localPos = transform.position.x;
            localPos +=( GameManager.SharedInstance.CanMove ? GameplayManager.SharedInstance.Speed * Time.deltaTime : 0);
            transform.position = new Vector3 (localPos, transform.position.y, transform.position.z);
           
               //Debug.Log(GameplayManager.SharedInstance.Speed+gameObject.name); 
            
            if(localPos < limitPos+25 && flag)
            {
                PlatformGenerator.SharedInstance.OnPlatformLimit?.Invoke();
                flag=false;
            }
            if(localPos < limitPos)
            {
                ResetPlatform();
                PlatformGenerator.SharedInstance.platforms.Add(this);
                break;
            }
        
            yield return null;

        }
    }

    public void ResetPlatform()
    {
        enemySpawner.ResetObj();
        gameObject.SetActive(false);
        transform.position =  spawnPoint;
        localPos=spawnPoint.x;
        
    }


}
