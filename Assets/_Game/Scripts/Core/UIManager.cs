using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UA.Toolkit;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    
    public float fadeInOutTime = .2f;
    [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Images")]
    [SerializeField] Images img;
    [Header("Buttons")]
    [SerializeField] Buttons btn;
    [Header("Texts")]
    [SerializeField] Texts txt;

    public Slider complateSlider;
    private CanvasGroup activePanel = null;

    public Panels GetPanel() => pnl;
    public Buttons GetButtons() => btn;
    
    private void Start()
    {
        UpdateTexts();
        StartGame();
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
    
    public void LevelCompleteAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(complateSlider.DOValue(1f, 0.3f))
            .Append(img.star.DOColor(Color.yellow, 0.2f))
            .Append(img.star.transform.DOMove(img.mainStar.transform.position, 0.4f))
            .Append(img.star.transform.DOPunchScale(Vector3.zero, 0.1f).SetEase(Ease.InOutBounce))
            .OnStart(() =>
            {
                SaveLoadManager.AddStar(1);
                UpdateStarText();
            })
            .Append(img.mainStar.transform.DOPunchScale(Vector3.one, 0.1f).SetEase(Ease.InOutBounce));

        sequence.Play();

        new DelayedAction((() =>
        {
            GameManager.OnLevelCompleted();
        }), 1f).Execute(this);
    }

    public void OnSuccess()
    {
        FadeInAndOutPanels(pnl.success);
        btn.nextLevel.gameObject.SetActive(true);
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
        UpdateStarText();
    }

    public void UpdateStarText()
    {
        txt.starText.text = SaveLoadManager.GetStar().ToString();
    }

    [System.Serializable]
    public class Panels
    {
        public CanvasGroup gameIn, success, fail;
    }

    [System.Serializable]
    public class Images
    {
        public Image star, mainStar;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button nextLevel, reTry;
    }

    [System.Serializable]
    public class Texts
    {
        public TextMeshProUGUI level, starText;
    }
}
