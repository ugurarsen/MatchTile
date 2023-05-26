using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingArea : Singleton<MatchingArea>
{
    public Slot[] slots;
    
    public List<Tile> tiles = new List<Tile>();

    public Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].tile == null)
            {
                return slots[i];
            }
        }
        Debug.Log("Matching area is full.");
        return null;
    }
    
    public void AddTile(Tile tile)
    {
        if (tiles.Contains(tile))
        {
            tiles.Add(tile);
        }
    }

    public void CheckMatches()
    {
        
    }
}
