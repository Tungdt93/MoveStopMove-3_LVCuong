using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>,IInitializeVariables
{
    public enum GameState { gameStarted, gameOver, gameWin}
    public GameState gameState;
    public ArrayList CharacterList = new ArrayList();
    public Transform[] Obstacle;
    public int TotalCharacterAmount,IsAliveAmount,KilledAmount,TotalCharAlive,SpawnAmount;


    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        IsAliveAmount = IsAliveCounting(); 
        TotalCharAlive = TotalCharacterAmount - KilledAmount;
        SpawnAmount = TotalCharacterAmount - KilledAmount - IsAliveAmount;
        if (IsAliveAmount==1) gameState = GameState.gameWin;
    }
    public void InitializeVariables()
    {
        TotalCharacterAmount = 20;
        IsAliveAmount = IsAliveCounting();
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
