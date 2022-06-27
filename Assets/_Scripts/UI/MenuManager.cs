using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    [SerializeField] Animator player;
    [SerializeField] Animator whitePlayer;
    public UnityAction OnPlay;
    [SerializeField] GameObject UI;

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
            UI.SetActive(false);
           
        }
    }
}
