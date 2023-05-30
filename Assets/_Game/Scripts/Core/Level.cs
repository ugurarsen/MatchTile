using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UA.Toolkit;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public Transform[] dropTwo;
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

    
    public void MixTiles()
    { 
        List<Vector3> _tilePositions = new List<Vector3>(); 
        List<int> _tileZIndexes = new List<int>();
        List<Tile> _tilesToMove = new List<Tile>();

        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tempTile = tiles[i];
            _tilePositions.Add(tempTile.transform.position);
            _tileZIndexes.Add(tempTile.zIndex);
            _tilesToMove.Add(tempTile);
        }
        
        for (int i = 0; i < tiles.Count; i++)
        {
            int randomIndex = Random.Range(0, _tilesToMove.Count);
           
            _tilesToMove[randomIndex].transform.DOMove(_tilePositions[i], Random.Range(.2f, .4f));
            _tilesToMove[randomIndex].zIndex = _tileZIndexes[i];
            _tilesToMove[randomIndex].SpriteID = _tilesToMove[randomIndex].SpriteID;
            _tilesToMove.RemoveAt(randomIndex);
        }
        new DelayedAction((() => MatchingArea.I.CheckTiles()), .5f).Execute(this);
    }

}
