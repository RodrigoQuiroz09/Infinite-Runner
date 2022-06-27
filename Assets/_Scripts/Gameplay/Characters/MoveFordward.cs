using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFordward : MonoBehaviour
{
    [Range(0, 20)]
    public float Speed;
    public bool CanMoveForward=true;
    void Update()
    {
        if(CanMoveForward)
        {
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }

    }
}
