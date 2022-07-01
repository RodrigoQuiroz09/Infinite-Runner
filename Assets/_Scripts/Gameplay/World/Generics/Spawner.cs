using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Spawner : MonoBehaviour
{
    [SerializeField] Transform [] positions;
    [SerializeField] GameObject prefab;
    public List<GameObject> ObjectsSpawned;

    public void InstantiateObj()
    {
        foreach (var pos in positions)
        {
            if(Random.Range(0,99)<30) ObjectsSpawned.Add(Instantiate(prefab,pos));
        }

    }

    public void ResetObj()
    {
        foreach (var obj in ObjectsSpawned)
        {
            Destroy(obj);
        }
        ObjectsSpawned.Clear();
    }
}
