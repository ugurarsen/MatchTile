using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using UA.Toolkit;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int currentSpriteID;
    public SpriteRenderer bgImage;
    public SpriteRenderer tileImage;
    public Sprite[] sprites;
    public int zIndex;
    public int slotID;
    public BoxCollider2D boxCollider2D;
    public int SpriteID
    {
        get => _spriteID;
        set
        {
            _spriteID = value;
            ChangeSprite(value);
        }
    }
    private int _spriteID;
    private Level level;
    public bool TileStatus
    {
        get => _tileStatus;
        set
        {
            if (_tileStatus != value)
            {
                _tileStatus = value;
                if (value)
                {
                    bgImage.DOColor(Color.white, .0f);
                    tileImage.DOColor(Color.white, .0f);
                    gameObject.layer = LayerMask.NameToLayer("OpenTile");
                    
                }
                else
                {
                    bgImage.DOColor(Color.grey, .0f);
                    tileImage.DOColor(Color.grey, .0f);
                    gameObject.layer = LayerMask.NameToLayer("CloseTile");
                   
                }
            }
        }
    }
    private bool _tileStatus;
    public void ChangeSprite(int id)
    {
        tileImage.sprite = sprites[id];
        tileImage.sortingOrder = zIndex*2;
        bgImage.sortingOrder = zIndex*2-1;
    }
    
    private void Start()
    {
        SpriteID = currentSpriteID;
        CheckOverlap();
        MatchingArea.I.TilesOverlapEvent += CheckOverlap;
    }

    public void OnTouch()
    {
        LevelHandler.I.GetLevel().RemoveTile(this);
        boxCollider2D.enabled = false;
        transform.DOKill();
        transform.DOMove(MatchingArea.I.slots[slotID].transform.position, Configs.Tile.duration).OnComplete((() =>
        {
            Debug.Log("SpriteID: " + SpriteID);
            MatchingArea.I.CheckMatch(SpriteID);
        }));
    }

    public void UpdateLocation()
    {
        transform.DOMove(MatchingArea.I.slots[slotID].transform.position, .2f);
    }

    public void CheckOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0f);

        if (colliders.Length > 1) // Kendi collider'ı hariç diğer colliderlar varsa
        {
            bool hasOverlap = colliders.Any(collider => collider.gameObject != gameObject);
            TileStatus = hasOverlap;

            if (hasOverlap)
            {
                int maxZ = -1;
                foreach (var collider in colliders)
                {
                    if (collider.gameObject != gameObject)
                    {
                        var tile = collider.GetComponent<Tile>();
                        if (tile != null && tile.zIndex > maxZ)
                        {
                            maxZ = tile.zIndex;
                        }
                    }
                }

                if (maxZ > zIndex)
                {
                    TileStatus = false;
                }
                else
                {
                    TileStatus = true;
                }
            }
        }
        else
        {
            TileStatus = true;
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
    private void UpdateImage()
    {
        SpriteID = currentSpriteID;
    }
#endif
}
