using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int currentSpriteID;
    public SpriteRenderer bgImage;
    public SpriteRenderer tileImage;
    public Sprite[] sprites;
    public int zIndex;
    public int slotID;
    public int SpriteID
    {
        get => _spriteID;
        set
        {
            if (_spriteID != value)
            {
                _spriteID = value;
                ChangeSprite(value);
            }
        }
    }
    private int _spriteID;
    public void ChangeSprite(int id)
    {
        tileImage.sprite = sprites[id];
        tileImage.sortingOrder = zIndex;
        bgImage.sortingOrder = zIndex-1;
    }
    
    private void Awake()
    {
        SpriteID = currentSpriteID;
    }
    

    public void OnTouch()
    {
        transform.DOMove(MatchingArea.I.slots[slotID].transform.position, Configs.Tile.duration).OnComplete((() =>
        {
            MatchingArea.I.CheckMatch(SpriteID);
        }));
    }

    public void UpdateLocation()
    {
        transform.DOMove(MatchingArea.I.slots[slotID].transform.position, .2f);
    }
    
#if UNITY_EDITOR
    [ButtonMethod]
    private void UpdateImage()
    {
        SpriteID = currentSpriteID;
    }
#endif
}
