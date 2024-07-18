using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    private static string DATA_ENEMY = "DATAENEMY";

    private static DATASHOPENEMY DATASHOPPLAYERMODEL;




    [Header("zz")]
    private static string Sound = "SOUND";
    private static string Music = "MUSIC";
    private static string Coin = "COIN";
    private static string CompletedTut = "CompletedTut";

    static PlayerDataManager()
    {
        DATASHOPPLAYERMODEL = JsonConvert.DeserializeObject<DATASHOPENEMY>(PlayerPrefs.GetString(DATA_ENEMY));

        if (DATASHOPPLAYERMODEL == null)
        {
            DATASHOPPLAYERMODEL = new DATASHOPENEMY();

            DATASHOPPLAYERMODEL.AddSkin(E_TypeEnemy.Zoombie);
        }

        SaveDataShopEnemy();
    }

    #region Data Shop Enemy
    private static void SaveDataShopEnemy()
    {
        string newData = JsonConvert.SerializeObject(DATASHOPPLAYERMODEL);
        PlayerPrefs.SetString(DATA_ENEMY, newData);
    }

    public static E_TypeEnemy getCurrentSkin()
    {
        return DATASHOPPLAYERMODEL.GetCurrentSKinUsing();
    }

    public static void SetCurrentSKinUsing(E_TypeEnemy id)
    {
        DATASHOPPLAYERMODEL.SetCurrentSKinUsing(id);

        SaveDataShopEnemy();
    }

    public static List<E_TypeEnemy> GetListSkinOnwed()
    {
        return DATASHOPPLAYERMODEL.GetListSkinOnwed();
    }

    public static void AddSkin(E_TypeEnemy Skin)
    {
        DATASHOPPLAYERMODEL.AddSkin(Skin);

        SaveDataShopEnemy();
    }



    #endregion

    public static bool GetSound()
    {
        return PlayerPrefs.GetInt(Sound, 1) == 1;
    }

    public static void SetSound(bool isOn)
    {
        PlayerPrefs.SetInt(Sound, isOn ? 1 : 0);
    }

    public static bool GetMusic()
    {
        return PlayerPrefs.GetInt(Music, 1) == 1;
    }

    public static void SetMusic(bool isOn)
    {
        PlayerPrefs.SetInt(Music, isOn ? 1 : 0);
    }

    public static int GetCoin()
    {
        return PlayerPrefs.GetInt(Coin, 0);
    }

    public static void SetCoin(int QuantityCoin)
    {
        PlayerPrefs.SetInt(Coin, QuantityCoin);
    }


    public static bool GetCompletedTut()
    {
        return PlayerPrefs.GetInt(CompletedTut, 0) == 1;
    }

    public static void setCompletedTut(bool isComplete)
    {
        PlayerPrefs.SetInt(CompletedTut, isComplete ? 1 : 0);
    }

}
public class DATASHOPENEMY
{
    public E_TypeEnemy CurrentEnemy;

    public List<E_TypeEnemy> L_EnemyOwned = new List<E_TypeEnemy>();

    public E_TypeEnemy GetCurrentSKinUsing()
    {
        return CurrentEnemy;
    }

    public void SetCurrentSKinUsing(E_TypeEnemy id)
    {
        CurrentEnemy = id;
    }

    public List<E_TypeEnemy> GetListSkinOnwed()
    {
        return L_EnemyOwned;
    }

    public void AddSkin(E_TypeEnemy Skin)
    {
        if (L_EnemyOwned.Contains(Skin)) return;

        L_EnemyOwned.Add(Skin);
    }
}
