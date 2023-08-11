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
    private bool isAiming;
    
    private Vector3 aimWeaponPosition = new Vector3(0.0f, -0.36f, 0.12f);
    private Quaternion aimWeaponRotation = Quaternion.identity;
    private Vector3 easeWeaponPosition = new Vector3(0.2f, -0.5f, 0.25f);
    private Quaternion easeWeaponRotation = Quaternion.Euler(12.0f, -3.5f, 15.0f);

    private bool isWeaponMoving = false;
    private float transitionDuration = 0.8f;
    #endregion

    private void OnEnable()
    {
        isAiming = false;
        weaponPivotTransform.localPosition = easeWeaponPosition;
        weaponPivotTransform.localRotation = Quaternion.identity;
    }

    public void HandleShootEvent()
    {
        Debug.Log("SHOOT from WeaponController");
    }

    public void HandleAimEvent()
    {
        Debug.Log("AIM from WeaponController");
        
        if (!isWeaponMoving)
        {
            isAiming = !isAiming;
            if (isAiming)
            {
                StartCoroutine(MoveWeapon(aimWeaponPosition, aimWeaponRotation));
            }
            else
            {
                StartCoroutine(MoveWeapon(easeWeaponPosition, Quaternion.identity));
            }
        }
    }

    private IEnumerator MoveWeapon(Vector3 newPosition, Quaternion newQuaternion)
    {
        isWeaponMoving = true;
        float elapsedTime = 0.0f;
        Vector3 initialPosition = weaponPivotTransform.localPosition;
        Quaternion initialRotation = weaponPivotTransform.localRotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            float easedT = EaseInOutQuad(t);

            weaponPivotTransform.localPosition = Vector3.Lerp(initialPosition, newPosition, easedT);
            weaponPivotTransform.localRotation = Quaternion.Lerp(initialRotation, newQuaternion, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        weaponPivotTransform.localPosition = newPosition;
        weaponPivotTransform.localRotation = newQuaternion;
        
        isWeaponMoving = false;
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5 ? 4 * Mathf.Pow(t, 3) : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
}
