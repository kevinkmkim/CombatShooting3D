using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    #region Serialized Field

    [Header("Serizlied Field")]
    [SerializeField] private Button readyWeaponButton;
    [SerializeField] private Transform weaponPivotTransform;

    #endregion

    #region Field Variable
    private bool isWeaponReady = false;
    private Vector3 readyWeaponPosition = new Vector3(0.0f, -0.36f, 0.12f);
    private Quaternion readyWeaponRotation = Quaternion.Euler(0f, 0f, 0f);
    private Vector3 easeWeaponPosition = new Vector3(0.2f, -0.5f, 0.25f);
    private Quaternion easeWeaponRotation = Quaternion.Euler(12.0f, -3.5f, 15.0f);
    #endregion

    void OnEnable()
    {
        readyWeaponButton.onClick.AddListener(OnReadyWeapon);
    }

    private void OnReadyWeapon()
    {
        isWeaponReady = !isWeaponReady;
        if (isWeaponReady)
        {
            weaponPivotTransform.position = readyWeaponPosition;
            weaponPivotTransform.rotation = readyWeaponRotation;
        }
        else
        {
            weaponPivotTransform.position = easeWeaponPosition;
            weaponPivotTransform.rotation = easeWeaponRotation;
        }
    }
}
