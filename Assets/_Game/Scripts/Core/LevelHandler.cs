using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] Level testLevel;
    [SerializeField] Transform pool;
    [SerializeField] Level[] allLevels;

    Level crntLevel;

    public Level GetLevel() => crntLevel;

    private void Start()
    {
        CreateLevel();

        //InputHandler.I.Initialize(isStart: true);

    }

    public void CreateLevel()
    {
        if (testLevel == null && allLevels.Length == 0) return;

        int levelID = allLevels.Length >= 1 ? (SaveLoadManager.GetLevel()-1) % allLevels.Length : 0;

        crntLevel = Instantiate(testLevel != null ? testLevel : allLevels[levelID], pool);

        GameManager.canStart = true;
    }

}