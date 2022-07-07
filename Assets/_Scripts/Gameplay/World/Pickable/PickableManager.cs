using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 *TODO: Do more pickable objects and check the randomness spawn. For the moment is just taking the first position.
*/

public class PickableManager : MonoBehaviour
{
    [Tooltip("Spawn point where pickable objects will apear")]
    [SerializeField] PickableHolder [] spawnPoints;

    [Tooltip("Pickable tha give points")]
    [SerializeField] List<Pickable> objectsPoints;

    [Tooltip("Pickable that give health")]
    [SerializeField] List<Pickable> objectsHelth;

    [Tooltip("Array of points. MUST align with objectsPoints")]
    [SerializeField] int [] Points;

    void Start() 
    {
        GeneratePickableObj();
    }

    /// <summary>
    /// Randomly generate objects from the list. Health objects have a max of 2 per platform.
    /// </summary>
    public void GeneratePickableObj()
    {
        int counterHealth=0;
        foreach (var item in spawnPoints)
        {
            int pos=0;
        
            if(counterHealth<2)
            {
                int random=Random.Range(0,99);
                if(random<10)
                {
                    ResetPickable(item,objectsHelth[0],0);
                    counterHealth++;
                }
                else
                {
                    ResetPickable(item,objectsPoints[pos],Points[pos]);
                }
            }
            else
            {
                ResetPickable(item,objectsPoints[pos],Points[pos]);
            }
        }
    }

    /// <summary>
    /// Assing a pickable and restart values from PickableHolder with its respective points
    /// </summary>
    /// <param name="pick">The gameObject/PickableHolder assigned</param>
    /// <param name="pickable">Pickable to be assigned</param>
    /// <param name="points">Points to be added</param>
    void ResetPickable(PickableHolder pick, Pickable pickable, int points)
    {
        pick.points=points;
        pick.objectPickable=pickable;
        pick.HandleRestart();
    }
}
