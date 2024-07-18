using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Data/ShopEnemy" , fileName ="ShopEnemy")]
public class ShopEnemySO : ScriptableObject
{
    public List<SHOPENEMY> L_Enemies;

    public SHOPENEMY getCurrentEnemyShop(E_TypeEnemy _typeEnemy)
    {
        for (int i = 0; i < L_Enemies.Count; i++)
        {
            if (L_Enemies[i].TypeEnemy == _typeEnemy)
            {
                return L_Enemies[i];
            }
        }
        return null;
    }
}

[Serializable]
public class SHOPENEMY
{
    public E_TypeEnemy TypeEnemy;
    public string Name;
    public Image Icon;
    public int QuantityCoin;
}
