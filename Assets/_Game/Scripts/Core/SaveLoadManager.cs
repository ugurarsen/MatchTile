using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 1);

    #endregion

    #region STAR

    const string KEY_STAR = "stars";

    public static void AddStar(int add) => PlayerPrefs.SetInt(KEY_STAR, GetStar() + add);
    public static int GetStar() => PlayerPrefs.GetInt(KEY_STAR, 0);

    #endregion

}