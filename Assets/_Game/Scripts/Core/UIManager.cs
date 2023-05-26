using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public enum StartButton
    {
        TapToStartText,
        StartButton,
    }
    
    public float fadeInOutTime = .2f;
    [Header("Start Button Methods")]
    [SerializeField] private StartButton startButtonMethod;
    [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Images")]
    [SerializeField] Images img;
    [Header("Buttons")]
    [SerializeField] Buttons btn;
    [Header("Texts")]
    [SerializeField] Texts txt;

    private CanvasGroup activePanel = null;

    public Panels GetPanel() => pnl;
    public Buttons GetButtons() => btn;
    
    private void Start()
    {
        UpdateTexts();
        StartGame();
    }
    
    public void Initialize()
    {
        
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
    }

    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    public void OnSuccess(bool hasPrize = true)
    {
        if(hasPrize)
        {
            btn.nextLevel.gameObject.SetActive(false);
        }
        else
        {
            btn.nextLevel.gameObject.SetActive(true);
            FadeInAndOutPanels(pnl.success);
        }
        
    }
    

    public void ReloadScene(bool isSuccess)
    {
        GameManager.ReloadScene(isSuccess);
    }

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            _out.interactable = false;
            _out.blocksRaycasts = false;

            _out.DOFade(0f, fadeInOutTime).OnComplete(() =>
            {
                _in.DOFade(1f, fadeInOutTime).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                });
            });
        }
        else
        {
            _in.DOFade(1f, fadeInOutTime).OnComplete(() =>
            {
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
       
       
    }
    

    public void UpdateTexts()
    {
        txt.level.text = SaveLoadManager.GetLevel().ToString();
        //UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        txt.coin.text = SaveLoadManager.GetCoin().ToString();
    }

    
    

    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn, success, fail;
    }
    

    [System.Serializable]
    public class Images
    {
        public Image taptoStart;
        public Image[] joystickHighlights, vibrations;
    }

    
    [System.Serializable]
    public class Buttons
    {
        public Button play, nextLevel, reTry ,getPrize;
    }

    [System.Serializable]
    public class Texts
    {
        public TextMeshProUGUI level,coin;
    }
}
