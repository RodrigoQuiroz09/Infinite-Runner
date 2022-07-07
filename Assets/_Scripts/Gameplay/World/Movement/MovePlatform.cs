using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class MovePlatform : MonoBehaviour
{
    //Left limit to trigger the next platform start
    float limitPos= -25f;
    [Tooltip("Spawn where the platforms will appear")][SerializeField] Vector3 spawnPoint;
    [Tooltip("Manager for the enemy spawn")]public Spawner enemySpawner;
    [Tooltip("Manager for pickable objects")]public PickableManager pickableManager;

    private float localPos;

    /// <summary>
    /// Save the initial position for future reference for movement
    /// </summary>
    void Start() 
    {
        localPos=transform.position.x;
    }

    /// <summary>
    /// <para>
    /// Move the platform to the left base upon its actual position to the limitPos, at the speed declared on the Gameplay manager
    /// </para>
    /// <para>
    /// When reach a certain limit triggers OnPlatformLimit to respawn a new platform and reset this
    /// </para>
    /// </summary>
    /// <returns>Wait until next frame</returns>
    public IEnumerator GoLeft()
    {   //Auxiliar flag to indicate it reached a point before limit
        bool flag=true;

        while (localPos >= limitPos && gameObject.activeSelf==true)
        {
            localPos = transform.position.x;
            localPos +=( GameManager.SharedInstance.CanMove ? GameplayManager.SharedInstance.Speed * Time.deltaTime : 0);
            transform.position = new Vector3 (localPos, transform.position.y, transform.position.z);
            
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

    /// <summary>
    /// Auxiliar method to reset the platform variables such as position and clear enemies spawned
    /// </summary>
    public void ResetPlatform()
    {
        enemySpawner.ResetObj();
        gameObject.SetActive(false);
        transform.position =  spawnPoint;
        localPos=spawnPoint.x;
        
    }


}
