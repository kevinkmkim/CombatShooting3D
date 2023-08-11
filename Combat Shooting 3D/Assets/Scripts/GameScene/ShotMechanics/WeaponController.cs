using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{

    #region Serialized Field
    [SerializeField] private Transform weaponPivotTransform;
    #endregion

    #region Properties
    private bool isAiming = false;
    private Vector3 aimWeaponPosition = new Vector3(0.0f, -0.36f, 0.12f);
    private Quaternion aimWeaponRotation = Quaternion.identity;
    private Vector3 easeWeaponPosition = new Vector3(0.2f, -0.5f, 0.25f);
    private Quaternion easeWeaponRotation = Quaternion.Euler(12.0f, -3.5f, 15.0f);
    #endregion

    public void HandleShootEvent()
    {
        Debug.Log("SHOOT from WeaponController");
    }

    public void HandleAimEvent()
    {
        Debug.Log("SHOOT from WeaponController");
        
        isAiming = !isAiming;
        if (isAiming)
        {
            weaponPivotTransform.localPosition = aimWeaponPosition;
            weaponPivotTransform.localRotation = aimWeaponRotation;
        }
        else
        {
            weaponPivotTransform.localPosition = easeWeaponPosition;
            weaponPivotTransform.localRotation = Quaternion.identity;
        }
    }
}
