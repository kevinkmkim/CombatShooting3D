using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float slowDownDuration = 2.0f;

    private float startTimeScale;

    [SerializeField]
    private GameObject dimPanel;

    private CanvasGroup dimPanelCanvasGroup;

    [SerializeField]
    private TextMeshProUGUI tutorialMessage;

    void Start()
    {
        dimPanelCanvasGroup = dimPanel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
    }

    void StartTutorial()
    {
        // StartCoroutine(WaitAndPlayTutorial());
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
        // PlayerPrefs.SetInt("IsFirstTime", false);
        // PlayerPrefs.Save();
    }

    void WaitAndPlayTutorial()
    {
        // yield return new WaitForSeconds(2.0f);
        // Time.timeScale = 0;
        // yield return new WaitForSeconds(2.0f);
        // Time.timeScale = 1;
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
