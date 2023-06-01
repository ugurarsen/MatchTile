using DG.Tweening;
using UA.Toolkit;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public Transform hand;
    public GameObject bgBlur;
    private int _levelNo;
    private void Start()
    {
        _levelNo = SaveLoadManager.GetLevel();
        if (_levelNo > 3)
        {
            AbilityHandler.I.OpenAllButtons();
        }
        new DelayedAction((() =>
        {
            OnLevelStart();
        }), 2f).Execute(this);
    }

    public void OnLevelStart()
    {
        Debug.Log(_levelNo);
        if (_levelNo == 1)
        {
            Debug.Log("Level 1");
            DropBackTutorial();
        }else if (_levelNo == 2)
        {
            AbilityHandler.I.dropBackButton.gameObject.SetActive(true);
            MatchingArea.I.TilesOverlapEvent += DropTwoButtonTutorial;
        }else if (_levelNo == 3)
        {
            AbilityHandler.I.dropTwoButton.gameObject.SetActive(true);
            AbilityHandler.I.dropBackButton.gameObject.SetActive(true);
            MixButtonTutorial();
        }
    }

    public void DropTwoButtonTutorial()
    {
        if (MatchingArea.I.slots[1].tile != null)
        {
            new DelayedAction((() =>
            {
                if (MatchingArea.I.slots[0].tile.SpriteID != MatchingArea.I.slots[1].tile.SpriteID)
                {
                    AbilityHandler.I.dropTwoButton.gameObject.SetActive(true);
                    MatchingArea.I.slots[0].tile.zIndex = 999;
                    MatchingArea.I.slots[0].tile.SpriteID = MatchingArea.I.slots[0].tile.SpriteID;
                    MatchingArea.I.slots[1].tile.zIndex = 999;
                    MatchingArea.I.slots[1].tile.SpriteID = MatchingArea.I.slots[1].tile.SpriteID;
                    MatchingArea.I.TilesOverlapEvent -= DropTwoButtonTutorial;
                    hand.transform.position = AbilityHandler.I.dropTwoButton.transform.position;
                    OpenHand();
                    AbilityHandler.I.dropTwoButton.onClick.AddListener(() =>
                    {
                        CloseHand();
                    });
                }
            }), 1f).Execute(this);
            
        }
    }

    public void MixButtonTutorial()
    {
        AbilityHandler.I.mixTilesButton.gameObject.SetActive(true);
        bgBlur.GetComponent<SpriteRenderer>().sortingOrder = 0;
        hand.transform.position = AbilityHandler.I.mixTilesButton.transform.position;
        OpenHand();
        AbilityHandler.I.mixTilesButton.onClick.AddListener(() =>
        {
            CloseHand();
        });
    }

    public void DropBackTutorial()
    {
        Tile selectedTile = null;
        Level level = LevelHandler.I.GetLevel();
        for (int i = 0; i < level.tiles.Count; i++)
        {
            if (level.tiles[i].gameObject.layer == LayerMask.NameToLayer("OpenTile"))
            {
                selectedTile = level.tiles[i];
                selectedTile.zIndex = 999;
                selectedTile.SpriteID = selectedTile.SpriteID;
                break;
            }
        }
        if (selectedTile != null)
        {
            hand.position = Camera.main.WorldToScreenPoint(selectedTile.transform.position);
        }
        OpenHand();
        Debug.Log("DropBackTutorial");
        MatchingArea.I.TilesOverlapEvent += CloseHand;
        MatchingArea.I.TilesOverlapEvent += OpenDropBackButton;
    }

    public void OpenHand()
    {
        hand.DOScale(1, .5f).OnComplete((() =>
        {
            hand.DOScale(Vector3.one * .8f, .5f).SetLoops(-1, LoopType.Yoyo);
        }));
        hand.gameObject.SetActive(true);
        bgBlur.SetActive(true);
    }
    
    public void CloseHand()
    {
        if (hand.gameObject.activeSelf)
        {
            hand.DOKill();
            hand.DOScale(0, .5f);
            hand.gameObject.SetActive(false);
            bgBlur.SetActive(false);
        }
    }

    public void OpenDropBackButton()
    {
        AbilityHandler.I.dropBackButton.gameObject.SetActive(true);
        MatchingArea.I.TilesOverlapEvent -= OpenDropBackButton;
        MatchingArea.I.TilesOverlapEvent -= CloseHand;
        hand.transform.position = AbilityHandler.I.dropBackButton.transform.position;
        OpenHand();
        AbilityHandler.I.dropBackButton.onClick.AddListener(() =>
        {
            CloseHand();
        });
    }
    
    
}
