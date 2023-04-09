using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDimPanel : MonoBehaviour
{
    private CanvasGroup dimPanelCanvasGroup;

    [SerializeField]
    private TextMeshProUGUI tipText;

    private string[]
        tipArray =
        {
            "Hold down your thumbs to take close aim",
            "Pull the trigger to shoot",
            // "You get 20 shots each game",
            "Hit the targets in your lane",
            "Keep your gun facing forward",
            "Aim higher for targets further away"
        };

    void Start()
    {
        dimPanelCanvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup(dimPanelCanvasGroup,
        dimPanelCanvasGroup.alpha,
        0f,
        0.5f));
        StartCoroutine(EnableText());

        Button classicButton =
            GameObject.Find("ClassicButton").GetComponent<Button>();
        Button longrangeButton =
            GameObject.Find("LongrangeButton").GetComponent<Button>();
        Button marathonButton =
            GameObject.Find("MarathonButton").GetComponent<Button>();

        // Button multiplayerButton =
        //     GameObject.Find("MultiplayerButton").GetComponent<Button>();
        classicButton.onClick.AddListener (FadeDimPanel);
        longrangeButton.onClick.AddListener (FadeDimPanel);
        marathonButton.onClick.AddListener (FadeDimPanel);

        // multiplayerButton.onClick.AddListener (FadeDimPanel);
    }

    IEnumerator EnableText()
    {
        yield return new WaitForSeconds(1.0f);
        int randomIndex = Random.Range(0, tipArray.Length);
        string randomTip = tipArray[randomIndex];
        tipText.text = randomTip;
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
