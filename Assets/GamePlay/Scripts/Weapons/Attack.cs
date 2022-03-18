using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack: MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    private int OwnerID, OpponentID;
    private float AttackRange;
    void _HideWeapon()
    {
        foreach (Transform weapon in weaponPosition)
        {
            weapon.GetComponent<MeshRenderer>().enabled = false;
            if (weapon.gameObject.activeSelf)
            {
                weapon.GetComponent<BulletSpawner>().CreateBullet(weaponPosition.position, OwnerID, OpponentID, AttackRange);
            }
        }
    }

    void _ShowWeapon()
    {
        foreach (Transform weapon in weaponPosition)
        {
            weapon.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void SetID(int _ownerID, int _opponentID,float _attackRange)
    {
        OwnerID = _ownerID;
        OpponentID = _opponentID;
        AttackRange = _attackRange;
    }
}
