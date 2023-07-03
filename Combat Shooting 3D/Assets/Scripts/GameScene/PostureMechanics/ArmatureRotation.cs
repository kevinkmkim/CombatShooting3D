using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureRotation : MonoBehaviour
{

    #region Serialized Field
    
    [Header("Serizlied Field")]

    [SerializeField]
    private GameObject weaponPivot;

    #endregion

    #region Field Variable

    private bool isGyroEnabled;

    private Gyroscope gyro;

    float smoothFactor = 5.0f;

    private Vector3 armatureRotationYZ;

    private Vector3 globalForwardYZ;

    #endregion

    private void Start()
    {
        isGyroEnabled = EnableGyro();
        Debug.Log (isGyroEnabled);

        globalForwardYZ = Vector3.forward;
        armatureRotationYZ = new Vector3(0f, transform.forward.y, transform.forward.z);
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

        transform.rotation =
            Quaternion
                .Slerp(transform.rotation,
                newRotation,
                Time.deltaTime * smoothFactor);

        Debug.Log("y: " + transform.forward.y);

        armatureRotationYZ.y = transform.forward.y;
        armatureRotationYZ.z = transform.forward.z;

        Debug.Log("z: " + transform.forward.z);

        Debug.Log("Angle: " + Vector3.Angle(armatureRotationYZ, globalForwardYZ));
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
