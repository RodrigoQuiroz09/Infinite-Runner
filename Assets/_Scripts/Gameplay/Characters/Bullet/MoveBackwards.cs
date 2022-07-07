using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwards : MonoBehaviour
{

    /// <summary>
    /// Go the left in a determined distance. (Mainly for match the explosion animation, with the moving platforms)
    /// </summary>
    /// <param name="localPos">Actual position</param>
    /// <param name="limitPos">Destiny position</param>
    /// <returns>Wait for the next frame</returns>
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
