using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGameSceneDimPanel : MonoBehaviour
{
    private CanvasGroup dimPanelCanvasGroup;

    void Start()
    {
        dimPanelCanvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup(dimPanelCanvasGroup,
        1f,
        0f,
        0.5f));
    }

    void FadeDimPanel()
    {
        StartCoroutine(FadeCanvasGroup(dimPanelCanvasGroup,
        dimPanelCanvasGroup.alpha,
        0f,
        0.5f));
    }

    IEnumerator
    FadeCanvasGroup(
        CanvasGroup canvasGroup,
        float startAlpha,
        float endAlpha,
        float duration
    )
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha =
                Mathf.Lerp(startAlpha, endAlpha, timeElapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
