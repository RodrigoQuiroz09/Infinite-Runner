using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MenuScreen, Play, LoseScreen
}

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    public static GameManager SharedInstance;

    void Start()
    {
        MenuManager.SharedInstance.OnPlay+=()=>{_gameState = GameState.Play;};
    }

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
        _gameState = GameState.Play;
    }

    void Update() 
    {

        if(_gameState == GameState.Play)
        {
            GameplayManager.SharedInstance.HandleUpdate();
        }
        if(_gameState == GameState.MenuScreen)
        {
            MenuManager.SharedInstance.HandleUpdate();
        }
        if(_gameState == GameState.LoseScreen)
        {
            
        }
    }
}
