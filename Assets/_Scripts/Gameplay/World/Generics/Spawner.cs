using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Spawner : MonoBehaviour
{
    [Tooltip("Positions where the prefab will appear")]
    [SerializeField] 
    Transform [] positions;
    [Tooltip("Object to Instatiate (Prefab)")]
    [SerializeField] 
    GameObject prefab;

    //Tracker of Objects spawned
    public List<GameObject> ObjectsSpawned;

    /// <summary>
    /// Instatiate a prefab with a certain probability on each position declared.
    /// </summary>
    public void InstantiateObj()
    {
        foreach (var pos in positions)
        {
            if(Random.Range(0,99)<30) ObjectsSpawned.Add(Instantiate(prefab,pos));
        }

    }

    /// <summary>
    /// Clears all the GameObjects spawned
    /// </summary>
    public void ResetObj()
    {
        foreach (var obj in ObjectsSpawned)
        {
            Destroy(obj);
        }
        ObjectsSpawned.Clear();
    }
}
