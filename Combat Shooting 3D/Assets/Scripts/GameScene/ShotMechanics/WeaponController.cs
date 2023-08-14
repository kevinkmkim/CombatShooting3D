using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{

    #region Serialized Field
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform armatureTransform;
    #endregion

    #region Properties
    private GameObject weaponPrefab;
    private GameObject bulletPrefab;

    private bool isAiming;

    private Vector3 aimWeaponPosition = new Vector3(0.0f, -0.3f, 0.17f);
    private Quaternion aimWeaponRotation = Quaternion.identity;

    private Vector3 positionOffset = new Vector3(0.2f, -0.14f, 0.13f);
    private Vector3 easeWeaponPosition;
    private Quaternion easeWeaponRotation = Quaternion.Euler(12.0f, -3.5f, 15.0f);

    private bool isMovingAim = false;
    private float transitionDuration = 0.8f;
    #endregion

    private void OnEnable()
    {
        easeWeaponPosition = aimWeaponPosition + positionOffset;

        weaponPrefab = Instantiate(weapon.weaponPrefab, armatureTransform);
        weaponPrefab.transform.localPosition = easeWeaponPosition;
        weaponPrefab.transform.localRotation = easeWeaponRotation;

        bulletPrefab = weapon.defaultBullet.bulletPrefab;

        isAiming = false;
    }

    public void HandleShootEvent()
    {
        Debug.Log("SHOOT from WeaponController");
        Debug.Log(weaponPrefab.transform.position);
        Debug.Log(weaponPrefab.transform.localPosition);
        GameObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.transform.position = weaponPrefab.transform.position;
        bulletInstance.transform.rotation = weaponPrefab.transform.rotation;
        Vector3 newScale = new Vector3(5f, 5f, 5f);
        bulletInstance.transform.localScale = newScale;

        Rigidbody bulletRigidBody = bulletInstance.GetComponent<Rigidbody>();

        bulletRigidBody.velocity = bulletInstance.transform.forward * 500f;

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
        Vector3 initialPosition = weaponPrefab.transform.localPosition;
        Quaternion initialRotation = weaponPrefab.transform.localRotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            float easedT = EaseInOutQuad(t);

            weaponPrefab.transform.localPosition = Vector3.Lerp(initialPosition, newPosition, easedT);
            weaponPrefab.transform.localRotation = Quaternion.Lerp(initialRotation, newQuaternion, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        weaponPrefab.transform.localPosition = newPosition;
        weaponPrefab.transform.localRotation = newQuaternion;

        isMovingAim = false;
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5 ? 4 * Mathf.Pow(t, 3) : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
}
