using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject GameOverUI;
    //Main menu Highscore text
    [SerializeField] Text highScore; 
    //Trigger to pass to GameState -> Play
    public UnityAction OnPlay; 

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    /// <summary>
    /// Initializes the highscore text from a variable stored in PlayerPrefs
    /// </summary>
    void Start() 
    {
        highScore.text=$"HIGHSCORE: <color=#feae34>{PlayerPrefs.GetInt("hs", 0)}</color> ";
    }

    /// <summary>
    /// Toggles the needed UI GameObjects to Restart the Main menu
    /// </summary>
    public void HandleRestartMainMenu()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    /// <summary>
    /// A HanldeUpdate to manage it in the GameManager that triggers the Unity Action to Play and toggles the necesary UI
    /// </summary>
    public void HandleUpdateMainMenu()
    {
         if (Input.anyKeyDown)
        {
            OnPlay?.Invoke();
            MenuUI.SetActive(false);
            GameOverUI.SetActive(false);
            InGameUI.SetActive(true);
        }
    }

    /// <summary>
    /// Toggles the needed UI GameObjects to Restart the Game Over Screen
    /// </summary>
    public void HandleRestartGameOver()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    /// <summary>
    /// A HanldeUpdate to manage it in the GameManager that, for the moment, contains the same functionality that the Update of Main Menu
    /// </summary>
    /**
     *TODO: Include 2 types of restart :
     *    1. Restart the level without going to main menu
     *    2. Go to Main Menu and then start again.
    */
    public void HandleUpdateGameOver()
    {

         if (Input.anyKeyDown)
        {
            HandleUpdateMainMenu();
        }
    }
}
