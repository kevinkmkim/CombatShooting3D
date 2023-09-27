using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Bullet")]
public class Bullet : ScriptableObject
{
    public GameObject bulletPrefab;
    public float speed;
}