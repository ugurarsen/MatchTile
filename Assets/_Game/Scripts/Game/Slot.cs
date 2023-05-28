using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public ParticleSystem particle;
    public int slotID;
    public Tile _tile;
    public Tile tile
    {
        get => _tile;
        set
        {
            if (_tile != value)
            {
                _tile = value;
                SetTileSlotID();
            }
        }
    }
    
    public void SetTileSlotID()
    {
        if (_tile != null)
        {
            _tile.slotID = slotID;
        }
    }
    
}
