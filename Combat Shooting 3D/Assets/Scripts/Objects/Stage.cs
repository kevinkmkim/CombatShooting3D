using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]
    public int id;
    public string stageName;
    public GameObject terrain;
    public GameObject target;
}