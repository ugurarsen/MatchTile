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
    [HideInInspector] public Tile lastTile;
    [HideInInspector] public Vector3 lastTilePosition;
    public void JoinEmptySlot(Tile selectedTile)
    {
        lastTile = selectedTile;
        lastTilePosition = selectedTile.transform.position;
        selectedTile.boxCollider2D.enabled = false;
        TilesOverlapEvent -= selectedTile.CheckOverlap;
        CheckTiles();
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
        // Eşleşme kontrolü
        int matchCount = 0;
        List<Slot> matchedSlots = new List<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile != null)
            {
                if (slots[i].tile.SpriteID == spriteID)
                {
                    matchCount++;
                    matchedSlots.Add(slots[i]);
                }
            }
        }

        // 3'lü eşleşme varsa
        if (matchCount >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Matched");
                Tile tempTile = matchedSlots[i].tile;
                matchedSlots[i].tile = null;
                tempTile.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    slots[tempTile.slotID].particle.Play();
                    Destroy(tempTile.gameObject);
                });
            }
            new DelayedAction(() =>
            {
                lastTile = null;
                MoveToSlots();
            },.4f).Execute(this);
        }

        new DelayedAction((() =>
        {
            if (slots[slots.Length-1].tile != null)
            {
                GameManager.OnLevelFailed();
            }
        }),1f).Execute(this);
    }


    public void MoveToSlots()
    {
        //Slotlardaki Tile'ları kaydır.
        List<Tile> allTiles = new List<Tile>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile != null)
            {
                allTiles.Add(slots[i].tile);
                slots[i].tile = null;
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
    
    // tileları kontrol eden event'ı çağıran bir fonksiyon yaz
    
    public void CheckTiles()
    {
        TilesOverlapEvent?.Invoke();
    }

    public void DropBackLastTile()
    {
        slots[lastTile.slotID].tile = null;
        lastTile.transform.DOMove(lastTilePosition, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            lastTile.boxCollider2D.enabled = true;
            lastTile = null;
            CheckTiles();
        });
    }

    public bool DropFirstTwoTile()
    {
        List<Tile> dropTiles = new List<Tile>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile != null)
            {
                dropTiles.Add(slots[i].tile);
                slots[i].tile = null;
                if (dropTiles.Count == 2)
                {
                    break;
                }
            }
        }

        if (dropTiles.Count >=2)
        {
            lastTile = null;
            Level level = LevelHandler.I.GetLevel();
            for (int i = 0; i < dropTiles.Count; i++)
            {
                dropTiles[i].transform.DOMove(level.dropTwo[i].position, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    dropTiles[i].boxCollider2D.enabled = true;
                    dropTiles[i].slotID = -1;
                });
            }
            new DelayedAction((() =>
            {
                CheckTiles();
                MoveToSlots();
            }),.5f).Execute(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
