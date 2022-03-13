using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [HideInInspector]public enum weaponType { Arrow, Axe_0, Axe_1, boomerang, candy_0, candy_1, candy_2, candy_4, Hammer, knife, uzi, Z}
    public UnityAction OnAttack;
    public UnityAction OnRun;
    public UnityAction OnIdle;
    public UnityAction OnDeath;
    public UnityAction OnWin;
    public UnityAction OnDance;
    public UnityAction OnUlti;
    [SerializeField] private Animator anim;
    public float AttackRange;
    public CharacterInfo _character;
    public Transform weaponPosition; //GameObject chứa weapon trên tay Character.
    public GameObject[] weaponArray = new GameObject[12]; //Mảng dùng để quản lý weapon trên tay Character
    public static Character CharacterAnim;
    public bool enableToAttackFlag=false;
    private void Start()
    {
        _character = GetComponent<CharacterInfo>();
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

    public void weaponSwitching(weaponType _weaponType)
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (i == (int)_weaponType)
            {
                weaponArray[i].SetActive(true);
            }
            else
            {
                weaponArray[i].SetActive(false);
            }
        }
    }
}
