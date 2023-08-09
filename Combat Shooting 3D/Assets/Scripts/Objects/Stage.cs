using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Stage")]
public class Stage : ScriptableObject
{
    public int id;
    public string stageName;
    public GameObject terrain;
    public GameObject target;
    public Vector3[] playerPositions;
    public int columns;
    public float[] distances;
    public Target[] targets;
}