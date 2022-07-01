using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    [SerializeField] Animator player;
    [SerializeField] Animator whitePlayer;
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject GameOverUI;

    public UnityAction OnPlay;

    void Awake()
    {
        if (SharedInstance != null) Destroy(this);
        SharedInstance=this;
    }

    
    public void HandleUpdate()
    {
         if (Input.anyKeyDown)
        {
            player.SetBool("IsRunning", true);
            whitePlayer.SetBool("IsRunning", true);
            OnPlay?.Invoke();
            MenuUI.SetActive(false);
            InGameUI.SetActive(true);
        }
    }
}
