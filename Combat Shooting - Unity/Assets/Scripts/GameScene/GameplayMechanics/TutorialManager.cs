using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject dimPanel;
    [SerializeField] private TextMeshProUGUI tutorialMessage;
    #endregion

    #region Properties
    public float slowDownDuration = 2.0f;
    private float startTimeScale;
    private CanvasGroup dimPanelCanvasGroup;
    #endregion

    void Start()
    {
        dimPanelCanvasGroup = dimPanel.GetComponent<CanvasGroup>();
    }

    void StartTutorial()
    {
        dimPanel.SetActive(true);
        FadeDimPanel();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Hold down your thumbs to take close aim");
            Debug.Log("Pull the trigger to shoot");
            Debug.Log("You get 20 shots each game");
            Debug.Log("Hit the targets in your lane");
            Debug.Log("Keep your gun facing forward");
        }
    }
    
    public void StopSlowMotion()
    {
        Time.timeScale = 1.0f;
    }

    void FadeDimPanel()
    {
        StartCoroutine(FadeCanvasGroup(dimPanelCanvasGroup,
        dimPanelCanvasGroup.alpha,
        1f,
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
