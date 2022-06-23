using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovePlatform : MonoBehaviour
{
    private float x;
private float initPos;
[SerializeField]bool isInitialPlatform=false;
[SerializeField] int timeToFade=11;
    private void Start() {
        if(isInitialPlatform)
        {
            initPos=28;
            StartCoroutine(GoLeft());
        }
        else
        {
            initPos=transform.position.x;
        }
    }

    public IEnumerator GoLeft()
    {
    
        float timePassed = 0;
        while (timePassed < timeToFade)
        {
            timePassed += Time.deltaTime;
            x = transform.position.x;
            x += GameManager.SharedInstance.Speed * Time.deltaTime;
            transform.position = new Vector3 (x, transform.position.y, transform.position.z);
            yield return null;
        }
        gameObject.SetActive(false);
        transform.position = new Vector3 (initPos, transform.position.y, transform.position.z);
        PlatformGenerator.SharedInstance.platforms.Add(this);
    }


}
