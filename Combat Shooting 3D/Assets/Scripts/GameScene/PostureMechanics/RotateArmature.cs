using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArmature : MonoBehaviour
{
    #region Field Variable
    private Gyroscope gyro;
    private float smoothFactor = 0.5f;
    private Quaternion rotationOffset = Quaternion.identity;
    private Quaternion deviceRotation;
    #endregion

    private void EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }

        Debug.LogWarning("Gyroscope is not supported on this device.");
    }

    private void OnEnable()
    {
        EnableGyro();
    }

    void Update()
    {
        if (gyro != null)
        {
            // Get the gyroscope's rotation and apply it to the GameObject
            deviceRotation = gyro.attitude;
            Quaternion adjustedRotation = Quaternion.Euler(90f, 0f, 0f) * (new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w));

            // Apply the smooth factor to the rotation update
            rotationOffset = Quaternion.Slerp(rotationOffset, adjustedRotation, smoothFactor);

            transform.rotation = rotationOffset;
        }
    }
}
