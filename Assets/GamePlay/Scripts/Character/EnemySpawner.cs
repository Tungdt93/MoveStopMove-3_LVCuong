using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IInitializeVariables
{
    private enum SpawnArea {BottomLeft, BottomRight , UpLeft , UpRight}
    private SpawnArea AreaToSpawn;
    private int CharacterOnMapAmount;
    private int BottomLeft, BottomRight, UpLeft, UpRight, minAmount;  //Dùng biến này để xác định số lượng Character ở từng vùng trong map
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        FindAreaToSpawn();
    }

    public void InitializeVariables()
    {
        CharacterOnMapAmount = 7; //Mặc định trên map chỉ có 7 Character
    }

    #region Find the Area to Spawn Enemy
    void FindAreaToSpawn()
    {
        UpRight = 0;
        UpLeft = 0;
        BottomRight = 0;
        BottomLeft = 0;
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.CompareTag("Enemy"))
            {
                if (character.transform.position.x >= 0 && character.transform.position.z >= 0) UpRight++;
                else if (character.transform.position.x < 0 && character.transform.position.z > 0) UpLeft++;
                else if (character.transform.position.x > 0 && character.transform.position.z < 0) BottomRight++;
                else if (character.transform.position.x < 0 && character.transform.position.z < 0) BottomLeft++;
            }
            else if (character.CompareTag("Player")) //Mục đích không cho Enemy Spawn ở gần Player.
            {
                if (character.transform.position.x >= 0 && character.transform.position.z >= 0) UpRight=GameManager.Instance.TotalCharacterAmount;
                else if (character.transform.position.x < 0 && character.transform.position.z > 0) UpLeft = GameManager.Instance.TotalCharacterAmount;
                else if (character.transform.position.x > 0 && character.transform.position.z < 0) BottomRight = GameManager.Instance.TotalCharacterAmount;
                else if (character.transform.position.x < 0 && character.transform.position.z < 0) BottomLeft = GameManager.Instance.TotalCharacterAmount;
            }
            
        }
        minAmount = UpRight;
        AreaToSpawn = SpawnArea.UpRight;
        if (minAmount> UpLeft)
        {
            minAmount = UpLeft;
            AreaToSpawn = SpawnArea.UpLeft;
        }
        if (minAmount > BottomRight)
        {
            minAmount = BottomRight;
            AreaToSpawn = SpawnArea.BottomRight;
        }
        if (minAmount > BottomLeft)
        {
            minAmount = BottomLeft;
            AreaToSpawn = SpawnArea.BottomLeft;
        }
        if (GameManager.Instance.IsAliveAmount < CharacterOnMapAmount && GameManager.Instance.SpawnAmount > 1)
        {
            if(AreaToSpawn == SpawnArea.UpRight)
            {
                GameObject gob = Pooling.instance._Pull("Enemy", "Prefabs/Enemy");
                gob.transform.position = new Vector3(Random.Range(15f, 24f), 0, Random.Range(10f, 18.5f));
                gob.GetComponent<EnemyController>().IsDeath = false;
            }
            else if (AreaToSpawn == SpawnArea.UpLeft)
            {
                GameObject gob = Pooling.instance._Pull("Enemy", "Prefabs/Enemy");
                gob.transform.position = new Vector3(Random.Range(-24f, -15f), 0, Random.Range(10f, 18.5f));
                gob.GetComponent<EnemyController>().IsDeath = false;
            }
            else if (AreaToSpawn == SpawnArea.BottomLeft)
            {
                GameObject gob = Pooling.instance._Pull("Enemy", "Prefabs/Enemy");
                gob.transform.position = new Vector3(Random.Range(-24f, -15f), 0, Random.Range(-18.5f, -10f));
                gob.GetComponent<EnemyController>().IsDeath = false;
            }
            else if (AreaToSpawn == SpawnArea.BottomRight)
            {
                GameObject gob = Pooling.instance._Pull("Enemy", "Prefabs/Enemy");
                gob.transform.position = new Vector3(Random.Range(15f,24f), 0, Random.Range(-18.5f, -10f));
                gob.GetComponent<EnemyController>().IsDeath = false;
            }
        }
    }
    #endregion
}
