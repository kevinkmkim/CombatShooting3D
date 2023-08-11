using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilEffect : MonoBehaviour
{
    // [SerializeField] private TriggerManager triggerEvents;

    #region Properties
    public float recoilDistance = 0.01f;
    public float recoilSpeed = 2.0f;
    private Vector3 originalPosition;
    #endregion

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void OnEnable()
    {
        // triggerEvents.OnShoot += HandleShoot;
    }

    void OnDisable()
    {
        // triggerEvents.OnShoot -= HandleShoot;
    }

    void HandleShoot()
    {
        if (!GameManager.isGameOver)
        {
            StartCoroutine(Recoil());
        }
    }

    IEnumerator Recoil()
    {
        float timer = 0.0f;

        Vector3 newPosition =
            transform.localPosition +
            transform.forward * UnityEngine.Random.Range(0.1f, 0.4f) +
            transform.up * UnityEngine.Random.Range(0.05f, 0.1f) +
            transform.right * UnityEngine.Random.Range(0.05f, 0.1f) * -1;

        while (timer < 0.2f)
        {
            transform.localPosition =
                Vector3
                    .Lerp(originalPosition,
                    newPosition,
                    Time.deltaTime * recoilSpeed);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;

        while (timer < 5.0f)
        {
            transform.localPosition =
                Vector3
                    .Lerp(transform.localPosition,
                    originalPosition,
                    Time.deltaTime * recoilSpeed);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
