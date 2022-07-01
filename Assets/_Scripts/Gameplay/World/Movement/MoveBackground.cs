using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
	[SerializeField] float relativeSpeed;
	[SerializeField] float PontoDeDestino;
	[SerializeField] float PontoOriginal;

	bool CanMove=false;

	[SerializeField] float speed;
	float localPos;
    void Start() {
        MenuManager.SharedInstance.OnPlay+=()=>{CanMove=true;};
    }
	void Update () 
	{
		if (CanMove)
		{
			speed=GameplayManager.SharedInstance.Speed-GameplayManager.SharedInstance.InitialSpeed-relativeSpeed;
			localPos = transform.position.x;
			localPos += speed * Time.deltaTime;
			transform.position = new Vector3 (localPos, transform.position.y, transform.position.z);

			if (localPos <= PontoDeDestino)
			{
				localPos = PontoOriginal;
				transform.position = new Vector3 (localPos, transform.position.y, transform.position.z);
			}	
		}

	}

	
}
