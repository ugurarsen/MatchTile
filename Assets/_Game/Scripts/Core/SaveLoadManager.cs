using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";
    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 2);

    #endregion

    #region STAR

    const string KEY_STAR = "stars";
    public static void AddStar(int add) => PlayerPrefs.SetInt(KEY_STAR, GetStar() + add);
    public static int GetStar() => PlayerPrefs.GetInt(KEY_STAR, 0);

    #endregion

    
    #region ABILITIES
    
    const string KEY_MIX_TILES = "mixTiles";
    public static void AddMixTiles(int add) => PlayerPrefs.SetInt(KEY_MIX_TILES, GetMixTiles() + add);
    public static int GetMixTiles() => PlayerPrefs.GetInt(KEY_MIX_TILES, 5);
    
 

    const string KEY_DROP_BACK= "dropBack";
    public static void AddDropBack(int add) => PlayerPrefs.SetInt(KEY_DROP_BACK, GetDropBack() + add);
    public static int GetDropBack() => PlayerPrefs.GetInt(KEY_DROP_BACK, 5);
    

    
    const string KEY_DROP_TWO = "dropTwo";
    public static void AddDropTwo(int add) => PlayerPrefs.SetInt(KEY_DROP_TWO, GetDropTwo() + add);
    public static int GetDropTwo() => PlayerPrefs.GetInt(KEY_DROP_TWO, 5);

    #endregion
    

}