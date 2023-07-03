using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonManager : MonoBehaviour
{
    public void OnRestartClick()
    {
        SceneManager.LoadScene("GameScene");
        if (PauseManager.isPaused)
        {
            PauseManager.isPaused = false;
        }
        ResetTimeScale();
    }

    public void OnHomeClick()
    {
        SceneManager.LoadScene("StartScene");
        if (PauseManager.isPaused)
        {
            PauseManager.isPaused = false;
        }
        ResetTimeScale();
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }
}
