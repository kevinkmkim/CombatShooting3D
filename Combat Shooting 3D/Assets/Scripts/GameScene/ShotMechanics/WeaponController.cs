using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{

    #region Serialized Field
    [SerializeField] private Transform weaponPivotTransform;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject bulletPrefab;
    #endregion

    #region Properties
    private GameObject weaponInstance;
    private Weapon weaponComponent;

    private bool isAiming;
    
    private Vector3 aimWeaponPosition = new Vector3(0.0f, -0.36f, 0.12f);
    private Quaternion aimWeaponRotation = Quaternion.identity;
    private Vector3 easeWeaponPosition = new Vector3(0.2f, -0.5f, 0.25f);
    private Quaternion easeWeaponRotation = Quaternion.Euler(12.0f, -3.5f, 15.0f);

    private bool isMovingAim = false;
    private float transitionDuration = 0.8f;
    #endregion

    private void OnEnable()
    {

        weaponInstance = Instantiate(weaponPrefab);
        // weapon = weaponPrefab.GetComponent<Weapon>();

        weaponPivotTransform.localPosition = easeWeaponPosition;
        weaponPivotTransform.localRotation = Quaternion.identity;

        isAiming = false;
    }

    public void HandleShootEvent()
    {
        Debug.Log("SHOOT from WeaponController");
        // Instantiate(bulletPrefab, weaponPivotTransform);
        Vector3 initialScale = new Vector3(2, 2, 2);
        Instantiate(bulletPrefab);
    }

    public void HandleAimEvent()
    {
        Debug.Log("AIM from WeaponController");
        
        if (!isMovingAim)
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
        isMovingAim = true;
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
        
        isMovingAim = false;
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5 ? 4 * Mathf.Pow(t, 3) : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
}
