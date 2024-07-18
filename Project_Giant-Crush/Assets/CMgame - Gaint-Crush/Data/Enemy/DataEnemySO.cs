using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Enemy" , fileName = "Enemy")]
public class DataEnemySO : ScriptableObject
{
    public List<ENEMY> L_Enemies;

    public ENEMY getEnemyByType(E_TypeEnemy _Type)
    {
        for (int i = 0; i < L_Enemies.Count; i++)
        {
            if (L_Enemies[i].TypeEnemy == _Type)
            {
                return L_Enemies[i];
            }
        }

        return null;
    }
}

[Serializable]
public class ENEMY
{
    public E_TypeEnemy TypeEnemy;
    public int Hp;
    public int Reward;
}
