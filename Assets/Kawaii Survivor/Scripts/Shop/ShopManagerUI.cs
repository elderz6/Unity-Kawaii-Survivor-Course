using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private GameObject playerStatsBackground;

    private Vector2 playerStatsOpenPos;
    private Vector2 playerStatsClosedPos;

    IEnumerator Start()
    {
        yield return null;
        ConfigureStatsPanel();
    }

    private void ConfigureStatsPanel()
    {
        float width = Screen.width / (4 * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);

        playerStatsOpenPos = playerStatsPanel.anchoredPosition;
        playerStatsClosedPos = playerStatsOpenPos - Vector2.right * width;

        playerStatsPanel.anchoredPosition = playerStatsClosedPos;
        HidePlayerStats();
    }

    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsBackground.SetActive(true);
        playerStatsBackground.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsOpenPos, .3f).setEase(LeanTweenType.easeInCubic);
    }

    public void HidePlayerStats()
    {
        playerStatsBackground.SetActive(false);
        playerStatsBackground.GetComponent<Image>().raycastTarget = false;
        
        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsClosedPos, .3f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));
    }
}
