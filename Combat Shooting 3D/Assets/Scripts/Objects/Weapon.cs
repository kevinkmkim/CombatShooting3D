using System;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public GameObject weaponPrefab;
    public Vector3 easePosition;
    public Vector3 aimPosition;
    public Bullet defaultBullet;
}

[CreateAssetMenu(menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
    public WeaponData weaponData;

    private Transform firePointPosition;
    private Transform primaryPivot;
    private Transform secondaryPivot;
}
