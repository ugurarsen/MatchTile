public class GameManager : Singleton<GameManager>
{
    public static bool isRunning = false;
    public static void OnStartGame()
    {
        isRunning = true;
        UIManager.I.OnGameStarted();
    }

    public static void OnLevelCompleted()
    {
        isRunning = false;
        UIManager.I.OnSuccess();
        
    }

    public static void OnLevelFailed()
    {
        isRunning = false;
        UIManager.I.OnFail();
    }

    public static void ReloadScene(bool isSuccess)
    {
        if (isSuccess)
        {
            SaveLoadManager.IncreaseLevel();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSceene");
        }
        
    }
}