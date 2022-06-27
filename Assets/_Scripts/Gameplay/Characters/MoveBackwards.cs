using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwards : MonoBehaviour
{
    public IEnumerator GoLeft(float localPos,float limitPos)
    {

        while (localPos >= limitPos)
        {
            localPos=gameObject.transform.position.x;
            localPos += GameplayManager.SharedInstance.Speed * Time.deltaTime;
            gameObject.transform.position = new Vector3 (localPos, gameObject.transform.position.y, gameObject.transform.position.z);
            yield return null;
        }
    }
}
