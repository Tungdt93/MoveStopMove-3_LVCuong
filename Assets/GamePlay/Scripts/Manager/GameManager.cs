using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>,IInitializeVariables
{
    public enum GameState { gameUI, gameStarted, gameOver, gameWin }
    public GameState gameState;
    public ArrayList CharacterList = new ArrayList();
    public Transform[] Obstacle;
    public int TotalCharacterAmount, IsAliveAmount, KilledAmount, TotalCharAlive, SpawnAmount;
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Awake()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        IsAliveAmount = IsAliveCounting();
        TotalCharAlive = SpawnAmount + IsAliveAmount;
        if (IsAliveAmount==1) gameState = GameState.gameWin;
    }
    public void InitializeVariables()
    {
        gameState = GameState.gameUI;
        TotalCharacterAmount = 50;
        KilledAmount = 0;
    }

    public int IsAliveCounting() //Số character đang có trên map
    {
        int IsAliveAmount=0;
        foreach (GameObject character in CharacterList)
        {
            if (character.activeSelf)
            {
                if (character.CompareTag("Enemy"))
                {
                    if (character.GetComponent<EnemyController>().IsDeath == false)
                    {
                        IsAliveAmount++;
                    }
                }
                else if (character.CompareTag("Player"))
                {
                    if (character.GetComponent<PlayerController>().IsDeath == false)
                    {
                        IsAliveAmount++;
                    }
                }
            }
        }
        return IsAliveAmount;
    }
}
