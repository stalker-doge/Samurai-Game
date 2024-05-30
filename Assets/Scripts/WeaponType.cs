using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{

    public enum WeaponTypes
    {
        Sword,
        Blunt
    }
    public WeaponTypes weaponType;
    public int damage = 10;
}
