using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateGameOverUI : MonoBehaviour
{
    //GameOver Screen text for Highscore
    [SerializeField] TextMeshProUGUI highScore;
    //Toggle text to indicate if the player reached a new highscore
    [SerializeField] TextMeshProUGUI newRecord;
    [SerializeField] PlayerController playerController;
    
    /// <summary>
    /// Add listener to the Death of the player to update the values on the losing screen
    /// </summary>
    void Start() 
    {
        playerController.PlayerDeath+=UpdateHighScore;
    }

    /// <summary>
    /// Obtain the points from the Score Manager and round the to update the text. 
    /// Update the highscore in Playerpref if needed and toggle text for feedback of a new Record.
    /// </summary>
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
