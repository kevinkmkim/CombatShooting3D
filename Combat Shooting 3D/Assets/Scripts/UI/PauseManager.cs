using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private GameObject dimPanel;
    [SerializeField] private CanvasGroup dimPanelCanvasGroup;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private CanvasGroup pausePanelCanvasGroup;
    #endregion

    #region Properties
    private Button pauseButton;
    public static bool isPaused = false;
    #endregion

    void OnEnable()
    {
        pauseButton = GetComponent<Button>();
    }

    public void OnPauseButtonClick()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = pauseSprite;
            isPaused = false;
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = false;
            }
            HidePausePanel();
        }
        else
        {
            Time.timeScale = 0;
            pauseButton.image.sprite = startSprite;
            isPaused = true;
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = true;
            }
            ShowPausePanel();
        }
    }

    private void ShowPausePanel()
    {
        dimPanel.SetActive(true);
        pausePanel.SetActive(true);
        StartCoroutine(MainUIManager
            .FadePanel(dimPanelCanvasGroup,
            dimPanelCanvasGroup.alpha,
            1,
            2 / 5));
        StartCoroutine(MainUIManager
            .FadePanel(pausePanelCanvasGroup,
            pausePanelCanvasGroup.alpha,
            1,
            2 / 5));
    }

    private void HidePausePanel()
    {
        StartCoroutine(MainUIManager
            .FadePanel(dimPanelCanvasGroup,
            dimPanelCanvasGroup.alpha,
            0,
            2 / 5));
        StartCoroutine(MainUIManager
            .FadePanel(pausePanelCanvasGroup,
            pausePanelCanvasGroup.alpha,
            1,
            2 / 5));
        dimPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    IEnumerator UnpauseCountDown()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
