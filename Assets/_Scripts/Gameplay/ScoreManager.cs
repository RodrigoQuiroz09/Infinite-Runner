using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager SharedInstance;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI scoreText;

    [HideInInspector]public float DistanceTraveled;
    [HideInInspector]public float PointsObtained;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    /// <summary>
    /// Reset score points and the distance.
    /// </summary>
    public void HandleStart()
    {
        DistanceTraveled=0;
        PointsObtained=0;
    }

    /// <summary>
    /// Every second it add 5 to the points and distance.
    /// Updates the text in the Gameplay UI.
    /// </summary>
    public void HandleUpdate()
    {
        DistanceTraveled+=Time.deltaTime*5;
        PointsObtained+=Time.deltaTime*5;

        scoreText.text=$"{Mathf.CeilToInt(PointsObtained)}";
        distanceText.text=$"<color=#9A9A9A>{Mathf.CeilToInt(DistanceTraveled)}<size=20>m</size></color>";
    }
}
