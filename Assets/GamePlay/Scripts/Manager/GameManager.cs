using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>,IInitializeVariables
{
    public enum GameState { gameStarted, gameOver, gameWin}
    public GameState gameState;
    public Transform player;
    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameState = GameState.gameStarted;
        }
        if (Input.GetMouseButtonDown(1))
        {
            gameState = GameState.gameOver;
        }
    }
     public void InitializeVariables()
    {
        
    }
}
