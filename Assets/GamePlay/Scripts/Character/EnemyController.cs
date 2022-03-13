using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : Character,IInitializeVariables
{

    #region Parameter
    public NavMeshAgent agent;
    public Vector3 EnemyDestination;
    float stopTimeCounting,StandingTime;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (GameManager.Instance.gameState==GameManager.GameState.gameStarted)
        {
            stopTimeCounting += Time.deltaTime;
            if (stopTimeCounting > StandingTime)
            {
                Moving();
                stopTimeCounting = 0;
                StandingTime = Random.Range(3f, 10f);
            }
            else if (Vector3.Distance(transform.position, EnemyDestination) < 0.1f)
            {
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
    }
    void Moving()
    {
        EnemyDestination = new Vector3(Random.Range(-24f, 24f), 0, Random.Range(-18.5f, 18.5f)); //Find the random position
        agent.SetDestination(EnemyDestination);
        OnRun();
    }
    void StopMoving()
    {
        agent.SetDestination(transform.position);
        OnIdle() ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            EnemyMovement();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            EnemyMovement();
        }
    }

    public void InitializeVariables()
    {
        stopTimeCounting = 5f;
        StandingTime = 3f;
        weaponListCreate(); //Khởi tạo danh sách vũ khí
        EnemyMovement();
    }
}
