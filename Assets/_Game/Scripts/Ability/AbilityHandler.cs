using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHandler : Singleton<AbilityHandler>
{
    public Button mixTilesButton, dropBackButton, dropTwoButton;
    public TextMeshProUGUI mixTilesText, dropBackText, dropTwoText;
    public GameObject mixTilesRedPoint, dropBackRedPoint, dropTwoRedPoint;

    private void Start()
    {
        UpdateDropBack();
        UpdateDropTwo();
        UpdateMixTiles();
    }

    public void MixTiles()
    {
        if (SaveLoadManager.GetMixTiles() <= 0) return;

        LevelHandler.I.GetLevel().MixTiles();
        SaveLoadManager.AddMixTiles(-1);
        UpdateMixTiles();
    }
    
    public void DropBack()
    {
        if (SaveLoadManager.GetDropBack() <= 0 || MatchingArea.I.lastTile == null) return;
        MatchingArea.I.DropBackLastTile();
        SaveLoadManager.AddDropBack(-1);
        UpdateDropBack();
    }

    public void DropTwo()
    {
        if (SaveLoadManager.GetDropTwo() <= 0 || MatchingArea.I.DropFirstTwoTile()) return;
        
        SaveLoadManager.AddDropTwo(-1);
        UpdateDropTwo();
    }
    
    public void UpdateMixTiles()
    {
        int abilityCount = SaveLoadManager.GetMixTiles();
        if (abilityCount > 0)
        {
            mixTilesText.text = abilityCount.ToString();
        }else
        {
            mixTilesRedPoint.gameObject.SetActive(false);
        }
    }
    
    public void UpdateDropBack()
    {
        int abilityCount = SaveLoadManager.GetDropBack();
        if (abilityCount > 0)
        {
            dropBackRedPoint.gameObject.SetActive(true);
            dropBackText.text = abilityCount.ToString();
        }
    }
    
    public void UpdateDropTwo()
    {
        int abilityCount = SaveLoadManager.GetDropTwo();
        if (abilityCount > 0)
        {
            dropTwoRedPoint.gameObject.SetActive(true);
            dropTwoText.text = abilityCount.ToString();
        }
    }

    public void OpenAllButtons()
    {
        mixTilesButton.gameObject.SetActive(true);
        dropBackButton.gameObject.SetActive(true);
        dropTwoButton.gameObject.SetActive(true);
    }
    
    // Aslında ineractable false yapmak da olabilirdi ama referans oyunlarda böyle kullanılmış.
}
