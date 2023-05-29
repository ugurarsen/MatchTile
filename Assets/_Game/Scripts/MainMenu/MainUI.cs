using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI starText;

    private void Start()
    {
        levelText.text = SaveLoadManager.GetLevel().ToString();
        starText.text = SaveLoadManager.GetStar().ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
