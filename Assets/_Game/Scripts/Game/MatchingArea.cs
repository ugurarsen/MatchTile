using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UA.Toolkit;

public class MatchingArea : Singleton<MatchingArea>
{
    public Slot[] slots;
    public event Action TilesOverlapEvent;
    public void JoinEmptySlot(Tile selectedTile)
    {
        TilesOverlapEvent -= selectedTile.CheckOverlap;
        // Eşleşen taş varsa
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].tile != null && slots[i].tile.SpriteID == selectedTile.SpriteID)
            {
                for (int j = slots.Length - 1; j > i; j--)
                {
                    if (slots[j].tile != null)
                    {
                        slots[j + 1].tile = slots[j].tile;
                        slots[j].tile = null;
                        slots[j + 1].tile.UpdateLocation();
                    }
                }
                slots[i + 1].tile = selectedTile;
                selectedTile.slotID = i + 1;
                selectedTile.OnTouch();
                return;
            }
        }
        
        // Eşleşen taş yoksa
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile == null)
            {
                slots[i].tile = selectedTile;
                selectedTile.slotID = i;
                selectedTile.OnTouch();
                return;
            }
        }
    }

    public void CheckMatch(int spriteID)
    {
        TilesOverlapEvent?.Invoke();
        // Eşleşme kontrolü
        int matchCount = 0;
        List<Slot> matchedSlots = new List<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile == null) continue;
            if (slots[i].tile.SpriteID == spriteID)
            {
                matchCount++;
                matchedSlots.Add(slots[i]);
            }
        }

        // 3'lü eşleşme varsa
        if (matchCount >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Tile tempTile = matchedSlots[i].tile;
                matchedSlots[i].tile = null;
                tempTile.transform.DOScaleY(0, 0.2f).OnComplete(() =>
                {
                    slots[tempTile.slotID].particle.Play();
                    DestroyImmediate(tempTile.gameObject);
                });
                

            }
            new DelayedAction(() =>
            {
                MoveToSlots();
            },.4f).Execute(this);
        }
        
    }

    public void MoveToSlots()
    {
        List<Tile> allTiles = new List<Tile>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile != null)
            {
                allTiles.Add(slots[i].tile);
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < allTiles.Count)
            {
                slots[i].tile = allTiles[i];
                allTiles[i].UpdateLocation();
            }
            else
            {
                slots[i].tile = null;
            }
            
        }
    }
}
