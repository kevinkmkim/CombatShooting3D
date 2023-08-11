using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArmature : MonoBehaviour
{
    [SerializeField] private float smoothFactor = 50f;

    #region Properties
    private Gyroscope gyro;
    private Quaternion rotationOffset = Quaternion.identity;
    private Quaternion deviceRotation;
    #endregion

    private IEnumerator WaitForGyro()
    {
        float timeout = 5.0f;
        float elapsedTime = 0.0f;

        while (!SystemInfo.supportsGyroscope && elapsedTime < timeout)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Condition met. Proceeding...");
        }
        else
        {
            Debug.Log("Timeout occurred. Gyroscope is not supported on this device.");
        }
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForGyro());
    }

    private void Update()
    {
        if (gyro != null)
        {
            deviceRotation = gyro.attitude;
            Quaternion adjustedRotation = Quaternion.Euler(90f, 0f, 0f) * (new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w));
            rotationOffset = Quaternion.Slerp(rotationOffset, adjustedRotation, smoothFactor);
            transform.rotation = rotationOffset;
        }
    }
}
