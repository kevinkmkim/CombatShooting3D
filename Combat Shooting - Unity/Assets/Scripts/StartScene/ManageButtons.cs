using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
{
    public void OnSettingButtonClick()
    {
    }

    public void OnClassicButtonClick()
    {
        GameManager.currentMode = GameManager.Mode.Classic;
        StartCoroutine(WaitAndLoadScene());
    }

    public void OnLongrangeButtonClick()
    {
        GameManager.currentMode = GameManager.Mode.Longrange;
        StartCoroutine(WaitAndLoadScene());
    }

    public void OnMarathonButtonClick()
    {
        GameManager.currentMode = GameManager.Mode.Marathon;
        StartCoroutine(WaitAndLoadScene());
    }

    public void OnMultiplayerButtonClick()
    {
        GameManager.currentMode = GameManager.Mode.Multiplayer;
        StartCoroutine(WaitAndLoadScene());
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("GameScene");
    }
}
