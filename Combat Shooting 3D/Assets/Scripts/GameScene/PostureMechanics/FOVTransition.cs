using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVTransition : MonoBehaviour
{
    #region Properties
    private Camera mainCamera;
    private Coroutine zoomCoroutine;
    #endregion

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (LowerLeftClick() && LowerRightClick())
        {
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(LerpFieldOfView(mainCamera, 20, 0.5f));
        }
        else
        {
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(LerpFieldOfView(mainCamera, 60, 0.5f));
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

            //Change FOV
            targetCamera.fieldOfView = Mathf.Lerp(fromFOV, toFOV, fOVTime);
            
            yield return null;
        }
    }
}
