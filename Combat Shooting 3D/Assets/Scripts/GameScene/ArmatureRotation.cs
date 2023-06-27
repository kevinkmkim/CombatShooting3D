using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureRotation : MonoBehaviour
{
    private bool gyroEnabled;

    private Gyroscope gyro;

    float smoothFactor = 5.0f;

    void Start()
    {
        gyroEnabled = EnableGyro();
        Debug.Log (gyroEnabled);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    void Update()
    {
        Quaternion newRotation =
            ConvertRightHandedToLeftHandedQuaternion(gyro.attitude *
            Quaternion.Euler(-90, 0, 0));

        // float angle = Quaternion.Angle(currRotation, Quaternion.identity);
        // Quaternion newRotation = ClampRotation(currRotation);
        transform.rotation =
            Quaternion
                .Slerp(transform.rotation,
                newRotation,
                Time.deltaTime * smoothFactor);
    }

    private Quaternion ClampRotation(Quaternion currentRotation)
    {
        return Quaternion
            .FromToRotation(Vector3.down,
            Vector3
                .ProjectOnPlane(currentRotation * Vector3.forward,
                Vector3.down));
    }

    private Quaternion
    ConvertRightHandedToLeftHandedQuaternion(Quaternion rightHandedQuaternion)
    {
        return new Quaternion(-rightHandedQuaternion.x,
            -rightHandedQuaternion.z,
            -rightHandedQuaternion.y,
            rightHandedQuaternion.w);
    }
}
