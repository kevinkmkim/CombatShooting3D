using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponPrefab;
    public Vector3 easePosition;
    public Vector3 aimPosition;
    public Bullet defaultBullet;
}
