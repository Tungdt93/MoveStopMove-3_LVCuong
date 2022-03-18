using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 TargetPos,OwnerAttackPos;
    private float bulletSpeed=10f;
    private float AttackRange;
    private Rigidbody _bullet;
    [SerializeField]private int OwnerID, OpponentID;
    void Start()
    {
        BulletMove();
    }
    private void Update()
    {
        if (Vector3.Distance(OwnerAttackPos, transform.position) > AttackRange)
        {
            _bullet.velocity = Vector3.zero;
            Pooling.instance._Push(gameObject.tag, gameObject);
        }
    }
    public void BulletMove()
    {
        _bullet = GetComponent<Rigidbody>();
        Vector3 dirrect = TargetPos - transform.position;
        //transform.Translate(dirrect.normalized * Time.deltaTime* bulletSpeed, Space.World);
        //_bullet.AddForce(dirrect.normalized * bulletSpeed);
        _bullet.velocity=dirrect.normalized * bulletSpeed;
        transform.LookAt(TargetPos);
        
    }

    public void SetID(int _ownerID,int _oppenentID,float _attackRange)
    {
        OwnerID = _ownerID;
        OpponentID = _oppenentID;
        AttackRange = _attackRange;
        FindTarget();
    }

    public void FindTarget()
    {
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.GetInstanceID() == OpponentID && character.gameObject.activeSelf)
            {
                TargetPos = character.transform.position;
                TargetPos.y = 1f;
            }
            else if(character.GetInstanceID() == OwnerID)
            {
                OwnerAttackPos = character.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetInstanceID() == OpponentID)
        {
            other.gameObject.GetComponent<IHit>().OnHit();
            AddOwnerLevel();
            _bullet.velocity = Vector3.zero;
            Pooling.instance._Push(gameObject.tag, gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            _bullet.velocity = Vector3.zero;
            Pooling.instance._Push(gameObject.tag, gameObject);
        }
    }
    void AddOwnerLevel()
    {
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.GetInstanceID() == OwnerID && character.gameObject.activeSelf)
            {
                if (character.CompareTag("Enemy"))
                {
                    if (character.GetComponent<EnemyController>().IsDeath == false)
                    {
                        character.GetComponent<EnemyController>().AddLevel();
                        Debug.Log("EnemyLevel=" + character.GetComponent<EnemyController>().EnemyLevel);
                    }
                }
                else if (character.CompareTag("Player"))
                {
                    if (character.GetComponent<PlayerController>().IsDeath == false)
                    {
                        character.GetComponent<PlayerController>().AddLevel();
                    }
                }
            }
        }
    }
}
