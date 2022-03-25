using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 TargetPos,OwnerAttackPos;
    private float bulletSpeed;
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
            DestroyBullet();
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
        if(other.gameObject.GetInstanceID() != OwnerID) //Xét xem đối tượng trúng đạn có phải Owner ko?
        {
            if (other.CompareTag("Enemy"))              //Kiểm tra xem đối tượng trúng đạn có còn sống không (Vì đối tượng còn sống lúc Owner bắn đạn nhưng có thể đã trúng đạn và chết trước khi đạn của Owner bắn đến nơi) )
            {
                if (other.GetComponent<EnemyController>().IsDeath == false)
                {
                    other.gameObject.GetComponent<IHit>().OnHit();
                    AddOwnerLevel();
                    DestroyBullet();
                }
            }
            else if (other.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerController>().IsDeath == false)
                {
                    foreach (GameObject character in GameManager.Instance.CharacterList) //Lấy tên Enemy đã giết Player
                    {
                        if (character.GetInstanceID() == OwnerID)
                        {
                            if (character.CompareTag("Enemy"))
                            {
                                other.GetComponent<PlayerController>().KillerName = character.GetComponent<EnemyController>().enemyName;
                            }
                        }
                    }
                    other.gameObject.GetComponent<IHit>().OnHit();
                    AddOwnerLevel();
                    DestroyBullet();
                }
            }
            
        }
        else if (other.CompareTag("Obstacle"))
        {
            DestroyBullet();
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
    void DestroyBullet()
    {
        _bullet.velocity = Vector3.zero;
        Pooling.instance._Push(gameObject.tag, gameObject);
    }
}
