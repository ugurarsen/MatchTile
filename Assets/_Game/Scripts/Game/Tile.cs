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
    public int spriteID
    {
        get => spriteID;
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

    public void GoToTarget(Transform target)
    {
        transform.DOKill();
        transform.DOMove(target.position, Configs.Tile.duration);
    }
    
#if UNITY_EDITOR
    [ButtonMethod]
    private void UpdateImage()
    {
        spriteID = currentSpriteID;

    }
#endif
}
