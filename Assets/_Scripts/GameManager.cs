using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MenuScreen, Play, GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private GameState _gameState;
    public static GameManager SharedInstance;

    void Start()
    {
        MenuManager.SharedInstance.OnPlay+=()=>
        {
            MenuManager.SharedInstance.HandleRestartMainMenu();
            _gameState = GameState.Play;
        };
        playerController.PlayerDeath+=()=>
        {
            MenuManager.SharedInstance.HandleRestartMainMenu();
            _gameState = GameState.GameOver;
        };
        EffectPickableFactory.InitFactory();
    }

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
        _gameState = GameState.MenuScreen;
    }

    void Update() 
    {

        if(_gameState == GameState.Play)
        {
            GameplayManager.SharedInstance.HandleUpdate();
        }
        if(_gameState == GameState.MenuScreen)
        {
            MenuManager.SharedInstance.HandleUpdateMainMenu();
        }
        if(_gameState == GameState.GameOver)
        {
            MenuManager.SharedInstance.HandleUpdateGameOver();
        }
    }
}
