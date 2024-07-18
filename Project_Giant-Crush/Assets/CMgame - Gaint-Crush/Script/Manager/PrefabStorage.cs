using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PrefabStorage : MonoBehaviour
{
    public static PrefabStorage Instance { get; private set; }


    public InputHandle inputHandle;
    public GameObject ViewFinder;
    public EnemyController CurrentEnemy;

    public Piston Piston;
    public PunchController PunchController;
    public PopupHealEnemy PopupHealEnemy;

    [Header("Data")]

    public ShopEnemySO DataShopEnemy;

    public List<EnemyController> L_Enemies;

    private void Start()
    {
        LoadEnemies((int)PlayerDataManager.getCurrentSkin());
    }

    public void LoadEnemies(int Current)
    {
        if (Current >= L_Enemies.Count) return;

        if (CurrentEnemy)
        {
            Destroy(CurrentEnemy.gameObject);
        }

        EnemyController Enemy = Instantiate(L_Enemies[Current], Vector3.zero, Quaternion.identity);
        CurrentEnemy = Enemy;
        Enemy.A_Ingame?.Invoke();
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
