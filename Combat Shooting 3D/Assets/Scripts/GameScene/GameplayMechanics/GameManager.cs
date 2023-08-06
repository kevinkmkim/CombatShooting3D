using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Mode
    {
        Classic,
        Longrange,
        Marathon,
        Multiplayer
    }

    public static Mode currentMode = Mode.Classic;

    [SerializeField]
    private GameObject targetPrefab;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private AudioClip gunShotAudioClip;

    private AudioSource gunShotAudioSource;

    public static int score;

    public static int remainingAmmos;

    public static int laneNumber;

    public static int activeDistance = -1;

    public static bool isGameOver;

    public static bool isPregame;

    // private bool isViolationGracePeriod;

    private float[]
        playerXPosition = { 216.8f, 236.8f, 256.8f, 276.8f, 296.8f };

    [SerializeField]
    TextMeshProUGUI laneText;

    [SerializeField]
    private GameObject armature;

    [SerializeField]
    private GameObject player;

    private Vector3 forwardDirection = Vector3.zero;

    public static HashSet<int> targetsHit = new HashSet<int>();

    private int[]
        classicModeOrder =
        {
            100,
            200,
            100,
            200,
            100,
            200,
            100,
            200,
            50,
            100,
            25,
            50,
            25,
            50,
            100,
            200,
            25,
            50,
            25,
            50
        };

    private int[]
        longrangeModeOrder =
        {
            100,
            200,
            250,
            100,
            200,
            250,
            100,
            200,
            250,
            100,
            200,
            250,
            100,
            200,
            250,
            100,
            200,
            250,
            200,
            250
        };

    public event OutOfAmmoDelegate OnOutOfAmmo;

    public delegate void OutOfAmmoDelegate();

    public event GameOverDelegate OnGameOver;

    public delegate void GameOverDelegate(Mode m);

    public event ViolationOfSafetyProtocolDelegate OnViolationOfSafetyProtocol;

    public delegate void ViolationOfSafetyProtocolDelegate();

    [SerializeField]
    TriggerManager triggerEvents;

    [SerializeField]
    Target[] targetEvents;

    void OnEnable()
    {
        triggerEvents.OnShoot += HandleShot;

        foreach (Target targetEvent in targetEvents)
        {
            targetEvent.OnTargetHit += HandleTargetHit;
        }
        score = 0;
        scoreText.text = score.ToString();
        remainingAmmos = 20;
        gunShotAudioSource = GetComponent<AudioSource>();
        gunShotAudioClip = gunShotAudioSource.clip;

        laneNumber = UnityEngine.Random.Range(1, 6);
        Vector3 playerPosition = player.transform.position;
        playerPosition.x = playerXPosition[laneNumber - 1];
        player.transform.position = playerPosition;

        laneText.text = "Lane " + laneNumber.ToString();

        isGameOver = false;
        // isViolationGracePeriod = true;
        isPregame = true;

        if (Application.systemLanguage == SystemLanguage.English)
        {
            Debug.Log(Application.systemLanguage);
        }

        Debug.Log (currentMode);
        StartCoroutine(EndViolationGracePeriod());
        StartCoroutine(EndPregame());
        switch (currentMode)
        {
            case Mode.Classic:
                StartCoroutine(PlayClassic());
                break;
            case Mode.Longrange:
                StartCoroutine(PlayLongrange());
                break;
            case Mode.Marathon:
                StartCoroutine(PlayMarathon());
                break;
            case Mode.Multiplayer:
                StartCoroutine(PlayMultiplayer());
                break;
            default:
                StartCoroutine(PlayClassic());
                break;
        }
    }

    // void Update()
    // {
    //     if (
    //         Quaternion.Angle(armature.transform.rotation, Quaternion.identity) >
    //         70 &&
    //         !isViolationGracePeriod
    //     )
    //     {
    //         isGameOver = true;
    //         OnViolationOfSafetyProtocol?.Invoke();
    //     }
    // }

    IEnumerator EndPregame()
    {
        yield return new WaitForSeconds(5.0f);
        isPregame = false;
    }

    IEnumerator EndViolationGracePeriod()
    {
        yield return new WaitForSeconds(5.0f);
        // isViolationGracePeriod = false;
        yield return new WaitForSeconds(3.0f);
    }

    private void HandleTargetHit(int targetDistance, int targetNum)
    {
        Debug.Log("HandleTargetHit");
        Debug.Log (targetDistance);
        Debug.Log (targetNum);
        if (targetNum == laneNumber)
        {
            score++;
            scoreText.text = score.ToString();
        }
        else
        {
            if (currentMode == Mode.Marathon)
            {
                isGameOver = true;
            }
        }
    }

    private void HandleShot()
    {
        if (!PauseManager.isPaused)
        {
            if (currentMode == Mode.Classic || currentMode == Mode.Longrange)
            {
                if (remainingAmmos > 0)
                {
                    remainingAmmos--;
                    Weapon.Shoot();
                    gunShotAudioSource.PlayOneShot (gunShotAudioClip);
                    VibrateDevice.Vibrate(100);
                }
                else
                {
                    OnOutOfAmmo?.Invoke();
                }
            }
            if (currentMode == Mode.Marathon)
            {
                if (!isGameOver)
                {
                    Weapon.Shoot();
                    gunShotAudioSource.PlayOneShot (gunShotAudioClip);
                    VibrateDevice.Vibrate(100);
                }
            }
        }
    }

    IEnumerator PlayClassic()
    {
        yield return new WaitForSeconds(5.0f);

        // PanelManager.StartCountdown();
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < classicModeOrder.Length; i++)
        {
            Debug.Log(classicModeOrder[i]);
            activeDistance = classicModeOrder[i];
            if (activeDistance <= 50)
            {
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(8f);
            }
            targetsHit.Clear();
            activeDistance = -1;
            yield return new WaitForSeconds(3f);
        }
        OnGameOver?.Invoke(currentMode);
    }

    IEnumerator PlayLongrange()
    {
        yield return new WaitForSeconds(5.0f);

        // PanelManager.StartCountdown();
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < longrangeModeOrder.Length; i++)
        {
            Debug.Log(longrangeModeOrder[i]);
            activeDistance = longrangeModeOrder[i];
            if (activeDistance <= 50)
            {
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(8f);
            }
            targetsHit.Clear();
            activeDistance = -1;
            yield return new WaitForSeconds(3f);
        }
        OnGameOver?.Invoke(currentMode);
    }

    IEnumerator PlayMarathon()
    {
        yield return new WaitForSeconds(5.0f);

        // PanelManager.StartCountdown();
        yield return new WaitForSeconds(3.0f);

        int[] distance = { 25, 50, 100, 200, 250 };
        // int[] distance = { 25 };
        while (true)
        {
            int randomIndex = UnityEngine.Random.Range(0, distance.Length);
            int randomDistance = distance[randomIndex];
            Debug.Log (randomDistance);

            activeDistance = randomDistance;
            yield return new WaitForSeconds(8f);
            if (!targetsHit.Contains(laneNumber))
            {
                isGameOver = true;
            }
            targetsHit.Clear();
            activeDistance = -1;
            yield return new WaitForSeconds(3f);
            if (isGameOver) break;
        }
        yield return new WaitForSeconds(1f);
        OnGameOver?.Invoke(currentMode);
    }

    IEnumerator PlayMultiplayer()
    {
        yield return new WaitForSeconds(5f);
    }
}
