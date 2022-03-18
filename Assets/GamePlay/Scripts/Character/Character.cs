using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [HideInInspector]public enum weaponType { Arrow, Axe_0, Axe_1, boomerang, candy_0, candy_1, candy_2, candy_4, Hammer, knife, uzi, Z}
    [HideInInspector]public enum weaponMaterialsType { 
        Axe0, Axe0_2,
        Axe1, Axe1_2, 
        Boomerang_1, Boomerang_2, 
        Candy0_1, Candy0_2, 
        candy1_1, candy1_2, candy1_3, candy1_4, 
        Candy2_1, Candy2_2, 
        Candy4_1, Candy4_2, 
        Hammer_1, Hammer_2, 
        Knife_1, Knife_2, 
        Uzi_1, Uzi_2,
        Azure, Black,
        Blue, Chartreuse,
        Cyan, Green,
        Magenta, Orange,
        Red, Rose,
        SpringGreen, Violet,
        White, Yellow
    }
    [SerializeField] private Animator anim;
    public UnityAction OnAttack;
    public UnityAction OnRun;
    public UnityAction OnIdle;
    public UnityAction OnDeath;
    public UnityAction OnWin;
    public UnityAction OnDance;
    public UnityAction OnUlti;
    public GameObject attackScript;
    public float AttackRange;
    public float AttackSpeed;
    public CharacterInfo _character;
    public Transform weaponPosition;                        //GameObject chứa weapon trên tay Character.
    public GameObject[] weaponArray = new GameObject[12];   //Mảng dùng để quản lý weapon trên tay Character
    public WeaponInfo _weapon;
    public bool enableToAttackFlag=false;
    public float distanceToNearistEnemy;
    public Vector3 nearistEnemyPosition;
    public int opponentID;
    public bool IsDeath;

    private void Start()
    {
        _character = GetComponent<CharacterInfo>();
        _weapon = GetComponent<WeaponInfo>();
    }
    public virtual void attack()
    { 

    }

    public virtual void move() 
    {

    }

    public void _onHit()
    {

    }

    public void _onTakeDamage()
    {

    }

    public void _onDeath()
    {

    }

    public void weaponListCreate() //Thêm vũ khí vào weaponList
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            weaponArray[i] = weaponPosition.GetComponent<Transform>().transform.GetChild(i).gameObject;
        }
    }
    
    public Vector3 FindNearistEnemy(float attackRange)
    {
        distanceToNearistEnemy = 1000f;
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.GetInstanceID() != gameObject.GetInstanceID()&&Vector3.Distance(character.transform.position,gameObject.transform.position)< attackRange&&character.activeSelf)
            {
                if (character.CompareTag("Enemy"))
                {
                    if (Vector3.Distance(character.transform.position, gameObject.transform.position) < distanceToNearistEnemy && character.GetComponent<EnemyController>().IsDeath == false)
                    {
                        distanceToNearistEnemy = Vector3.Distance(character.transform.position, gameObject.transform.position);
                        nearistEnemyPosition = character.transform.position;
                        opponentID = character.GetInstanceID(); //Lấy ID của đối phương
                    }
                }
                else if (character.CompareTag("Player"))
                {
                    if (Vector3.Distance(character.transform.position, gameObject.transform.position) < distanceToNearistEnemy && character.GetComponent<PlayerController>().IsDeath == false)
                    {
                        distanceToNearistEnemy = Vector3.Distance(character.transform.position, gameObject.transform.position);
                        nearistEnemyPosition = character.transform.position;
                        opponentID = character.GetInstanceID(); //Lấy ID của đối phương
                    }
                }
            }
        }
        if (distanceToNearistEnemy > 900f) return Vector3.zero;
        else return nearistEnemyPosition;
    }

    public void AddWeaponPower()     //Truyền vào loại vũ khí Character đang cầm. Là loại nào thì sẽ cộng thêm AttackRange và AttackSpeed tương ứng vào.
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (weaponArray[i].activeSelf)
            {
                AttackRange += _weapon.AddAttackRange[i];
                AttackSpeed += _weapon.AddAttackSpeed[i];
                break;
            }
        }
    }
}
