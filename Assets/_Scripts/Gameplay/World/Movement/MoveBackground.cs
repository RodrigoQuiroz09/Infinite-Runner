using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code extracted from a parallax package and adapted to the game. For more information of the original code check the next link to the asset store.
/// <see href="link">
/// https://assetstore.unity.com/packages/2d/textures-materials/nature/free-pixel-art-forest-133112#content
/// </see>
/// </summary>
public class MoveBackground : MonoBehaviour
{
	//Variable to escalate speed according to its layer
	[Tooltip("Speed to match the gameplay speed")][SerializeField] float relativeSpeed;
	[SerializeField] float PontoDeDestino;
	[SerializeField] float PontoOriginal;
	

	[Tooltip("Initial velocity of the background")][SerializeField] float speed;
	float localPos;

	/// <summary>
    /// Code for parallax movement. Reaches a limit and restart position.
    /// </summary>
	void Update () 
	{
		if (GameManager.SharedInstance.CanMove)
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
