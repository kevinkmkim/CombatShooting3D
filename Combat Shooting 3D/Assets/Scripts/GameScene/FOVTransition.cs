using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVTransition : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    Coroutine zoomCoroutine;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (LowerLeftClick() && LowerRightClick())
        {
            //Stop old coroutine
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);

            //Start new coroutine and zoom within 1 second
            zoomCoroutine = StartCoroutine(LerpFieldOfView(cam, 20, 0.5f));
        }
        else
        {
            //Stop old coroutine
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);

            //Start new coroutine and zoom within 1 second
            zoomCoroutine = StartCoroutine(LerpFieldOfView(cam, 60, 0.5f));
        }
    }

    private bool LowerLeftClick()
    {
        foreach (Touch touch in Input.touches)
        {
            if (
                (touch.position.y < Screen.height * 2 / 3) &&
                (touch.position.x < Screen.width / 2)
            )
            {
                return true;
            }
        }
        return false;
    }

    private bool LowerRightClick()
    {
        foreach (Touch touch in Input.touches)
        {
            if (
                (touch.position.y < Screen.height * 2 / 3) &&
                (touch.position.x > Screen.width / 2)
            )
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator
    LerpFieldOfView(Camera targetCamera, float toFOV, float duration)
    {
        float counter = 0;

        float fromFOV = targetCamera.fieldOfView;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            float fOVTime = counter / duration;

            // Debug.Log(fOVTime);
            //Change FOV
            targetCamera.fieldOfView = Mathf.Lerp(fromFOV, toFOV, fOVTime);

            //Wait for a frame
            yield return null;
        }
    }
}
