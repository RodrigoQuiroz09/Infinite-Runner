using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateGameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI newRecord;
    [SerializeField] PlayerController playerController;

    private void Start() {
    
        playerController.PlayerDeath+=UpdateHighScore;
    }

    void UpdateHighScore()
    {

        int pointsObtained=Mathf.CeilToInt(ScoreManager.SharedInstance.PointsObtained);
        float oldHighscore=PlayerPrefs.GetInt("hs", 0); //hs -> Highscore
        highScore.text=$"Score: {pointsObtained}";
        
        if(pointsObtained>oldHighscore)   
        {
            newRecord.text="New highscore!";
            PlayerPrefs.SetInt("hs", pointsObtained);
            PlayerPrefs.Save();
            return;
        }
        newRecord.text="";
    }
}
