using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack: MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    void _HideWeapon()
    {
        foreach (Transform weapon in weaponPosition)
        {
            weapon.GetComponent<MeshRenderer>().enabled = false;
            if (weapon.gameObject.activeSelf)
            {
                weapon.GetComponent<BulletSpawner>().CreateBullet(weaponPosition.position);
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
}
