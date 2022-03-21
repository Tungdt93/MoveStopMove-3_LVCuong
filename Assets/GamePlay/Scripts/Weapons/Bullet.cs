using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 TargetPos,OwnerAttackPos;
    private float bulletSpeed;
    private float AttackRange;
    private Rigidbody _bullet;
    [SerializeField]private Animator anim;
    [SerializeField]private int OwnerID, OpponentID;
    void Start()
    {
        anim = GetComponent<Animator>();
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
        _bullet.velocity=dirrect.normalized * bulletSpeed;
        transform.LookAt(TargetPos);
    }

    public void SetID(int _ownerID,int _oppenentID)
    {
        OwnerID = _ownerID;
        OpponentID = _oppenentID;
        GetPower(OwnerID);
        FindTarget();
    }
    
    void GetPower(int _ownerID)
    {
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.GetInstanceID() == _ownerID && character.gameObject.activeSelf)
            {
                if (character.CompareTag("Enemy"))
                {
                    if (character.GetComponent<EnemyController>().IsDeath == false)
                    {
                        AttackRange = character.GetComponent<EnemyController>().AttackRange;
                        bulletSpeed = character.GetComponent<EnemyController>().AttackSpeed;
                        transform.localScale = character.transform.localScale;
                    }
                }
                else if (character.CompareTag("Player"))
                {
                    if (character.GetComponent<PlayerController>().IsDeath == false)
                    {
                        AttackRange = character.GetComponent<PlayerController>().AttackRange;
                        bulletSpeed = character.GetComponent<PlayerController>().AttackSpeed;
                        transform.localScale = character.transform.localScale;
                    }
                }
            }
        }
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
