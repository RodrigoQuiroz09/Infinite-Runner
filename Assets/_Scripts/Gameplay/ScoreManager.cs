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

    public float DistanceTraveled;
    public float PointsObtained;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    public void HandleStart()
    {
        DistanceTraveled=0;
        PointsObtained=0;
    }

    public void HandleUpdate()
    {
        DistanceTraveled+=Time.deltaTime*5;
        PointsObtained+=Time.deltaTime*5;

        scoreText.text=$"{Mathf.CeilToInt(PointsObtained)}";
        distanceText.text=$"<color=#9A9A9A>{Mathf.CeilToInt(DistanceTraveled)}<size=20>m</size></color>";
    }
}
