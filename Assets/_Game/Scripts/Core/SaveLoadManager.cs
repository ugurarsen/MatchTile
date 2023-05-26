using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 1);

    #endregion

    #region COIN

    const string KEY_COIN = "coins";

    public static void AddCoin(int add) => PlayerPrefs.SetInt(KEY_COIN, GetCoin() + add);
    public static int GetCoin() => PlayerPrefs.GetInt(KEY_COIN, 0);

    #endregion

}