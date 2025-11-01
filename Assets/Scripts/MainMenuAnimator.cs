using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour
{
    [Header("Main Buttons")]
    public RectTransform playButton;
    public RectTransform optionsButton;
    public RectTransform quitButton;

    [Header("Canvas Groups")]
    public CanvasGroup playGroup;
    public CanvasGroup optionsGroup;
    public CanvasGroup quitGroup;

    [Header("Options Menu Panel")]
    public RectTransform optionsMenu;
    public CanvasGroup optionsMenuGroup;

    [Header("Animation Settings")]
    public float moveDuration = 0.8f;
    public float fadeDuration = 0.6f;
    public float optionsSlideDuration = 0.6f;
    public Ease easeType = Ease.OutBack;

    private Vector2 playStartPos, optionsStartPos, quitStartPos;
    private Vector2 playEndPos, optionsEndPos, quitEndPos;
    private Vector2 optionsMenuHiddenPos, optionsMenuShownPos;

    private void Start()
    {
        playEndPos = playButton.anchoredPosition;
        optionsEndPos = optionsButton.anchoredPosition;
        quitEndPos = quitButton.anchoredPosition;

        playStartPos = playEndPos + new Vector2(0, 800);
        optionsStartPos = optionsEndPos + new Vector2(0, 800);
        quitStartPos = quitEndPos + new Vector2(0, -800);

        playButton.anchoredPosition = playStartPos;
        optionsButton.anchoredPosition = optionsStartPos;
        quitButton.anchoredPosition = quitStartPos;

        playGroup.alpha = 0;
        optionsGroup.alpha = 0;
        quitGroup.alpha = 0;

        optionsMenuShownPos = optionsMenu.anchoredPosition;
        optionsMenuHiddenPos = optionsMenuShownPos + new Vector2(1200, 0);
        optionsMenu.anchoredPosition = optionsMenuHiddenPos;
        optionsMenuGroup.alpha = 0;

        AnimateMainMenuIn();
    }

    public void AnimateMainMenuIn()
    {
        playButton.DOAnchorPos(playEndPos, moveDuration).SetEase(easeType);
        playGroup.DOFade(1f, fadeDuration);

        optionsButton.DOAnchorPos(optionsEndPos, moveDuration).SetEase(easeType).SetDelay(0.1f);
        optionsGroup.DOFade(1f, fadeDuration).SetDelay(0.1f);

        quitButton.DOAnchorPos(quitEndPos, moveDuration).SetEase(easeType).SetDelay(0.2f);
        quitGroup.DOFade(1f, fadeDuration).SetDelay(0.2f);
    }

    public void OnOptionsPressed()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(playButton.DOAnchorPos(playStartPos, 0.5f).SetEase(Ease.InBack));
        seq.Join(optionsButton.DOAnchorPos(optionsStartPos, 0.5f).SetEase(Ease.InBack));
        seq.Join(quitButton.DOAnchorPos(quitStartPos, 0.5f).SetEase(Ease.InBack));
        seq.Join(playGroup.DOFade(0f, 0.4f));
        seq.Join(optionsGroup.DOFade(0f, 0.4f));
        seq.Join(quitGroup.DOFade(0f, 0.4f));

        seq.AppendInterval(0.1f);
        seq.Append(optionsMenu.DOAnchorPos(optionsMenuShownPos, optionsSlideDuration).SetEase(Ease.OutCubic));
        seq.Join(optionsMenuGroup.DOFade(1f, 0.5f));
    }

    public void OnBackFromOptions()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(optionsMenu.DOAnchorPos(optionsMenuHiddenPos, optionsSlideDuration).SetEase(Ease.InCubic));
        seq.Join(optionsMenuGroup.DOFade(0f, 0.4f));
        seq.AppendInterval(0.1f);
        seq.AppendCallback(() => AnimateMainMenuIn());
    }
}
