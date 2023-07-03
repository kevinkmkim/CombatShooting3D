using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// #if UNITY_IOS
// using UnityEngine.iOS;
// using CoreHaptics;
// #endif
public class TriggerManager : MonoBehaviour
{
    RectTransform triggerRectTranform;

    private float initialTouchPosition;

    private float deltaY;

    Touch? triggerTouch = null; // Touch that is used to pull the trigger

    private Gamepad gamepad;

    private float coolDownTime = 1.0f;

    private bool isCoroutineRunning = false;

    public event ShootDelegate OnShoot;

    public delegate void ShootDelegate();

    void Start()
    {
        Gamepad gamepad = Gamepad.current;
        triggerRectTranform = GetComponent<RectTransform>();
        initialTouchPosition = -1.0f;
        deltaY = 0.0f;
    }

    void Update()
    {
        DetectUIDrag();
        if (isTriggerPulled() && !isCoroutineRunning)
        {
            // INVOKE SHOT
            OnShoot?.Invoke();

            StartCoroutine(CoolDownCoroutine());
        }
    }

    private IEnumerator CoolDownCoroutine()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(coolDownTime);
        deltaY = 0.0f;
        isCoroutineRunning = false;
    }

    private bool isTriggerPulled()
    {
        if (deltaY < -1.0f)
        {
            return true;
        }
        return false;
    }

    private Touch? GetTriggerTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector2 screenPoint = touch.position;
            bool isInsideRect =
                RectTransformUtility
                    .RectangleContainsScreenPoint(triggerRectTranform,
                    screenPoint,
                    null);
            if (isInsideRect)
            {
                return touch;
            }
        }
        return null;
    }

    private void DetectUIDrag()
    {
        triggerTouch = GetTriggerTouch();

        if (triggerTouch.HasValue)
        {
            Touch touch = triggerTouch.Value;
            if (initialTouchPosition < 0)
            {
                initialTouchPosition = touch.position.y;
            }
            deltaY = touch.position.y - initialTouchPosition;
            // Debug.Log(touch.phase);
            // Debug.Log(touch.position.y);
            // Debug.Log (deltaY);
        }
        else
        {
            // Debug.Log("No Trigger Touch");
            initialTouchPosition = -1.0f;
            deltaY = 0;
        }
    }
}
