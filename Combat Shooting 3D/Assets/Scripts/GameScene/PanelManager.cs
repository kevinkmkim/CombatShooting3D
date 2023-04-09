using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    private float fadeTime = 1.0f;

    private float targetAlpha = 1.0f;

    private float easeFactor = 0.05f;

    private float easeLength = 2f;

    [SerializeField]
    private GameObject triggerPanel;

    [SerializeField]
    private GameObject uiPanel;

    [SerializeField]
    private GameObject dimPanel;

    [SerializeField]
    private CanvasGroup dimPanelCanvasGroup;

    [SerializeField]
    private GameObject warningPanel;

    [SerializeField]
    private CanvasGroup warningPanelCanvasGroup;

    [SerializeField]
    private GameObject violationPanel;

    [SerializeField]
    private CanvasGroup violationPanelCanvasGroup;

    [SerializeField]
    private GameObject classicModePanel;

    [SerializeField]
    private CanvasGroup classicModePanelCanvasGroup;

    [SerializeField]
    private GameObject laneNumberPanel;

    [SerializeField]
    private TextMeshProUGUI classicScoreText;

    [SerializeField]
    private GameObject marathonModePanel;

    [SerializeField]
    private CanvasGroup marathonModePanelCanvasGroup;

    [SerializeField]
    private TextMeshProUGUI marathonScoreText;

    [SerializeField]
    GameManager gameEvents;

    void Start()
    {
        gameEvents.OnOutOfAmmo += HandleOutOfAmmo;
        gameEvents.OnGameOver += HandleGameOver;
        gameEvents.OnViolationOfSafetyProtocol +=
            HandleViolationOfSafetyProtocol;

        StartCoroutine(FadePanel(laneNumberPanel.GetComponent<CanvasGroup>(),
        laneNumberPanel.GetComponent<CanvasGroup>().alpha,
        0.0f,
        3.0f));
    }

    private void HandleViolationOfSafetyProtocol()
    {
        DisableGamePanels();
        violationPanel.SetActive(true);
        StartCoroutine(FadePanel(violationPanel.GetComponent<CanvasGroup>(),
        violationPanel.GetComponent<CanvasGroup>().alpha,
        1.0f,
        fadeTime));
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2.0f);
        violationPanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }

    private void HandleOutOfAmmo()
    {
        StartCoroutine(BlinkWarningPanel());
    }

    IEnumerator BlinkWarningPanel()
    {
        warningPanel.SetActive(true);
        float elapsedTime = 0.0f;
        float initialAlpha = warningPanelCanvasGroup.alpha;
        while (elapsedTime < 1f / 2.0f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (1f / 2.0f);
            warningPanelCanvasGroup.alpha = Mathf.Lerp(initialAlpha, 1, t);
            yield return null;
        }

        // Gradually decrease the alpha of the panel over time
        elapsedTime = 0.0f;
        while (elapsedTime < 2.0f / 2.0f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (2.0f / 2.0f);
            warningPanelCanvasGroup.alpha = Mathf.Lerp(1, initialAlpha, t);
            yield return null;
        }

        // Set the final alpha of the panel to the initial alpha
        warningPanelCanvasGroup.alpha = initialAlpha;
        warningPanel.SetActive(false);
    }

    private void HandleGameOver(GameManager.Mode mode)
    {
        easeTime();
        dimPanel.SetActive(true);
        StartCoroutine(FadePanel(dimPanelCanvasGroup,
        dimPanelCanvasGroup.alpha,
        targetAlpha,
        fadeTime / 5));

        switch (mode)
        {
            case GameManager.Mode.Classic:
                classicModePanel.SetActive(true);
                ShowClassicScorePanel();
                break;
            case GameManager.Mode.Longrange:
                classicModePanel.SetActive(true);
                ShowClassicScorePanel();
                break;
            case GameManager.Mode.Marathon:
                marathonModePanel.SetActive(true);
                ShowMarathonScorePanel();
                break;
            case GameManager.Mode.Multiplayer:
                // multiplayerModePanel.SetActive(true);
                ShowMultiplayerScorePanel();
                break;
            default:
                ShowClassicScorePanel();
                break;
        }
        StartCoroutine(FadePanel(triggerPanel.GetComponent<CanvasGroup>(),
        triggerPanel.GetComponent<CanvasGroup>().alpha,
        0.0f,
        fadeTime));
        StartCoroutine(FadePanel(uiPanel.GetComponent<CanvasGroup>(),
        uiPanel.GetComponent<CanvasGroup>().alpha,
        0.0f,
        fadeTime));
        StartCoroutine(DisableGamePanels());
    }

    IEnumerator DisableGamePanels()
    {
        yield return new WaitForSeconds(1.0f);
        triggerPanel.SetActive(false);
        uiPanel.SetActive(false);
    }

    IEnumerator easeTime()
    {
        Time.timeScale = easeFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        float timer = 0f;
        while (timer < easeLength)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime / easeFactor;
    }

    public static IEnumerator
    FadePanel(
        CanvasGroup canvasGroup,
        float startAlpha,
        float endAlpha,
        float fadeTime
    )
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeTime)
        {
            float alpha =
                Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeTime);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    IEnumerator EnlargePanel(GameObject panel)
    {
        // Get the initial scale of the panel
        Vector3 initialScale = new Vector3(0.5f, 0.5f, 1.0f);

        // Calculate the target scale based on the initial scale and the desired scale speed (2.0f)
        Vector3 targetScale = initialScale * 2.0f;

        // Gradually increase the scale of the panel over time
        float elapsedTime = 0.0f;
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime;
            t = EaseOutCubic(t);
            panel.transform.localScale =
                Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // Set the final scale of the panel to the target scale
        panel.transform.localScale = targetScale;
    }

    private float EaseOutCubic(float t)
    {
        t -= 1.0f;
        return t * t * t + 1.0f;
    }

    private void ShowClassicScorePanel()
    {
        StartCoroutine(FadePanel(classicModePanelCanvasGroup,
        classicModePanelCanvasGroup.alpha,
        targetAlpha,
        fadeTime));
        StartCoroutine(EnlargePanel(classicModePanel));
    }

    private void ShowMarathonScorePanel()
    {
        Debug.Log(GameManager.currentMode);
        StartCoroutine(FadePanel(marathonModePanelCanvasGroup,
        marathonModePanelCanvasGroup.alpha,
        targetAlpha,
        fadeTime));
        StartCoroutine(EnlargePanel(marathonModePanel));
    }

    private void ShowMultiplayerScorePanel()
    {
    }
}
