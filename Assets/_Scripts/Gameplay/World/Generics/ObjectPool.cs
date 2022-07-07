using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance; 

    [Tooltip("Prefab to spawn")]public GameObject prefab;

    [Tooltip("List of objects spawned")]public List<GameObject> pooledObjects;
    [Tooltip("Amount of prefabs to spawn")]public int amountToPool;


    private void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    /// <summary>
    /// <para>
    /// Instatiate the prefab and assign it to the list.
    /// </para>
    /// <para>
    /// Extra. Assigned a parent for organization of GameObjects in the scene
    /// </para>
    /// </summary>
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(prefab);
            tmp.transform.parent = gameObject.transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    /// <summary>
    /// Get the first item of the pool (list of prefabs)
    /// </summary>
    /// <returns>GameObject from the list</returns>
    public GameObject GetFirstPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

}
