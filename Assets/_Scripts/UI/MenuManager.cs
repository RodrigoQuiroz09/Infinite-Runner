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
    [SerializeField] Text highScore;
    public UnityAction OnPlay;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    void Start() 
    {
        highScore.text=$"HIGHSCORE: <color=#feae34>{PlayerPrefs.GetInt("hs", 0)}</color> ";
    }

    public void HandleRestartMainMenu()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    
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

    public void HandleRestartGameOver()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(false);
        GameOverUI.SetActive(true);
    }


    public void HandleUpdateGameOver()
    {
         if (Input.anyKeyDown)
        {
            HandleUpdateMainMenu();
        }
    }
}
