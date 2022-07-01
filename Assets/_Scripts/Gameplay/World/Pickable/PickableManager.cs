using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
   [SerializeField] PickableHolder [] spawnPoints;
   [SerializeField] List<Pickable> objectsPoints;
   [SerializeField] List<Pickable> objectsHelth;

    [SerializeField] int [] Points;

   private void Start() {
        GeneratePickableObj();
   }

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

   void ResetPickable(PickableHolder pick, Pickable pickable, int points)
   {
    pick.points=points;
   
    pick.objectPickable=pickable;
    pick.HandleRestart();
   
   }
}
