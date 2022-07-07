using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO: Audio Manager and implementations of sounds
*/

/// <summary>
/// A enumerator to simulate a state machine and indicate which state th game is.
/// </summary>
public enum GameState
{
    MenuScreen, Play, GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private GameState _gameState;
    public static GameManager SharedInstance;
    
    /// <summary>
    /// Variable used to indicate of the platforms or background can move depending of the state of the game.
    /// </summary>
    [HideInInspector] public bool CanMove 
    {
        get
        {
            return _gameState == GameState.Play ? true : false;
        }
    }

    /// <summary>
    /// Contains listeners to diferent Unity Action that marksdown the triggers that change the gamestate.
    /// And also the Pickable Factory that initiates the ScriptableObject usage
    /// </summary>
    void Start()
    {
        MenuManager.SharedInstance.OnPlay+=()=>
        {
            MenuManager.SharedInstance.HandleRestartMainMenu();
            _gameState = GameState.Play;
        };
        playerController.PlayerDeath+=()=>
        {
            playerController.gameObject.SetActive(false);
            MenuManager.SharedInstance.HandleRestartGameOver();
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

    /// <summary>
    /// The main update that handles the different types of updates needed depending the state of the game
    /// </summary>
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
