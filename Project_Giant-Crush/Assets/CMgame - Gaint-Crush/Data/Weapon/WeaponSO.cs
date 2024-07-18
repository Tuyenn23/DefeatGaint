using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public List<WEAPON> L_weapons;
    
    public WEAPON getWeaponByType(E_TypeWeapon _typeWeapon)
    {
        for (int i = 0; i < L_weapons.Count; i++)
        {
            if (L_weapons[i].TypeEnemy == _typeWeapon)
            {
                return L_weapons[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class WEAPON
{
    public E_TypeWeapon TypeEnemy;

    public int Damage;
    public float DurationFire;
}
