using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;

    public Transform firePoint;

    public GameObject bulletPrefab;

    // private float angleIncrement = 90f;

    private void Awake()
    {
        Instance = this;
    }

    public static void Shoot()
    {
        GameObject bulletInstance =
            Instantiate(Instance.bulletPrefab,
            Instance.firePoint.position,
            Instance.transform.rotation);

        bulletInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        bulletInstance.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
    }
}
