using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    
    public void AddTile(Tile tile)
    {
        if (!tiles.Contains(tile))
        {
            tiles.Add(tile);
        }
    }
    
    public void RemoveTile(Tile tile)
    {
        if (tiles.Contains(tile))
        {
            tiles.Remove(tile);
        }

        if (tiles.Count == 0)
        {
            Debug.Log("Level complete!");
            UIManager.I.LevelCompleteAnimation();
        }
    }
}
