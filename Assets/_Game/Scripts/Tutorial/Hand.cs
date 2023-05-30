using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : Singleton<Hand>
{
    public Transform hand;
    public GameObject bgBlur;

    public void Activity(bool isActive = true)
    {
        hand.gameObject.SetActive(isActive);
        bgBlur.gameObject.SetActive(isActive);
    }
    public void Click()
    {
        hand.DOPunchScale(Vector3.one, .2f, 10, -1f);
    }
}
